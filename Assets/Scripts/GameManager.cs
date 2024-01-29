using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

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
    public GameObject DrumrollSFX => _DrumrollSFX;

    [SerializeField] int _RoundCount;
    [SerializeField] GameObject _GuillotineSFX;
    [SerializeField] GameObject _DrumrollSFX;
    [SerializeField] GameObject _TrumpetSFX;
    [SerializeField] Transform _RivalExit;
    [SerializeField] Mover _RivalMover;
    [SerializeField] Transform _PlayerExit;
    [SerializeField] Mover _PlayerMover;
    [SerializeField] TextMeshProUGUI _WonTextMesh;

    GameState _state;
    int _playerScore;
    int _rivalScore;
    int _roundsLeft;
    King _king;

    void Awake()
    {
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;

        _king = new King();
        _roundsLeft = _RoundCount;
    }

    public void StartGame() => SetGameState(GameState.PlayerChooses);
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

        Instantiate(_TrumpetSFX);

        yield return new WaitForSeconds(2.8f);

        Instantiate(DrumrollSFX);

        yield return new WaitForSeconds(2.8f);

        //select who won
        bool playerHasWon = _playerScore >= _rivalScore;

        string whoWonTR = playerHasWon ? "Kazandýn" : "Kaybettin";
        string whoWonENG = playerHasWon ? "You Won" : "You Lose";

        if (Prefs.Instance != null)
        {
            switch (Prefs.Instance.LanguagePref)
            {
                case Language.Turkish:
                    _WonTextMesh.text = whoWonTR; break;
                case Language.English:
                    _WonTextMesh.text = whoWonENG; break;
            }
        }

        Debug.Log(whoWonENG);

        yield return new WaitForSeconds(2f);

        Transform targetTransform = playerHasWon ? _RivalExit : _PlayerExit;
        Mover targetMover = playerHasWon ? _RivalMover : _PlayerMover;

        targetMover.TryMove(targetTransform.position, 0.008f, false);

        yield return new WaitForSeconds(1f);

        // other one dies
        Instantiate(_GuillotineSFX);

        yield return new WaitForSeconds(4f);

        Debug.Log("Game ended");

        FindObjectOfType<CurtainController>().CloseCurtain();
    }
}