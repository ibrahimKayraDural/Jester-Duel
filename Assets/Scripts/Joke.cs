using System;
using UnityEngine;


public enum FunDegree { Worst = -2, Bad = -1, Neutral = 0, Good = 1, Best = 2}
public enum JokeType { King, Queen, Vizier, KingsChildren, DirtyJoke, Citizens, RivalKingdom, Nobles, Military, Economy}
public enum Language { Turkish, English}
[Serializable] public struct LanguagedText { public Language Language; [TextArea(1, 100), Multiline] public string Text; }

[CreateAssetMenu(menuName = "New Joke")]
public class Joke : ScriptableObject
{
    public JokeType JokeType => _JokeType;
    public LanguagedText[] JokeTexts => _JokeTexts;

    [SerializeField] JokeType _JokeType;
    [SerializeField] LanguagedText[] _JokeTexts;
}
