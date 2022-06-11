using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimation
{
    public Animator Animator { get; }

    public string IsRunning { get; }

    public string IsDead  { get; }

    public string Attack { get; }

    public void Init();

    public void CallDeath();

    public void CallRun(bool isRunning);

    public void CallAttack();

    public void SetAnimationSpeed(float speed);
}
