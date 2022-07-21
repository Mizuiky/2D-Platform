using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class ChangeAudioTransition : MonoBehaviour
{
    [SerializeField]
    private AudioMixerSnapshot _snapshot;

    [SerializeField]
    private float _transitionTime;

    public void DoAudioTransition()
    {
        _snapshot.TransitionTo(_transitionTime);
    }
}
