using DG.Tweening;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager s_Instance;
    public static GameManager Instance => s_Instance;

    // ÉQÅ[ÉÄÇ™èâä˙âªÇ≥ÇÍÇƒÇ¢ÇÈÇ©
    public bool IsGameInitialized { get; set; }

    /// <summary>
    /// êiíª
    /// </summary>
    public enum Progresses
    {
        
    }
    public bool[] ProgressesBool;

    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        IsGameInitialized = true;
        ProgressesBool = new bool[Enum.GetValues(typeof(Progresses)).Length];

        DOTween.SetTweensCapacity(500, 100);
    }

    private void Start()
    {
        
    }

    void Update()
    {
        
    }
}
