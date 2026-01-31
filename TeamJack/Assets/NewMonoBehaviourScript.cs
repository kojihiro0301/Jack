using UnityEngine;

public class TestMaskTrigger : MonoBehaviour
{
    // MaskDisスクリプトがついているオブジェクトをここにセットします
    [Header("テスト対象のマスク（MaskDisを持つオブジェクト）")]
    public MaskDis targetMask;

    // ゲーム開始時（最初のフレーム）に自動で実行されます
    void Start()
    {
        if (targetMask != null)
        {
            Debug.Log("【テスト実行】プレイ開始直後に FadeObj を呼び出しました。");

            // 関数を強制的に呼び出す
            targetMask.FadeObj();
        }
        else
        {
            Debug.LogError("テスト対象のマスクがセットされていません！インスペクターを確認してください。");
        }
    }
}