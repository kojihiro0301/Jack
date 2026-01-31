using System;
using UnityEngine;

public class PlaySceneEventController : MonoBehaviour
{
    private static PlaySceneEventController s_Instance;
    public static PlaySceneEventController Instance => s_Instance;

    [SerializeField]
    private CinemachineCameraController m_CinemachineCameraController;
    [SerializeField]
    private CameraTargetController m_CameraTargetController;

    enum CameraMotionChackPoint
    {
        Start, // 開始位置
        Mid,   // 中間
        End    // 最終
    }

    private bool[] m_IsCameraMotionChackPoint;

    // CameraのMotionをしたか？
    public bool IsBeginCameraMotion { get; set; }

    [Header("遅延時間"), SerializeField]
    private float m_DelayTime;
    private float m_DelayTimer;

    // ゲーム開始時の演出用の変数
    [SerializeField]
    private Vector3 m_StartTargetRotate;
    [SerializeField]
    private Vector3 m_EndTargetRotate;

    // ゲーム開始時の演出用変数
    [Header("カメラの距離間"), SerializeField]
    private float[] m_CameraDistance;
    [Header("カメラの高さ"), SerializeField]
    private float[] m_CameraHeight;

    [Header("キャラクターから遠のく速さ")]
    [Header("中間まで"), SerializeField]
    private float m_MidRecedesSpeed;
    [Header("終盤まで"), SerializeField]
    private float m_EndRecedesSpeed;
    [Header("カメラの移動する速度"), SerializeField]
    private float m_CameraMoveDuration;

    private void Awake()
    {
        // インスタンスを変数に入れておく
        s_Instance = this;
    }

    void Start()
    {
        if (GameManager.Instance.IsGameInitialized)
        {
            IsBeginCameraMotion = false;
            m_IsCameraMotionChackPoint = new bool[Enum.GetValues(typeof(CameraMotionChackPoint)).Length];
            UtilityClass.BoolReset(m_IsCameraMotionChackPoint, false);
            m_DelayTimer = m_DelayTime;

            m_CinemachineCameraController.OnCinemachineThirdPersonFollowInitialized(m_CameraDistance[(int)CameraMotionChackPoint.Start], m_CameraHeight[(int)CameraMotionChackPoint.Start]);
            m_CameraTargetController.OnTargetTransformInitialized(m_StartTargetRotate);

            m_IsCameraMotionChackPoint[(int)CameraMotionChackPoint.Start] = true;
        }
        else
        {
            IsBeginCameraMotion = true;
        }
    }

    void Update()
    {
        if (m_DelayTimer >= 0)
            m_DelayTimer -= Time.deltaTime;

        if (GameManager.Instance.IsGameInitialized && !IsBeginCameraMotion)
        {
            if (m_IsCameraMotionChackPoint[(int)CameraMotionChackPoint.Start] && !m_IsCameraMotionChackPoint[(int)CameraMotionChackPoint.Mid] && m_DelayTimer <= 0)
            {
                bool isMid = m_CinemachineCameraController.PlayOpeningCameraMiddle(m_CameraDistance[(int)CameraMotionChackPoint.Mid], m_CameraHeight[(int)CameraMotionChackPoint.Mid], m_MidRecedesSpeed);
                if (isMid && !m_IsCameraMotionChackPoint[(int)CameraMotionChackPoint.Mid])
                {
                    m_DelayTimer = m_DelayTime;
                    m_IsCameraMotionChackPoint[(int)CameraMotionChackPoint.Mid] = true;
                }
            }
            else if (m_IsCameraMotionChackPoint[(int)CameraMotionChackPoint.Mid] && !m_IsCameraMotionChackPoint[(int)CameraMotionChackPoint.End] && m_DelayTimer <= 0)
            {
                m_CameraTargetController.OnOpeningSequence(
                    m_EndTargetRotate,
                    m_CameraMoveDuration,
                    DG.Tweening.Ease.OutQuad,
                    (() => { m_CinemachineCameraController.PlayOpeningCameraEnd(m_CameraDistance[(int)CameraMotionChackPoint.End], m_CameraHeight[(int)CameraMotionChackPoint.End], m_EndRecedesSpeed); })
                    );
                m_IsCameraMotionChackPoint[(int)CameraMotionChackPoint.End] = m_CameraTargetController.IsTweenEnd;

                if (m_IsCameraMotionChackPoint[(int)CameraMotionChackPoint.End])
                    IsBeginCameraMotion = true;
            }
        }
    }

    private void OnDestroy()
    {
        s_Instance = null;
    }
}
