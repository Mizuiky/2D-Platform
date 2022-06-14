using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class HUD : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _coinPoints;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        ItemManager.Instance.OnCoinCollect += SetCoinPoints;

        SetCoinPoints(0);
    }

    private void OnDisable()
    {
        ItemManager.Instance.OnCoinCollect -= SetCoinPoints;
    }

    public void SetCoinPoints(int points)
    {
        var pts = points.ToString();

        var builder = new StringBuilder();
        builder.AppendFormat("X {0}", pts);

        _coinPoints.text = builder.ToString();
    }
}
