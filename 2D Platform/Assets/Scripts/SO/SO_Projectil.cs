using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SO_Projectil : ScriptableObject
{
    [Header("Projectil Information")]

    public int _projectilDamage;

    public float _timeToDeactivate;

    public float _speed;
}
