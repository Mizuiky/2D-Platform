using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _coinText;
    [SerializeField]
    private SO_Int _coinPoints;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        SetCoinPoints();
    }

    public void SetCoinPoints()
    {
        var pts = _coinPoints.value.ToString();

        var builder = new StringBuilder();
        builder.AppendFormat(" X {0} ", pts);

        _coinText.text = builder.ToString();
    }
}
