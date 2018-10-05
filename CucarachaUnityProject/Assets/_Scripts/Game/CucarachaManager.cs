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
    /// called by game, or 
    /// </summary>
    public void RestartGame()
    {
        GameManager.Instance.SceneManagerLocal.PlayPrevious();
    }

    /// <summary>
    /// called by game, or 
    /// </summary>
    public void QuitGame()
    {
        GameManager.Instance.SceneManagerLocal.Quit();
    }

    private void NextLevel()
    {
        GameManager.Instance.SceneManagerLocal.PlayNext();
    }

    private void GameOver()
    {

    }

    private void Update()
    {

    }

    private void OnDisable()
    {
        EventManager.StopListening(GameData.Event.GameOver, GameOver);
    }
}
