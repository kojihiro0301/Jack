using UnityEngine;

/// <summary>
/// カメラターゲット制御クラス
/// </summary>
public class CameraTargetController : MonoBehaviour
{
    // 回転角度
    private Vector2 m_Rotate;
    public Vector2 Rotate => m_Rotate;
    [Header("回転制限"), SerializeField]
    private float m_RotateXMax;
    [SerializeField]
    private float m_RotateXMin;

    [Header("回転速度"), SerializeField]
    private Vector2 m_RotateSpeed;

    void Start()
    {

    }

    void Update()
    {
        RotateControll();
    }

    /// <summary>
    /// の回転
    /// </summary>
    private void RotateControll()
    {
        // カメラの回転
        m_Rotate.y += InputManagerList.CameraHorizontalValue * m_RotateSpeed.y * Time.deltaTime;
        m_Rotate.x -= InputManagerList.CameraVerticalValue * m_RotateSpeed.x * Time.deltaTime;

        // 制限を付ける
        m_Rotate.x = Mathf.Clamp(m_Rotate.x, m_RotateXMin, m_RotateXMax);

        // オブジェクトの回転を更新
        transform.rotation = Quaternion.Euler(m_Rotate.x, m_Rotate.y, 0);
    }

    /// <summary>
    /// カメラの表示している方向を正とし、その値を返す
    /// </summary>
    /// <returns>Targetのx軸回転を無視したforwad</returns>
    public Vector3 GetScreenForward()
    {
        Vector3 camForward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        return camForward;
    }

    /// <summary>
    /// カメラの表示している方向を正とし、その時のrightベクトル値を返す
    /// </summary>
    /// <returns>TargetのRightベクトル</returns>
    public Vector3 GetScreenRight()
    {
        Vector3 camRight = Vector3.ProjectOnPlane(transform.right, Vector3.up).normalized;
        return camRight;
    }
}