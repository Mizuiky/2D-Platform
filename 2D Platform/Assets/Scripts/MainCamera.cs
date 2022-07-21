using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MainCamera : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera _currentCam;

    [SerializeField]
    private Transform _originalPosition;

    private void Awake()
    {
        GameManager.OnFollowPlayer += SetCameraPosition;
    }

    private void OnDisable()
    {
        GameManager.OnFollowPlayer -= SetCameraPosition;
    }

    public void SetCameraPosition(Transform player)
    {
        if (_currentCam != null)
        {
            _currentCam.gameObject.transform.position = _originalPosition.position;
            _currentCam.Follow = player;            
        }
    }
}
