using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UI;

[TypeInfoBox("[ILevelLocal] Manage Setup Scene behaviour")]
public class CucarachaManager : SingletonMono<CucarachaManager>, ILevelLocal
{
    [SerializeField]
    private int numberCucarachaLevel = 300;

    [SerializeField, Range(1, 100)]
    private float percentToWin = 80.0f;

    private int juiceQuantity = 0;
    public void AddJuice() { juiceQuantity++; }
    public int GetJuice() { return (juiceQuantity); }

    [SerializeField, ReadOnly]
    private float maximumScore;

    [SerializeField]
    private JuiceGauge juice;

    //[SerializeField]
    //public GameObject Slider;

    [SerializeField]
    private FrequencyTimer frequency;

    private bool isWin = false;
    private bool isLose = false;

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

    //private Slider sliderScript;

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

    public void SetJuice()
    {
        juice.SetValue(GetJuice());
    }

    public void InitScene()
    {
        Debug.Log("INIT Cucaracha manager ! !!");
        cucarachas.Clear();
        FoodManager.Instance.Init();
        spawner.SpawnCuca(numberCucarachaLevel);
        //sliderScript = Slider.GetComponent<Slider>();
        maximumScore = juice.maxInput * (percentToWin / 100);
        isWin = false;
        isLose = false;
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

    public void TestEndLevel()
    {
        if (!TestWin())
            TestLose();
    }

    private bool TestWin()
    {
        if (isWin || isLose)
            return (false);

        if (juiceQuantity >= maximumScore)
        {
            isWin = true;
            //Debug.Log("It's over 9000 ! ");
            EventManager.TriggerEvent(GameData.Event.GameWin);
            return (true);
        }
        return (false);
    }

    private void TestLose()
    {
        if (isLose || isWin)
            return;

        int countCuca = GetCurarachaList().Count;

        if (juiceQuantity + countCuca < maximumScore)
        {
            isLose = true;
            EventManager.TriggerEvent(GameData.Event.GameOver);
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

    private void Update()
    {
        if (frequency.Ready())
        {
            //Debug.Log("launch machine");
            iaManager.Machine();
        }
    }
}
