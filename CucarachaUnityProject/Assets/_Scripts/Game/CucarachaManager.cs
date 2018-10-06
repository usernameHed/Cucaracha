using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

[TypeInfoBox("[ILevelLocal] Manage Setup Scene behaviour")]
public class CucarachaManager : SingletonMono<CucarachaManager>, ILevelLocal
{
    [SerializeField]
    private int numberCucarachaLevel = 300;

    [Space(10)]

    [SerializeField]
    private SpawnPoint spawner;

    [SerializeField, ReadOnly]
    private List<CucarachaController> cucarachas;
    public List<CucarachaController> GetCurarachaList() { return (cucarachas); }

    

    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.GameOver, GameOver);
    }

    public void InitScene()
    {
        Debug.Log("INIT Cucaracha manager ! !!");
        cucarachas = spawner.SpawnToList(numberCucarachaLevel);
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

    [Button]
    public void TriggerGameOver()
    {
        EventManager.TriggerEvent(GameData.Event.GameOver);
    }

    private void GameOver()
    {
        Debug.Log("ici game over !");    
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameData.Event.GameOver, GameOver);
    }
}
