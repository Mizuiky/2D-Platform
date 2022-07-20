using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;

public class AudioManager : Singleton<AudioManager>
{
    public List<AudioSetup> audioSetup;

    public List<AudioSource> audioSource;

}

[System.Serializable]
public class AudioSetup
{
    public enum AudioType
    {
        Walk,
        Jump,
        Shoot,
        Coin,
        Level1,
        Menu
    }

    public AudioType type;
    public AudioClip clip;
}
