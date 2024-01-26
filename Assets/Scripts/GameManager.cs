using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState { PlayerChooses, AwaitCardResult, EnemysTurn}
public class GameManager : MonoBehaviour
{
    public event Action OnPlayerChooses;
    public event Action OnPlayerAwait;

    public static GameManager Instance;
    public GameState State => _state;

    GameState _state;


    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;
    }

    public void AdvanceGameState()
    {
        switch (_state)
        {
            case GameState.PlayerChooses:
                SetGameState(GameState.AwaitCardResult); break;

            case GameState.AwaitCardResult:
                SetGameState(GameState.EnemysTurn); break;

            case GameState.EnemysTurn:
                SetGameState(GameState.PlayerChooses); break;
        }
    }
    public void SetGameState(GameState setTo)
    {
        switch (setTo)
        {
            case GameState.PlayerChooses:
                _state = GameState.PlayerChooses;
                OnPlayerChooses?.Invoke();
                break;

            case GameState.AwaitCardResult:
                _state = GameState.AwaitCardResult;
                OnPlayerAwait?.Invoke();
                break;

            case GameState.EnemysTurn:
                _state = GameState.EnemysTurn;
                break;
        }
    }
}
