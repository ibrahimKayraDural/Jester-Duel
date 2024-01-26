using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] CardController[] cardControllers;

    GameManager _gameManager;
    CardController _currentCard;
    King _king;
    JokeDatabase _jokeDatabase;
    int _playerScore;
    int _rivalScore;

    void Start()
    {
        _king = new King();
        _gameManager = GameManager.Instance;
        _gameManager.OnPlayerAwait += GM_OnPlayerAwait;
        _gameManager.OnPlayerChooses += GM_OnPlayerChooses; ;

        //for (int i = 0; i < 10; i++)
        //{
        //    Debug.Log(_king.GetReaction((JokeType)i));
        //}
    }

    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Space))
        {
            _gameManager.SetGameState(GameState.PlayerChooses);
        }
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

    void GM_OnPlayerAwait()
    {
        StartCoroutine(AwaitCardReaction());
    }
    public IEnumerator AwaitCardReaction()
    {
        

        yield return new WaitForSeconds(2);

        string fun = _currentCard.CurrentJoke.ToString();
        fun.Replace('_', ' ');

        Debug.Log("The joke was " + fun + "!");
        yield return new WaitForSeconds(1);

        _gameManager.AdvanceGameState();
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
