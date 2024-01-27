using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public class King
{
    Dictionary<JokeType, FunDegree> _jokeReactions = new Dictionary<JokeType, FunDegree>();
    Dictionary<FunDegree, GameObject> _reactionSFX = new Dictionary<FunDegree, GameObject>();

    public King()
    {
        //Shuffles array vvv
        int[] randomNumbers = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        System.Random rng = new System.Random();

        int n = randomNumbers.Length;
        while (n > 1)
        {
            int k = rng.Next(n--);
            int temp = randomNumbers[n];
            randomNumbers[n] = randomNumbers[k];
            randomNumbers[k] = temp;
        }
        //^^^

        _jokeReactions.Add((JokeType)randomNumbers[0], FunDegree.The_Best);
        _jokeReactions.Add((JokeType)randomNumbers[1], FunDegree.The_Best);
        _jokeReactions.Add((JokeType)randomNumbers[2], FunDegree.Good);
        _jokeReactions.Add((JokeType)randomNumbers[3], FunDegree.Good);
        _jokeReactions.Add((JokeType)randomNumbers[4], FunDegree.Bland);
        _jokeReactions.Add((JokeType)randomNumbers[5], FunDegree.Bland);
        _jokeReactions.Add((JokeType)randomNumbers[6], FunDegree.Bad);
        _jokeReactions.Add((JokeType)randomNumbers[7], FunDegree.Bad);
        _jokeReactions.Add((JokeType)randomNumbers[8], FunDegree.The_Worst);
        _jokeReactions.Add((JokeType)randomNumbers[9], FunDegree.The_Worst);

        for (int i = -2; i <= 2; i++)
        {
            FunDegree fun = (FunDegree)i;
            _reactionSFX.Add(fun, Resources.Load<GameObject>("reaction_" + fun.ToString()));
        }
    }

    public FunDegree GetJokeFunDegree(JokeType type) => _jokeReactions[type];
    public GameObject GetReactionSFX(FunDegree fun) => _reactionSFX[fun];
    public GameObject GetReactionSFX(JokeType type) => _reactionSFX[GetJokeFunDegree(type)];
}