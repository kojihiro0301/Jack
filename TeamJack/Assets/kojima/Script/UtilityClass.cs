using UnityEngine;

public static class UtilityClass
{
    /// <summary>
    /// Bool”z—ñ‚ğ‰Šú‰»‚·‚é
    /// </summary>
    /// <param name="targetBool"> ‰Šú‰»‚³‚¹‚éBool”z—ñ </param>
    /// <param name="enable"> true ‚© false ‚É‰Šú‰» </param>
    public static void BoolReset(bool[] targetBool, bool enable)
    {
        for (int i = 0; i < targetBool.Length; i++)
        {
            targetBool[i] = enable;
        }
    }
}
