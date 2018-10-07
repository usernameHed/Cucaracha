using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

[TypeInfoBox("[ILevelLocal] Manage Setup Scene behaviour")]
public class CucarachaManager : SingletonMono<CucarachaManager>, ILevelLocal
{
    [SerializeField]
    private int numberCucarachaLevel = 300;

    [SerializeField]
    public GameObject Slider;

    [SerializeField]
    private FrequencyTimer frequency;

    [Space(10)]

    [SerializeField]
    private SpawnPoint spawner;

    [SerializeField, ReadOnly]
    private List<CucarachaController> cucarachas = new List<CucarachaController>();
    public List<CucarachaController> GetCurarachaList() { return (cucarachas); }

    [SerializeField]
    private IACucaManager iaManager;

    [SerializeField, ReadOnly]
    private List<Food> food = new List<Food>();

    [SerializeField, ReadOnly]
    private List<Lamp> lamp = new List<Lamp>();

    AudioSource m_audioSource;

    private void Start()
    {
        m_audioSource = GetComponent<AudioSource>();
    }

    public void AddCucaracha(CucarachaController cuca)
    {
        if (!cucarachas.Contains(cuca))
            cucarachas.Add(cuca);
    }

    public void RemoveCuca(CucarachaController cuca)
    {
        cucarachas.Remove(cuca);
    }
    

    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.GameOver, GameOver);
    }

    public void AddFood(Food item)
    {
        if (!food.Contains(item))
          food.Add(item);
    }
    public void RemoveFood(Food item)
    {
        food.Remove(item);
    }
    public void AddLamp(Lamp item)
    {
        if (!lamp.Contains(item))
            lamp.Add(item);
    }
    public void RemoveLamp(Lamp item)
    {
        lamp.Remove(item);
    }

    public void InitScene()
    {
        Debug.Log("INIT Cucaracha manager ! !!");
        cucarachas.Clear();
        FoodManager.Instance.Init();
        spawner.SpawnCuca(numberCucarachaLevel);
    }

    [Button]
    public void ChangeDirectionCuca()
    {
        for (int i = 0; i < cucarachas.Count; i++)
        {
            CucarachaController cuca = cucarachas[i];
            //cuca.GetIA().
            Vector2 dir = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            cuca.ChangeDirectionIA(dir);
        }
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

    float GetGlobalVel()
    {
        float result = 0;

        foreach (CucarachaController c in cucarachas)
        {
            result += c.rb.velocity.magnitude;
        }

        return result;
    }

    private void Update()
    {
        if (frequency.Ready())
        {
            //Debug.Log("launch machine");
            iaManager.Machine();

            m_audioSource.volume = 0.1f + GetGlobalVel() / 50;
            m_audioSource.pitch = Mathf.Min(0.5f + GetGlobalVel() / 50, 1);
        }
    }
}
