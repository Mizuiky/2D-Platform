using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    [Header("Shoot Fields Information")]

    [SerializeField]
    private Transform _shootPosition;

    [SerializeField]
    private float _delayBetweenShoots = 5f;

    private Coroutine _currentCoroutine;

    private bool _isShooting;

    private Player _playerReference;

    private void Start()
    {
        _playerReference = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            _isShooting = true;
            _currentCoroutine = StartCoroutine(HandleShot());
        }
            

        if (Input.GetKeyUp(KeyCode.A))
        {
            _isShooting = false;

            if (_currentCoroutine != null) 
                StopCoroutine(HandleShot());
        }           
    }

    private IEnumerator HandleShot()
    {
        while (_isShooting)
        {
            Shoot();
            yield return new WaitForSeconds(_delayBetweenShoots);
        }
    }

    private void Shoot()
    {
        var obj = SpawnManager.Instance.GetPooledProjectil();

        if(obj != null)
        {
            var projectil = obj.GetComponent<ProjectilBase>();

            if (projectil != null)
            {
                projectil.Init(_shootPosition, _playerReference.Side);
                AudioManager.Instance.PlayClipByType(SFXType.Shoot);
            }
               
        }              
    }
}
