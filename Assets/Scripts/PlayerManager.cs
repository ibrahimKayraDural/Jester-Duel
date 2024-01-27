using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] CardController[] cardControllers;
    [Min(0)] public float CardIterationWaitSeconds = .008f;
    public Language CardLanguage;

    GameManager _gameManager;
    CardController _currentCard;
    JokeDatabase _jokeDatabase;

    void Start()
    {
        _jokeDatabase = JokeDatabase.Instance;

        _gameManager = GameManager.Instance;
        _gameManager.OnPlayerAwait += GM_OnPlayerAwait;
        _gameManager.OnPlayerChooses += GM_OnPlayerChooses;
        _gameManager.OnRivalsTurn += GM_OnEnemysTurn;

        SetCardValues();

       StartCoroutine(StartGame());

        //for (int i = 0; i < 10; i++)
        //{
        //    Debug.Log(_king.GetReaction((JokeType)i));
        //}
    }

    IEnumerator StartGame()
    {


        yield return new WaitForSeconds(1);

        _gameManager.SetGameState(GameState.PlayerChooses);
    }

    public void ChooseCard(CardController card)
    {
        Debug.Log(card.gameObject.name + " has been chosen.");

        SelectOnlyOneCard(card);
        card.SetButtonEnablity(false);
        SetShownnesOfAllCardsButOne(false, card);

        _currentCard = card;

        _gameManager.AdvanceGameState();
    }

    private void GM_OnEnemysTurn()
    {
        SetEnablityOfAllCards(false);
        SetShownnesOfAllCards(false);
    }

    void GM_OnPlayerAwait()
    {
        StartCoroutine(AwaitCardReaction());
    }
    public IEnumerator AwaitCardReaction()
    {
        
        //[SFX] drumroll

        yield return new WaitForSeconds(2);

        //[SFX] reaction sound

        Joke joke = _currentCard.CurrentJoke;
        FunDegree fun = _gameManager.KingRef.GetReaction(joke.JokeType);

        _gameManager.AddToPlayerScore((int)fun);

        string funString = fun.ToString();
        funString.Replace("_"," ");

        Debug.Log("The joke was " + fun + "!");


        yield return new WaitForSeconds(1);


        _gameManager.AdvanceGameState();
    }

    void SetCardValues()
    {
        foreach (CardController c in cardControllers)
        {
            c.SetIterationSpeed(CardIterationWaitSeconds);
            c.SetLanguage(CardLanguage);
        }
    }
    void GM_OnPlayerChooses()
    {
        SetNewJokesForAllCards();
        SetEnablityOfAllCards(true);
        SetShownnesOfAllCards(true);
    }
    void SetNewJokesForAllCards()
    {
        foreach (CardController c in cardControllers)
        {
            c.SetJoke(_jokeDatabase.GetAndUseRandomJoke());
        }
    }
    void SetShownnesOfAllCards(bool setTo)
    {
        foreach (CardController c in cardControllers)
        {
            c.SetShowCard(setTo);
        }
    }
    void SetShownnesOfAllCardsButOne(bool setTo, CardController card)
    {
        foreach (CardController c in cardControllers)
        {
            if(c != card) c.SetShowCard(setTo);
        }
    }
    void SetEnablityOfAllCards(bool setTo)
    {
        foreach (CardController c in cardControllers)
        {
            if (c.gameObject.TryGetComponent(out Button button))
            {
                button.interactable = setTo;
                c.SetButtonEnablity(true);
            }
        }
    }
    void SelectOnlyOneCard(CardController cardToSelect)
    {
        foreach (CardController c in cardControllers)
        {
            if (c.gameObject.TryGetComponent(out Button button))
            {
                bool setTo = c == cardToSelect;
                button.interactable = setTo;
            }
        }
    }
}
