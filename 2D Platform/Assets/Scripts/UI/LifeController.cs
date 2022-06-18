using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LifeController : MonoBehaviour
{
    #region Serializable Fields

    [SerializeField]
    private List<Life> _lives;

    #endregion


    //To Do
    //Instantiate hearts according of the soPlayerlives

    public bool HideHeart()
    {
        if (CountHiddenHearts() == _lives.Count)
            return false;

        for (int i = _lives.Count - 1; i >= 0; i--)
        {
            if (!_lives[i].IsHide)
            {
                _lives[i].SetHeartVisibility(true);
                return true;
            }
        }

        return false;
    }

    public bool FillHeart()
    {
        if (CountVisibleHearts() == _lives.Count)
            return false;
        
        for(int i = 0; i < _lives.Count; i++)
        {
            if (_lives[i].IsHide)
            {
                _lives[i].SetHeartVisibility(false);
                return true;
            }
        }

        return false;
    }

    private int CountVisibleHearts()
    {
        return _lives.Count(life => !life.IsHide);
    }

    private int CountHiddenHearts()
    {
        return _lives.Count(life => life.IsHide);
    }
}
