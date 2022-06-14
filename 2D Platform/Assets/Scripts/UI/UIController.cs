using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private HUD _hud;

    public void SetCoinPoints(int points)
    {
        _hud.SetCoinPoints(points);
    }
}
