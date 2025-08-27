using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField]
    private Transform _sfxParent;
    public AudioClip swingJump;
    public float swingJumpVolume = 1;
    public AudioClip swingGood;
    public float swingGoodVolume = 1;
    public AudioClip swingCrit;
    public float swingCritVolume = 1;
    public AudioClip swingMiss;
    public float swingMissVolume = 1;
    public AudioClip star;
    public float starVolume = 1;
    public AudioClip meteor;
    public float meteorVolume = 1;
    public AudioClip fuleOut;
    public float fuleOutVolume = 1;
    public AudioClip click;
    public float clickVolume = 1;

    // Music
    public AudioSource musicPlayer;
    public AudioClip nextMusic;
    public float currentMusicTargetVolume;
    public float nextMusicTargetVolume;
    private bool fadeIn = false;
    private bool fadeOut = false;
    public float fadeAmount = 1; 
    public AudioClip intoMusic;
    public float intoMusicVolume = 1;
    public AudioClip swingingMusic;
    public float swingingMusicVolume = 1;
    public AudioClip flyingMusic;
    public float flyingMusicVolume = 1;
    public AudioClip outroMusic;
    public float outroMusicVolume = 1;


    // Pitch
    public float pitchIncrease = 0.1f;
    public float stdPitch = 1;
    public float swingPitch = 1;
    public float starPitch = 1;
    private FixedTimer lastStarPitch;
    private FixedTimer lastSwingPitch;
    public float pitchResetTime = 1f;

    private void Start()
    {
        lastStarPitch = new();
        lastSwingPitch = new();
    }

    private void Update()
    {
        if (lastStarPitch.GetTime() > pitchResetTime)
        {
            starPitch = stdPitch;
        }
        if (lastSwingPitch.GetTime() > pitchResetTime)
        {
            swingPitch = stdPitch;
        }

        if (fadeIn && musicPlayer.volume < currentMusicTargetVolume)
        {
            musicPlayer.volume += fadeAmount * Time.deltaTime;
        }
        else if (fadeIn)
        {
            fadeIn = false;
        }

        if (fadeOut && musicPlayer.volume > 0)
        {
            musicPlayer.volume -= fadeAmount * Time.deltaTime;
        }
        else if (fadeOut)
        {
            fadeOut = false;
            fadeIn = true;
            musicPlayer.Stop();
            musicPlayer.clip = nextMusic;
            currentMusicTargetVolume = nextMusicTargetVolume;
            musicPlayer.Play();
        }
    }

    public static AudioManager Instance;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayIntroMusic()
    {
        PlayMusic(intoMusic, intoMusicVolume);
    }

    public void PlayOutroMusic()
    {
        PlayMusic(outroMusic, outroMusicVolume);
    }

    public void PlaySwingingMusic()
    {
        PlayMusic(swingingMusic, swingingMusicVolume);
    }

    public void PlayFlyingMusic()
    {
        PlayMusic(flyingMusic, flyingMusicVolume);
    }

    public void PlayMusic(AudioClip audioClip, float volume)
    {
        nextMusic = audioClip;
        nextMusicTargetVolume = volume;
        fadeOut = true;
    }

    public void PlaySwingJump()
    {
        PlayOnce(swingJump, swingJumpVolume);
    }

    public void PlaySwingGood()
    {
        PlayOnce(swingGood, swingGoodVolume, swingPitch * 0.7f);
        swingPitch += pitchIncrease;
        lastSwingPitch.Start();
    }

    public void PlaySwingCrit()
    {
        PlayOnce(swingCrit, swingCritVolume, swingPitch);
        swingPitch += pitchIncrease;
        lastSwingPitch.Start();
    }

    public void PlaySwingMiss()
    {
        PlayOnce(swingMiss, swingingMusicVolume, 0.4f);
    }

    public void PlayStar()
    {
        PlayOnce(star, starVolume, starPitch);
        starPitch += pitchIncrease;
        lastStarPitch.Start();
    }

    public void PlayMeteor()
    {
        PlayOnce(meteor, meteorVolume);
    }

    public void PlayFuleOut()
    {
        PlayOnce(fuleOut, fuleOutVolume);
    }

    public void PlayClick()
    {
        PlayOnce(click, clickVolume);
    }

    public void PlayOnce(AudioClip clip, float volume)
    {
        PlayOnce(clip, volume, stdPitch);
    }

    public void PlayOnce(AudioClip clip, float volume, float pitch)
    {
        GameObject sfx = new GameObject("SFX");
        sfx.transform.parent = _sfxParent;
        AudioSource source = sfx.AddComponent<AudioSource>();
        source.clip = clip;
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
        float destructionTime = clip.length + 0.1f;
        StartCoroutine(DestroyAudioSource(sfx, destructionTime));
    }
    
    private IEnumerator DestroyAudioSource(GameObject audioSouce, float destructionTime)
    {
        yield return new WaitForSeconds(destructionTime);
        Destroy(audioSouce);
    }
}
