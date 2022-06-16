using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_Health : ScriptableObject
{
    [Header("Health Fields")]

    public int startLife = 10;

    public float _delayToDestroy = 0.3f;

    public bool _destroyOnKill;
}
