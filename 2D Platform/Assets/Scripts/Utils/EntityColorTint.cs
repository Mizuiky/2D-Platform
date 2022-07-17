using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityColorTint : MonoBehaviour
{
    [SerializeField]
    private List<SpriteRenderer> _sprites;

    [SerializeField]
    private Color _color;

    private Coroutine _currentCoroutine;

    private void OnValidate()
    {
        _sprites = new List<SpriteRenderer>();

        foreach(var child in gameObject.GetComponentsInChildren<SpriteRenderer>())
        {
            _sprites.Add(child);
        }
    }

    private void Awake()
    {
        Init();    
    }

    public void Init()
    {
        gameObject.SetActive(true);   
    }

    public void ChangeColor()
    {
        _currentCoroutine = StartCoroutine(ColorTint());
    }

    private IEnumerator ColorTint()
    {
        ChangeAllColor(_color);

        yield return new WaitForSeconds(.1f);

        ChangeAllColor(Color.white);
    }

    private void ChangeAllColor(Color color)
    {
        _sprites.ForEach(sprite => sprite.color = color);
    }
}
