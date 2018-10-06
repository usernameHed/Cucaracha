using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

/// <summary>
/// Fonctions utile
/// <summary>
public static class GameData
{
    #region core script

    public enum Event
    {
        GameOver,
        GameWin,
        AdditiveJustFinishLoad, //used by SceneManagerGlobal
    };

    public enum Prefabs
    {
        Link,
        Cucaracha,
    };

    public enum PoolTag
    {
        Cucaracha,
        DeathCucaracha,
    }

    public enum Layers
    {
        Cucaracha,
        Wall,
        Default,
        Object,
    }

    public enum Sounds
    {

    }

    /// <summary>
    /// retourne vrai si le layer est dans la list
    /// </summary>
    public static bool IsInList(List<Layers> listLayer, int layer)
    {
        string layerName = LayerMask.LayerToName(layer);
        for (int i = 0; i < listLayer.Count; i++)
        {
            if (listLayer[i].ToString() == layerName)
            {
                return (true);
            }
        }
        return (false);
    }
    /// <summary>
    /// retourne vrai si le layer est dans la list
    /// </summary>
    public static bool IsInList(List<Prefabs> listLayer, string tag)
    {
        for (int i = 0; i < listLayer.Count; i++)
        {
            if (listLayer[i].ToString() == tag)
            {
                return (true);
            }
        }
        return (false);
    }
    #endregion
}
