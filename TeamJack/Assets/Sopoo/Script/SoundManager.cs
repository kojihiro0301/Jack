using UnityEngine;
using System;

[System.Serializable]
public class Sound
{
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    public enum SEname{Jump}
    public enum BGMname{Run}

    [Header("Sound Arrays")]
    [SerializeField, EnumLabel(typeof(BGMname))]
    private Sound[] bgmSounds;

    [SerializeField, EnumLabel(typeof(SEname))]

    private Sound[] sfxSounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(BGMname bgmname, float volume = 1.0f)
    {
        Sound s = bgmSounds[(int)bgmname];
        if (s == null)
        {
            return;
        }

        if (bgmSource.clip == s.clip && bgmSource.isPlaying) return;

        bgmSource.clip = s.clip;
        bgmSource.volume = volume;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    // --- SFX 재생 (이름으로 찾기) ---
    public void PlaySFX(SEname sename, float volume = 1.0f)
    {
        Sound s = sfxSounds[(int)sename];

        if (s == null)
        {
            Debug.LogWarning("SFX를 찾을 수 없습니다: " + name);
            return;
        }

        sfxSource.PlayOneShot(s.clip, volume);
    }
    
    public void StopBGM()
    {
        bgmSource.Stop();
    }
}