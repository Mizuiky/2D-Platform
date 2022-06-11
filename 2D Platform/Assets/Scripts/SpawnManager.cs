using Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
    [Header("Projectil Spawner")]

    [SerializeField]
    private GameObject _projectilPrefab;
    [SerializeField]
    private int _projectilAmount;
    [SerializeField]
    private Transform _projectilParent;

    private List<GameObject> _projectils;

    private void Start()
    {
        InitProjectilPool();
    }

    private void InitProjectilPool()
    {
        _projectils = new List<GameObject>();

        for(int i = 0; i < _projectilAmount; i++)
        {
            var projectil = Instantiate(_projectilPrefab, _projectilParent);

            if (projectil != null)
            {
                projectil.SetActive(false);
                _projectils.Add(projectil);
            }                 
        }
    }

    public GameObject GetPooledProjectil()
    {
        foreach(GameObject projectil in _projectils)
        {
            if(!projectil.activeInHierarchy)
                return projectil;
        }

        return null;
    }
}
