using UnityEngine;

public class NumberManager : MonoBehaviour
{
    [Header("集める数字の総数")]
    public int totalNumbers = 4; // 2,5,9,1 なので 4

    [Header("ロックされているドア")]
    public GameObject lockedDoor;

    private int currentCount = 0;

    // 数字アイテムから呼ばれる
    public void CollectNumber()
    {
        currentCount++;
        Debug.Log($"数字取得: {currentCount}/{totalNumbers}");

        if (currentCount >= totalNumbers)
        {
            Unlock();
        }
    }

    void Unlock()
    {
        Debug.Log("ロック解除！");
        if (lockedDoor != null)
        {
            // ドアを消す（あるいはアニメーションさせる）
            lockedDoor.SetActive(false);
        }
    }
}