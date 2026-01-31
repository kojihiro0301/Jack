using UnityEditor.UIElements;
using UnityEngine;

public class HoldableObject : MonoBehaviour
{
    private Rigidbody m_Rigidbody;

    [SerializeField]
    private TagField m_HoldableTag;

    [Header("重さ"), SerializeField]
    private float m_Weight;

    [SerializeField]
    private Vector3 m_Offset;
    private Vector3 m_HeldPos;

    private bool IsHeld;

    private void Awake()
    {
        IsHeld = false;
        // タグの変更
        gameObject.tag = m_HoldableTag.name;
        m_Rigidbody = GetComponent<Rigidbody>();
        // Rigidbodyの追加
        if (m_Rigidbody == null)
            gameObject.AddComponent<Rigidbody>();

        m_Rigidbody.mass = m_Weight;
    }

    private void Update()
    {
        if (IsHeld)
            transform.localPosition = m_HeldPos;
    }

    public void OnGrabbed(Transform parent)
    {
        IsHeld = true;
        transform.SetParent(parent);
        Vector3 heldPos = new Vector3(m_Offset.x, transform.localPosition.y, m_Offset.z);
        m_HeldPos = heldPos;
    }

    public void OnSeparated()
    {
        IsHeld = false;
        transform.parent = null;
    }
}
