using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class HUD : MonoBehaviour
{
    #region Serializable Fields

    [SerializeField]
    private TextMeshProUGUI _coinText;

    [SerializeField]
    private SO_Int _coinPoints;

    [SerializeField]
    private LifeController _lifeController;

    #endregion

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

    public bool FillHeartContainer()
    {
        return _lifeController.FillHeart() ? true : false;
    }

    public bool HideHeartContainer()
    {
        return _lifeController.HideHeart();
    }
}
