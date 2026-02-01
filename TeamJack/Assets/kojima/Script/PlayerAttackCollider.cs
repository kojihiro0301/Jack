using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{
    // つかめるオブジェクトのTag
    private string m_HeldObjectTag = "Holdable";
    private string m_BreakObjectTag = "Breakable";

    [SerializeField]
    private GameObject m_InObject;

    public bool IsHoldable { get; set; }
    public bool IsBreakable { get; set; }

    public GameObject InObject()
    {
        return m_InObject;
    }

    public void ClearInObject()
    {
        m_InObject = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Player.Instance.HoldableObject == null)
        {
            if (other.CompareTag(m_HeldObjectTag))
            {
                IsHoldable = true;
                m_InObject = other.gameObject;
            }
            else if(other.CompareTag(m_BreakObjectTag))
            {
                IsBreakable = true;
                m_InObject = other.gameObject;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(m_HeldObjectTag))
        {
            m_InObject = null;
            IsHoldable = false;
        }
        else if (other.CompareTag(m_BreakObjectTag))
        {
            m_InObject = null;
            IsBreakable = false;
        }
    }
}
