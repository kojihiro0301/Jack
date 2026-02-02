using UnityEngine;

public class HoldableObject : MonoBehaviour
{
    private Rigidbody m_Rigidbody;

    [Header("èdÇ≥"), SerializeField]
    private float m_Weight;

    [SerializeField]
    private Vector3 m_Offset;
    private Vector3 m_HeldPos;

    private bool IsHeld;

    [SerializeField]
    private GameObject m_ArrowUI;

    private void Awake()
    {
        IsHeld = false;
        m_Rigidbody = GetComponent<Rigidbody>();
        // RigidbodyÇÃí«â¡
        if (m_Rigidbody == null)
            m_Rigidbody = gameObject.AddComponent<Rigidbody>();

        m_Rigidbody.mass = m_Weight;

        if (m_ArrowUI != null)
            m_ArrowUI.SetActive(true);
    }

    private void Update()
    {
        if (IsHeld)
            transform.localPosition = m_HeldPos;
    }

    public void OnGrabbed(Transform parent)
    {
        IsHeld = true;
        if (m_ArrowUI != null)
            m_ArrowUI.SetActive(false);
        transform.SetParent(parent);
        Vector3 heldPos = new Vector3(m_Offset.x, transform.localPosition.y, m_Offset.z);
        m_HeldPos = heldPos;
    }

    public void OnSeparated()
    {
        IsHeld = false;
        if (m_ArrowUI != null)
            m_ArrowUI.SetActive(true);
        transform.parent = null;
    }
}
