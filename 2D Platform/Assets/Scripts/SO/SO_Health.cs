using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_Health : ScriptableObject
{
    [Header("Health Fields")]

    public  int startLife;

    public float _delayToDestroy;

    public bool _destroyOnKill;
}
