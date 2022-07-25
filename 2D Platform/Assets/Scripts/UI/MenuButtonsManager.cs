using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MenuButtonsManager : MonoBehaviour
{
    [Header("Animation")]
    public List<GameObject> buttons;

    [SerializeField]
    private float _duration = .2f;
    [SerializeField]
    private float _delay = .05f;
    [SerializeField]
    private Ease ease = Ease.OutBack;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        ResetButtonScale();

        HideButton();
        ShowButtons();
    }

    private void ShowButtons()
    {
        DelayButtons();
    }

    private void HideButton()
    {
        foreach (GameObject btn in buttons)
        {      
            btn.transform.localScale = Vector3.zero;
            btn.SetActive(false);
        }
    }

    private void ResetButtonScale()
    {
        foreach (GameObject btn in buttons)
        {
            btn.transform.localScale = Vector3.one;
        }
    }

    public void DelayButtons()
    {
        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].SetActive(true);
            buttons[i].transform.DOScale(1, _duration).SetDelay(i *_delay).SetEase(ease);
        }
    }
}
