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
        Debug.Log("change color 1");
        _currentCoroutine = StartCoroutine(ColorTint());
    }

    private IEnumerator ColorTint()
    {
        Debug.Log("change color 2");
        ChangeAllColor(_color);

        yield return new WaitForSeconds(.1f);

        Debug.Log("change color 4");
        ChangeAllColor(Color.white);
    }

    private void ChangeAllColor(Color color)
    {
        Debug.Log("change color 3");
        _sprites.ForEach(sprite => sprite.color = color);
    }
}
