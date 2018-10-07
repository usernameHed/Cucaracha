using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndManager : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.GameOver, GameOver);
        EventManager.StartListening(GameData.Event.GameWin, GameWin);
    }

    private void GameOver()
    {
        Debug.Log("GAME OVER");
    }

    private void GameWin()
    {
        Debug.Log("GAME WIN");
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameData.Event.GameOver, GameOver);
        EventManager.StopListening(GameData.Event.GameWin, GameWin);
    }
}
