using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChangeAudioVolume : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _groupMixer;

    [SerializeField]
    private string _parameter;

    public void SetGroupVolume(float volume)
    {
        _groupMixer.SetFloat(_parameter, volume);
    }
}
