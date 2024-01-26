using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokeDatabase : MonoBehaviour
{
    public static JokeDatabase Instance;

    public int JokesLeft => UnusedJokes.Count;
    public int UsedJokeCount => UsedJokes.Count;


    [SerializeField] List<Joke> UnusedJokes;
    List<Joke> UsedJokes;

    void Awake()
    {
        if (Instance != null || UnusedJokes.Count <= 0) Destroy(gameObject);
        else Instance = this;
    }

    public Joke GetAndUseRandomJoke()
    {
        if (JokesLeft <= 0)
        {
            Debug.Log("No Joke Left");
            ReuseJokes();
        }

        int index = Random.Range(0, UnusedJokes.Count);

        Joke joke = UnusedJokes[index];

        UnusedJokes.RemoveAt(index);
        UsedJokes.Add(joke);

        return joke;
    }

    public void ReuseJokes()
    {
        UnusedJokes = UsedJokes;
    }
}