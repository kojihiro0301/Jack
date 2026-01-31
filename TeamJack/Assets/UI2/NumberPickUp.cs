using UnityEngine;

public class NumberPickup : MonoBehaviour
{
    [Header("管理マネージャー")]
    public NumberManager manager;

    private bool isCollected = false;

    // プレイヤーが触れたとき
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            GetNumber();
        }
    }

    // マウスでクリックしたとき（念のため）
    private void OnMouseDown()
    {
        if (!isCollected)
        {
            GetNumber();
        }
    }

    void GetNumber()
    {
        isCollected = true;

        if (manager != null)
        {
            manager.CollectNumber();
        }

        // 取得音などを鳴らすならここに記述

        Destroy(this.gameObject); // 自分を消す
    }
}