using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokeDatabase : MonoBehaviour
{
    public static JokeDatabase Instance;

    public int JokesLeft => UnusedJokes.Count;

    [SerializeField] List<Joke> UnusedJokes;
    List<Joke> UsedJokes = new List<Joke>();

    void Awake()
    {
        if (Instance != null || UnusedJokes.Count <= 0) Destroy(gameObject);
        else Instance = this;
    }

    public Joke GetAndUseRandomJoke()
    {
        if (JokesLeft <= 0)
        {
            Debug.Log("No Joke Left. Reshuffling jokes");
            ReuseJokes();
        }

        int index = Random.Range(0, UnusedJokes.Count);

        Joke joke = UnusedJokes[index];

        UsedJokes.Add(joke);
        UnusedJokes.RemoveAt(index);

        return joke;
    }

    public void ReuseJokes()
    {
        UnusedJokes = UsedJokes;
        UsedJokes = new List<Joke>();
    }
}