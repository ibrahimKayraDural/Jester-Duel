using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState { PlayerChooses, AwaitCardResult, EnemysTurn, GameEnded}
public class GameManager : MonoBehaviour
{
    public event Action OnPlayerChooses;
    public event Action OnPlayerAwait;
    public event Action OnRivalsTurn;

    public static GameManager Instance;
    public GameState State => _state;
    public King KingRef => _king;
    public int RoundsLeft => _roundsLeft;

    [SerializeField] int _RoundCount;

    GameState _state;
    int _playerScore;
    int _rivalScore;
    int _roundsLeft;
    King _king;

    void Awake()
    {
        if (Instance != null) Destroy(gameObject);
        else Instance = this;

        _king = new King();
        _roundsLeft = _RoundCount;
    }

    public void AdvanceGameState()
    {
        switch (_state)
        {
            case GameState.PlayerChooses:
                SetGameState(GameState.AwaitCardResult);
                break;

            case GameState.AwaitCardResult:
                SetGameState(GameState.EnemysTurn); break;

            case GameState.EnemysTurn:
                if (_roundsLeft > 0) SetGameState(GameState.PlayerChooses);
                else SetGameState(GameState.GameEnded);
                break;
        }
    }
    public void SetGameState(GameState setTo)
    {
        _state = setTo;

        switch (_state)
        {
            case GameState.PlayerChooses:
                _roundsLeft--;
                OnPlayerChooses?.Invoke();
                break;

            case GameState.AwaitCardResult:
                OnPlayerAwait?.Invoke();
                break;

            case GameState.EnemysTurn:
                OnRivalsTurn?.Invoke();
                break;
            case GameState.GameEnded:
                StartCoroutine(EndGame());
                break;
        }
    }
    public void AddToPlayerScore(int addition)
    {
        _playerScore += addition;
    }
    public void AddToRivalScore(int addition)
    {
        _rivalScore += addition;
    }

    IEnumerator EndGame()
    {
        Debug.Log("Starting end");

        //[SFX] drumroll

        yield return new WaitForSeconds(3);

        //select who won

        yield return new WaitForSeconds(2);

        // other one dies

        yield return new WaitForSeconds(5);

        // curtain

        yield return new WaitForSeconds(1);

        Debug.Log("Game ended");

        // Open menu scene
    }
}
