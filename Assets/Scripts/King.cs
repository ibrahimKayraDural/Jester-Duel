using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime;

public class King
{
    Dictionary<JokeType, FunDegree> JokeReactions = new Dictionary<JokeType, FunDegree>();

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

        JokeReactions.Add((JokeType)randomNumbers[0], FunDegree.The_Best);
        JokeReactions.Add((JokeType)randomNumbers[1], FunDegree.The_Best);
        JokeReactions.Add((JokeType)randomNumbers[2], FunDegree.Good);
        JokeReactions.Add((JokeType)randomNumbers[3], FunDegree.Good);
        JokeReactions.Add((JokeType)randomNumbers[4], FunDegree.Bland);
        JokeReactions.Add((JokeType)randomNumbers[5], FunDegree.Bland);
        JokeReactions.Add((JokeType)randomNumbers[6], FunDegree.Bad);
        JokeReactions.Add((JokeType)randomNumbers[7], FunDegree.Bad);
        JokeReactions.Add((JokeType)randomNumbers[8], FunDegree.The_Worst);
        JokeReactions.Add((JokeType)randomNumbers[9], FunDegree.The_Worst);
    }

    public FunDegree GetReaction(JokeType type) => JokeReactions[type];
}