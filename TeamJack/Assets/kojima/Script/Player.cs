using UnityEngine;

// プレイヤークラス
public class Player : MonoBehaviour
{
    private static Player s_Instance;
    public static Player Instance => s_Instance;

    // 他クラス
    private CameraTargetController m_CameraTargetController;
    private PlayerAttackCollider m_PlayerAttackCollider;

    // コンポーネント
    private CharacterController m_CharacterController;
    private Animator m_Animator;

    /// <summary>
    /// プレイヤーの状態
    /// </summary>
    enum PlayerState
    {
        Idle,     // アイドル
        Move,     // 移動
        Floating, // ジャンプ中
        Attack,   // 攻撃
        Hold      // 持つ
    }
    // 現在の状態
    private PlayerState m_CurrentPlayerState;

    [Header("待機モーション変化時間"), SerializeField]
    private float[] m_IdleMotionTransitionTime;
    private float[] m_IdleMotionTransitionTimer;
    private bool m_IsAnotherIdolMotion;
    [Header("各攻撃Animationの時間"), SerializeField]
    private float[] m_AttackAnimationTime;
    private float m_AttackAnimationTimer;

    // オブジェクトの速度
    private Vector3 m_Velocity;
    [Header("移動速度")]
    private float m_MoveSpeed;
    [SerializeField]
    private float m_WalkSpeed;
    [SerializeField]
    private float m_DashSpeed;

    // つかみ時の速度割合
    private float m_HoldStateSpeedRatio = 0.6f;
    // つかんでいるオブジェクト
    private HoldableObject m_HoldableObject;
    public HoldableObject HoldableObject => m_HoldableObject;

    // Y座標の速度
    private float m_VerticalVelocity;
    // 重力
    private const float m_Gravity = 9.81f;
    // 地面張り付き速度
    private const float m_StickToGroundVelocity = -2.0f;
    [Header("ジャンプ力"), SerializeField]
    private float m_JumpSpeed;
    [Header("落下時の速さ制限（Infinityで無制限）"), SerializeField]
    private float m_FallSpeed;

    // 回転速度
    private float m_TurnVelocity;

    // 移動しているか
    private bool m_IsMove;
    // ジャンプ中か？
    private bool m_IsJump;
    // 地面に接地しているか
    private bool m_IsGrounded;
    // つかみ準備状態か？
    private bool m_IsHeldPreparation;
    private bool m_IsHeld;

    private string m_MaskTag;

    private void Awake()
    {
        s_Instance = this;

        // コンポーネント取得
        m_CharacterController = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();

        // 値の初期化
        m_IdleMotionTransitionTimer = new float[m_IdleMotionTransitionTime.Length];
        m_IsJump = false;
        m_IsGrounded = true;
        m_AttackAnimationTimer = 0;
        m_HoldableObject = null;
        m_IsHeld = false;

        // アイドルモーションの初期化
        m_IsAnotherIdolMotion = false;
        m_IdleMotionTransitionTimer[0] = m_IdleMotionTransitionTime[0];
        m_IdleMotionTransitionTimer[1] = 0;
    }

    private void Start()
    {
        // クラス取得
        m_CameraTargetController = GetComponentInChildren<CameraTargetController>();
        m_PlayerAttackCollider = GetComponentInChildren<PlayerAttackCollider>();

        CheckGrounded();
        OnMove();
    }

    private void Update()
    {
        CheckGrounded();
        AnimationControl();
        if (!PlaySceneEventController.Instance.IsBeginCameraMotion) return;

        if (m_CurrentPlayerState != PlayerState.Attack)
        {
            OnJump();
            OnMove();
        }

        OnAttack();
        OnHeldPreparation();

        StateControl();
    }

