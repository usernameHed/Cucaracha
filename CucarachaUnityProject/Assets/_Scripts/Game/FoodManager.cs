using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : SingletonMono<FoodManager>
{
    [SerializeField, ReadOnly]
    private List<FoodUI> foodUI = new List<FoodUI>();
    [SerializeField]
    private GameObject prefabsFood;
    [SerializeField]
    private GameObject prefabsUI;

    [SerializeField]
    private GameObject parentFoodUI;

    [SerializeField]
    private int foodNumber = 5;

    [SerializeField, ReadOnly]
    private List<Food> foodList = new List<Food>();

    [SerializeField]
    AudioSource m_sourceOk, m_sourceError;

    public void Init()
    {
        foodUI.Clear();
        parentFoodUI.transform.ClearChild();
        for (int i = 0; i < foodNumber; i++)
        {
            GameObject ui = Instantiate(prefabsUI, parentFoodUI.transform);
            foodUI.Add(ui.GetComponent<FoodUI>());
        }

        InitUI();
    }

    public void AddOne(Food food)
    {
        if (!foodList.Contains(food))
        {
            foodList.Add(food);
            InitUI();
        }

    }
    public void DeleteOne(Food food)
    {
        foodList.Remove(food);
        InitUI();
    }

    private bool CanAdd()
    {
        return ((foodList.Count != foodUI.Count));
    }

    /// <summary>
    /// init UI
    /// </summary>
    private void InitUI()
    {
        for (int i = 0; i < foodUI.Count; i++)
        {
            foodUI[i].Init();
        }
        List<Food> tmpList = new List<Food>(foodList);
        for (int i = foodUI.Count - 1; i >= 0; i--)
        {
            if (tmpList.Count > 0)
            {
                tmpList.RemoveAt(0);
                foodUI[i].FillFood(false);
            }
        }
    }

    private GameObject CheckForObjectUnderMouse()
    {
        //Vector2 touchPostion = GameManager.Instance.CameraMain.ScreenToWorldPoint(Input.mousePosition);
        Ray ray = GameManager.Instance.CameraMain.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Ground", "Wall", "Object")))
        {
            return (hit.transform.gameObject);
        }
        return (null);
    }

    private bool CleanClick()
    {
        GameObject obj = CheckForObjectUnderMouse();
        if (!obj)
            return (false);

        Debug.Log(obj.name);
        if (obj.name == "Plane" || obj.CompareTag(GameData.Prefabs.Cucaracha.ToString()))
        {
            return (true);
        }
        return (false);
    }

    private void HandleFood()
    {
        if (Input.GetMouseButtonUp(1) && CleanClick())
        {
            if(CanAdd())
            {
                Debug.Log("create food");
                Vector3 pos = Input.mousePosition;
                pos.z = 0;// transform.position.z - Camera.main.transform.position.z;
                pos = GameManager.Instance.CameraMain.ScreenToWorldPoint(pos);
                pos.y = 0;
                //GameObject foodObject = Instantiate(prefabsFood, pos, Quaternion.identity, transform);
                GameObject foodObject = ObjectsPooler.Instance.SpawnFromPool(GameData.PoolTag.Food, pos, Quaternion.identity, transform);
        
                Food food = foodObject.GetComponent<Food>();
                food.addedFromLevel = false;

                StartCoroutine(MoveFood(foodObject));
                m_sourceOk.Play();
            }
            else
            {
                m_sourceError.Play();
            }
        }        
    }

    private IEnumerator MoveFood(GameObject food)
    {
        yield return new WaitForEndOfFrame();
        food.transform.position = new Vector3(food.transform.position.x, food.transform.position.y + 0.001f, food.transform.position.z);
    }

    // Update is called once per frame
    void Update ()
    {
        HandleFood();
    }
}
