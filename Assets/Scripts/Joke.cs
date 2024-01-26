using UnityEngine;


public enum FunDegree { Worst = -2, Bad = -1, Neutral = 0, Good = 1, Best = 2}
public enum JokeType { King, Queen, Vizier, KingsChildren, DirtyJoke, Citizens, RivalKingdom, Nobles, Military, Economy}

[CreateAssetMenu(menuName = "New Joke")]
public class Joke : ScriptableObject
{
    public JokeType JokeType => _JokeType;
    public string JokeText_Turkish => _JokeText_Turkish;
    public string JokeText_English => _JokeText_English;

    [SerializeField] JokeType _JokeType;
    [SerializeField, TextArea(1,100), Multiline] string _JokeText_Turkish;
    [SerializeField, TextArea(1,100), Multiline] string _JokeText_English;
}
