using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

[TypeInfoBox("[ILevelLocal] Manage Setup Scene behaviour")]
public class CucarachaManager : SingletonMono<CucarachaManager>, ILevelLocal
{

    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.GameOver, GameOver);
    }


    public void InitScene()
    {
        Debug.Log("INIT Cucaracha manager ! !!");
    }

    /// <summary>
    /// called by game
    /// </summary>
    public void Back()
    {
        GameManager.Instance.SceneManagerLocal.PlayPrevious();
    }

    /// <summary>
    /// restart scene with fade
    /// </summary>
    public void Restart()
    {
        GameManager.Instance.SceneManagerLocal.PlayNext();
    }

    private void GameOver()
    {
        
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameData.Event.GameOver, GameOver);
    }
}
