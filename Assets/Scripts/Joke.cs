using UnityEngine;


public enum FunDegree { Worst = -2, Bad = -1, Neutral = 0, Good = 1, Best = 2}
public enum JokeType { King, Queen, Vizier, KingsChildren, DirtyJoke, Citizens, RivalKingdom, Nobles, Military, Economy}

[CreateAssetMenu(menuName = "New Joke")]
public class Joke : ScriptableObject
{
    [SerializeField] JokeType _JokeType;
    [SerializeField, TextArea(1,100), Multiline] string _Joke;
}
