using UnityEngine;

/// <summary>
/// InputManagerのリスト
/// </summary>
public static class InputManagerList
{
    public static float VerticalValue         => Input.GetAxis("Vertical");         // 移動用（パッド）
    public static float HorizontalValue       => Input.GetAxis("Horizontal");       // 移動用（パッド）
    //public static float HorizontalRawValue    => Input.GetAxisRaw("Horizontal");    // 入力用（パッド）
    public static float CameraVerticalValue   => Input.GetAxis("CameraVertical");   // カメラ用（パッド）
    public static float CameraHorizontalValue => Input.GetAxis("CameraHorizontal"); // カメラ用（パッド）
    public static float CameraVerticalValue_Mouse => Input.GetAxis("CameraVerticalMouse");   // カメラ用（パッド）
    public static float CameraHorizontalValue_Mouse => Input.GetAxis("CameraHorizontalMouse"); // カメラ用（パッド）

    public static bool Dash => Input.GetButton("Dash"); // ダッシュ
    public static bool Jump => Input.GetButtonDown("Jump"); // ジャンプ
    public static bool Attack => Input.GetButtonDown("Attack"); // 攻撃
    public static bool Hold => Input.GetButton("Hold"); // 持つ
    public static bool Interact => Input.GetButtonDown("Interact"); // インタラクト
    public static bool Cancel   => Input.GetButtonDown("Cancel");   // キャンセル
    public static bool Pause  => Input.GetButtonDown("Pause");  // ポーズ
    public static bool Submit => Input.GetButtonDown("Submit"); // 決定
    public static bool Back   => Input.GetButtonDown("Back");   // 戻る
}
