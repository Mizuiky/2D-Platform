using Core.Singleton;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    #region Serializable Fields

    [Header("Player Setup")]

    [SerializeField]
    private SO_PlayerSetup _playerSetup;

    [Header("Enemies")]

    [SerializeField]
    private List<GameObject> enemiePrefab;

    [Header("References")]

    [SerializeField]
    private Transform startPoint;

    [Header("UI")]

    [SerializeField]
    private UIController _ui;

    #endregion

    private PlayerAnimation _currentPlayerAnimator;

    private Player _currentPlayer;

    public static event Action<Transform> OnFollowPlayer;

    private void Awake()
    {
        _currentPlayer = FindObjectOfType<Player>();
    }

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        SpawnPlayer();
        AudioManager.Instance.PlayLevelAudio(Level.PLANET);
    }

    private void SpawnPlayer()
    {
        _currentPlayerAnimator = Instantiate(_playerSetup.player, _currentPlayer.transform);
        _currentPlayer.transform.position = startPoint.transform.position;

        _currentPlayer.PlayerAnimation = _currentPlayerAnimator;

        _currentPlayer.Health.SetTint(_currentPlayerAnimator.GetComponent<EntityColorTint>());
        _currentPlayer.Health.Init();

        NotifyCamera();
    }

    public void NotifyCamera()
    {
        OnFollowPlayer?.Invoke(_currentPlayer.transform);
        //AudioManager.Instance.PlayLevelAudio(Level.PLANET);
    }
}

public enum Level
{
    PLANET,
    UNDERGROUND,
}

