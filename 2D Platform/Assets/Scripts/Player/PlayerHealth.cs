using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthBase
{
    private void Start()
    {
        ItemManager.Instance.onHeartCollected += base.Heal;
    }

    private void OnDisable()
    {
        ItemManager.Instance.onHeartCollected -= base.Heal;
    }
}
