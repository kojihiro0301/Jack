using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [Header("”j‰óŽžEffect"), SerializeField]
    private GameObject m_BreakEffect;

    private Rigidbody m_Rigidbody;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        if (m_Rigidbody == null)
            m_Rigidbody = gameObject.AddComponent<Rigidbody>();
    }

    void Update()
    {

    }

    /// <summary>
    /// ”j‰ó
    /// </summary>
    public void OnBreak()
    {
        if (m_BreakEffect != null)
            Instantiate(m_BreakEffect, transform.position, m_BreakEffect.transform.rotation);
        gameObject.SetActive(false);
    }
}
