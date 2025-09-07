using System;
using UnityEngine;

public class AudioService : MonoBehaviour
{
    public void PlayOnce(AudioClip clip)
    {
        AudioManager.Instance.PlayOnce(clip, 1);
    }

    public void PlayOnceDownPitched(AudioClip clip)
    {
        AudioManager.Instance.PlayOnce(clip, 1, .5f);
    }
}
