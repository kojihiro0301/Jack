/// <summary>
/// AnimatorParametersを登録する場所
/// </summary>
public static class AnimatorParametersManager
{
    // もう一つのアイドルモーションを再生しているか
    public static string IsAnotherIdolMotion => "IsAnotherIdolMotion";

    // 待機しているか
    public static string IsIdle => "IsIdle";

    // 移動しているか
    public static string IsMove => "IsMove";

    // ダッシュしているか
    public static string IsDash => "IsDash";

    // 攻撃しているか
    public static string IsAttack => "IsAttack";

    // ジャンプ開始
    public static string IsJump => "IsJump";
    // 浮遊しているか
    public static string IsFloat => "IsFloat";
    // 浮遊を終了したか
    public static string IsFloatEnd => "IsFloatEnd";
}
