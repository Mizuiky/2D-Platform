using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour
{
    #region Serializable Fields

    [Header("Heart Animation")]

    [SerializeField]
    private Animator _animator;

    #endregion

    #region Private Fields

    private string _boolHide = "Hide";

    [SerializeField]
    private bool _isHide;

    #endregion

    public bool IsHide
    {
        get => _isHide;
    }

    public Animator Animator
    {
        get => _animator;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _isHide = false;
    }

    public void SetHeartVisibility(bool hide)
    {
        _isHide = hide ? true : false;

        _animator.SetBool(_boolHide, hide);
    }
}
