using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RivalManager : MonoBehaviour
{
    [SerializeField] CardController card;

    GameManager _gameManager;
    JokeDatabase _jokeDatabase;
    Animator _animator;

    void Start()
    {
        _jokeDatabase = JokeDatabase.Instance;
        _gameManager = GameManager.Instance;

        _gameManager.OnRivalsTurn += GM_OnRivalsTurn;
        _animator = card.GetComponent<Animator>();

        card.SetButtonEnablity(false);
    }

    void GM_OnRivalsTurn()
    {
        StartCoroutine(RivalJoke());
    }

    IEnumerator RivalJoke()
    {
        Debug.Log("Enemy is thinking");

        Joke joke = _jokeDatabase.GetAndUseRandomJoke();
        card.SetJoke(joke);

        yield return new WaitForSeconds(1);

        _animator.Play("in", -1, 0);
        card.SetShowCard(true);

        yield return new WaitForSeconds(2);

        //[SFX] rection sound

        FunDegree fun = _gameManager.KingRef.GetReaction(joke.JokeType);

        _gameManager.AddToRivalScore((int)fun);

        string funString = fun.ToString();
        funString.Replace("_", " ");

        Debug.Log("The joke was " + fun + "!");

        yield return new WaitForSeconds(1);

        _animator.Play("out", -1, 0);
        card.SetShowCard(false);

        yield return new WaitForSeconds(1);

        _gameManager.AdvanceGameState();
    }
}