    /// <summary>
    /// Playerの回転制御
    /// </summary>
    private void OnRotation(Vector3 velocity)
    {
        // 回転
        float targetAngleY = Mathf.Atan2(velocity.x, velocity.z)
            * Mathf.Rad2Deg;
        // イージングしながら次の回転角度[deg]を計算
        float angleY = Mathf.SmoothDampAngle(
            transform.eulerAngles.y,
            targetAngleY,
            ref m_TurnVelocity,
            0.1f
        );

        // プレーヤーの向きを変える
        if (velocity.magnitude > 0)
        {
            // オブジェクトの回転を更新
            transform.rotation = Quaternion.Euler(0, angleY, 0);
        }
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void OnMove()
    {
        // 視点の方向を取得
        Vector3 forward = m_CameraTargetController.GetScreenForward();
        Vector3 right = m_CameraTargetController.GetScreenRight();

        m_MoveSpeed = 0;
        m_Velocity = Vector3.zero;
        PlayerState playerState = PlayerState.Idle;

        // 移動速度を決める
        m_MoveSpeed = InputManagerList.Dash ? m_DashSpeed : m_WalkSpeed;

        if (m_CurrentPlayerState == PlayerState.Hold)
            m_MoveSpeed *= m_HoldStateSpeedRatio;

        // 速度を求める
        Vector3 velocity = forward * InputManagerList.VerticalValue * m_MoveSpeed +
                           right * InputManagerList.HorizontalValue * m_MoveSpeed;

        // 回転
        OnRotation(velocity);

        // 最終的な速度
        m_Velocity = new Vector3(velocity.x, m_VerticalVelocity, velocity.z) * Time.deltaTime;
        m_CharacterController.Move(m_Velocity);

        // スティックの入力がされている場合
        if (Vector3.Distance(velocity, Vector3.zero) > 0.01f)
        {
            m_IsMove = true;
            playerState = PlayerState.Move;
        }
        else m_IsMove = false;

        if (m_CurrentPlayerState != PlayerState.Hold)
            ChangeState(playerState);
    }

    /// <summary>
    /// 地面に接地しているか判定する
    /// </summary>
    private void CheckGrounded()
    {
        m_IsGrounded = m_CharacterController.isGrounded;

        // 空中にいるときは、下向きに重力加速度を与えて落下させる
        if (!m_IsGrounded)
        {
            m_VerticalVelocity -= m_Gravity * Time.deltaTime;
        }
        else
        {
            if (!m_IsJump)
                // 速度を制限する
                m_VerticalVelocity = m_StickToGroundVelocity;

            m_IsJump = false;
        }
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    private void OnAttack()
    {
        if (InputManagerList.Attack
            && !m_IsJump
            && m_CurrentPlayerState != PlayerState.Attack
            && m_CurrentPlayerState != PlayerState.Hold)
        {
            ChangeState(PlayerState.Attack);
        }
    }

    private void OnHeldPreparation()
    {
        if (InputManagerList.Hold)
        {
            if (!m_IsJump
            && m_CurrentPlayerState != PlayerState.Attack
            && m_CurrentPlayerState != PlayerState.Hold)
                m_IsHeldPreparation = true;
        }
        else m_IsHeldPreparation = false;


        if (m_IsHeldPreparation
            && !m_IsHeld
            && m_PlayerAttackCollider.InObject() != null
            && m_PlayerAttackCollider.IsHoldable
            && m_HoldableObject == null)
        {
            m_IsHeld = true;
            HoldableObject holdableObject = m_PlayerAttackCollider.InObject().GetComponent<HoldableObject>();
            OnHeld(holdableObject);
        }
    }

    public void OnHeld(HoldableObject heldObject)
    {
        m_HoldableObject = heldObject;
        m_HoldableObject.OnGrabbed(transform);
        ChangeState(PlayerState.Hold);
    }

    /// <summary>
    /// ジャンプ
    /// </summary>
    private void OnJump()
    {
        if (InputManagerList.Jump
            && m_IsGrounded
            && m_CurrentPlayerState != PlayerState.Hold)
        {
            m_VerticalVelocity = m_JumpSpeed;
            ChangeState(PlayerState.Floating);
        }
    }

    /// <summary>
    /// 状態の変化
    /// 変化時に一度だけ処理する
    /// </summary>
    /// <param name="nextState"></param>
    private void ChangeState(PlayerState nextState, int num = 0)
    {
        if (m_CurrentPlayerState != nextState)
        {
            switch (nextState)
            {
                case PlayerState.Idle:
                    // デフォルトのアイドルモーション時間の初期化
                    m_IsAnotherIdolMotion = false;
                    m_IdleMotionTransitionTimer[0] = m_IdleMotionTransitionTime[0];
                    m_IdleMotionTransitionTimer[1] = 0;
                    break;

                case PlayerState.Floating:
                    m_IsGrounded = false;
                    m_IsJump = true;
                    break;

                case PlayerState.Attack:
                    m_AttackAnimationTimer = m_AttackAnimationTime[num];
                    break;
            }

            m_CurrentPlayerState = nextState;
        }
    }

    /// <summary>
    /// 状態ごとの制御
    /// </summary>
    private void StateControl()
    {
        switch (m_CurrentPlayerState)
        {
            case PlayerState.Idle:
                if (m_IdleMotionTransitionTimer[0] >= 0)
                    m_IdleMotionTransitionTimer[0] -= Time.deltaTime;

                if (m_IdleMotionTransitionTimer[1] >= 0)
                    m_IdleMotionTransitionTimer[1] -= Time.deltaTime;

                // デフォルトのアニメーション時間が一定時間を過ぎた場合
                if (m_IdleMotionTransitionTimer[0] <= 0 && !m_IsAnotherIdolMotion)
                {
                    // もう一つのアニメーションを再生するためのトリガーをオンにする
                    m_IsAnotherIdolMotion = true;
                    m_IdleMotionTransitionTimer[1] = m_IdleMotionTransitionTime[1];
                }

                // もう一つのアニメーション時間が一定時間を過ぎた場合
                if (m_IdleMotionTransitionTimer[1] <= 0 && m_IsAnotherIdolMotion)
                {
                    // デフォルトのアニメーションを再生する
                    m_IsAnotherIdolMotion = false;
                    m_IdleMotionTransitionTimer[0] = m_IdleMotionTransitionTime[0];
                }
                break;

            case PlayerState.Move:
                break;

            case PlayerState.Floating:
                if (m_IsGrounded)
                    ChangeState(PlayerState.Idle);
                break;

            case PlayerState.Attack:
                m_AttackAnimationTimer -= Time.deltaTime;

                if (m_AttackAnimationTimer <= 0)
                {
                    ChangeState(PlayerState.Idle);
                }
                break;

            case PlayerState.Hold:
                if ((!m_IsHeldPreparation && m_IsHeld) || m_HoldableObject == null)
                {
                    if (m_HoldableObject != null)
                    {
                        m_IsHeld = false;
                        m_HoldableObject.OnSeparated();
                        m_HoldableObject = null;
                    }

                    ChangeState(PlayerState.Idle);
                }
                break;
        }
    }

    /// <summary>
    /// Animation制御
    /// </summary>
    private void AnimationControl()
    {
        m_Animator.SetBool(AnimatorParametersManager.IsIdle, m_CurrentPlayerState == PlayerState.Idle);
        m_Animator.SetBool(AnimatorParametersManager.IsAnotherIdolMotion, m_IsAnotherIdolMotion);
        m_Animator.SetBool(AnimatorParametersManager.IsMove, m_IsMove);
        m_Animator.SetBool(AnimatorParametersManager.IsDash, InputManagerList.Dash);
        m_Animator.SetBool(AnimatorParametersManager.IsJump, !m_IsGrounded);
        m_Animator.SetBool(AnimatorParametersManager.IsAttack, m_CurrentPlayerState == PlayerState.Attack);
        m_Animator.SetBool(AnimatorParametersManager.IsHeld, m_CurrentPlayerState == PlayerState.Hold || m_IsHeldPreparation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("m_MaskTag"))
        {
            MaskDis maskDis = other.GetComponent<MaskDis>();
            other.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        s_Instance = null;
    }
}


