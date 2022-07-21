using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using System.Linq;

public class AudioManager : Singleton<AudioManager>
{
    [Header("SFX Setup")]

    public List<SFXSetup> audioSetup;

    public AudioSource [] audioSource;

    [Header("SFX Level Setup")]

    public SFXLevelSetup [] _level;

    public AudioSource _levelSource;

    private Dictionary<Level, AudioClip> _audioLevel;

    private int _index;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        _index = 0;
        CreateLevelDictionary();
    }

    public void PlayClipByType(SFXType type)
    {
        if (_index >= audioSource.Length)
            Init();
        
        List<SFXSetup> setup = audioSetup.Where(x => x.type.Equals(type)).ToList();

        if(setup.Count > 0)
        {
            var source = audioSource[_index];

            if (type != SFXType.Walk)
            {
                source.clip = setup.First().clip;
            }
            else
            {
                source.clip = GetRandomSFXClip(setup);
            }

            if (source.clip != null)
                source.Play();

            _index++;
        }       
    }

    public void PlayLevelAudio(Level level)
    {
        var clip = GetLevelClip(level);

        if(clip != null)
        {
            _levelSource.clip = clip;
            _levelSource.Play();
        }
    }

    public void CreateLevelDictionary()
    {
        _audioLevel = new Dictionary<Level, AudioClip>();

        foreach(SFXLevelSetup set in _level)
        {
            _audioLevel.Add(set.type, set.clip);
        }
    }

    public AudioClip GetLevelClip(Level level)
    {
        return _audioLevel[level];
    }

    public AudioClip GetRandomSFXClip(List<SFXSetup> setup)
    {
        return setup[Random.Range(0, setup.Count - 1)].clip;
    }
}

public enum SFXType
{
    Walk,
    Jump,
    Shoot,
    Coin,
    Level1,
    Menu
}

[System.Serializable]
public class SFXSetup
{
    public SFXType type;
    public AudioClip clip;
}

[System.Serializable]
public class SFXLevelSetup
{
    public Level type;
    public AudioClip clip;
}

