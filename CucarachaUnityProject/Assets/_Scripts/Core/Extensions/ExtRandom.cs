using UnityEngine;
using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;

/// <summary>
/// Fonctions utile
/// <summary>
public static class ExtRandom
{
    #region core script
    
    /// <summary>
    /// return an nams
    /// </summary>
    /// <returns></returns>
    public static string GetRandomName()
    {
        return (RandomNameGenerator.Instance.GetNames());
    }

    /// <summary>
    /// get random number between 2;
    /// </summary>
    public static int GetRandomNumber(int minimum, int maximum)
    {
        System.Random random = new System.Random();
        return random.Next() * (maximum - minimum) + minimum;
    }

    /// <summary>
    /// return 1 or -1
    /// </summary>
    /// <returns></returns>
    public static int RandomNegative()
    {
        int negative = UnityEngine.Random.Range(0, 2);
        if (negative == 0)
            return (-1);
        return (1);
    }
    #endregion
}
