using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// シネマシーンカメラ制御クラス
/// </summary>
public class CinemachineCameraController : MonoBehaviour
{
    // コンポーネント
    private CinemachineThirdPersonFollow m_CinemachineThirdPersonFollow;

    [Header("カメラの距離"), SerializeField]
    private float m_CameraDistanceMin;
    [SerializeField]
    private float m_CameraDistanceMax;

    [Header("スフィアキャストの半径"), SerializeField]
    private float m_SphereCastRadius;
    [Header("カメラとの衝突を管理するLayerMask"), SerializeField]
    private LayerMask m_layerMask;

    private void Awake()
    {
        if (m_CinemachineThirdPersonFollow == null)
            m_CinemachineThirdPersonFollow = GetComponent<CinemachineThirdPersonFollow>();
    }

    void Start()
    {
        
    }

    void Update()
    {
    }

    public void OnCinemachineThirdPersonFollowInitialized(float distance, float height)
    {
        if(m_CinemachineThirdPersonFollow == null)
            m_CinemachineThirdPersonFollow = GetComponent<CinemachineThirdPersonFollow>();

        // CinemachineThirdPerson初期化
        m_CinemachineThirdPersonFollow.CameraDistance = distance;
        m_CinemachineThirdPersonFollow.ShoulderOffset = new Vector3(0, height, 0);
    }

    public bool PlayOpeningCameraMiddle(float distance, float height, float lerpTime)
    {
        m_CinemachineThirdPersonFollow.CameraDistance = Mathf.Lerp(m_CinemachineThirdPersonFollow.CameraDistance, distance, lerpTime);
        m_CinemachineThirdPersonFollow.ShoulderOffset = Vector3.Lerp(m_CinemachineThirdPersonFollow.ShoulderOffset, new Vector3(0, height, 0), lerpTime);

        if ((distance - m_CinemachineThirdPersonFollow.CameraDistance) < 0.01f)
            return true;
        else return false;
    }

    public bool PlayOpeningCameraEnd(float distance, float height, float lerpTime)
    {
        m_CinemachineThirdPersonFollow.CameraDistance = Mathf.Lerp(m_CinemachineThirdPersonFollow.CameraDistance, distance, lerpTime);
        m_CinemachineThirdPersonFollow.ShoulderOffset = Vector3.Lerp(m_CinemachineThirdPersonFollow.ShoulderOffset, new Vector3(0, height, 0), lerpTime);

        if ((distance - m_CinemachineThirdPersonFollow.CameraDistance) < 0.01f)
            return true;
        else return false;
    }

    ///// <summary>
    ///// ターゲットからレイを飛ばしてカメラを壁の向こう側に行かせないようにする
    ///// </summary>
    //private void CastRayForInteractions()
    //{
    //    RaycastHit hit;
    //    m_CinemachineThirdPersonFollow.CameraDistance = Mathf.Clamp(m_OffSet, m_CameraDistanceMin, m_CameraDistanceMax);

    //    if (Physics.SphereCast(m_Target.transform.position, m_SphereCastRadius, -transform.forward, out hit, m_NormalOffSet, m_layerMask))
    //    {
    //        m_OffSet = hit.distance;
    //    }
    //    else
    //    {
    //        m_OffSet = m_NormalOffSet;
    //    }
    //}
}
