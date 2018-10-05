using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManagerLocal : MonoBehaviour, ILevelManager
{
    public GameObject iLevelLocal;

    private ILevelLocal levelManger;

    public void InitScene()
    {
        if (iLevelLocal)
            levelManger = iLevelLocal.GetComponent<ILevelLocal>();
        if (levelManger != null)
            levelManger.InitScene();
    }

    public void InputLevel()
    {
        //Debug.Log("nothing for this scene");
    }

    public void Play()
    {
        //Debug.Log("play next");
        GameManager.Instance.SceneManagerLocal.PlayNext();
    }

    public void Previous()
    {
        GameManager.Instance.SceneManagerLocal.PlayPrevious();
    }

    public void PlayIndex(int index)
    {
        //Debug.Log("nothing for this scene");
    }
}
