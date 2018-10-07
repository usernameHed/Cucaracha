using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : SingletonMono<FoodManager>
{
    [SerializeField]
    private List<FoodUI> foodUI;
    [SerializeField]
    private GameObject prefabsFood;

    [SerializeField]
    private List<Food> foodList = new List<Food>();

    public void Init()
    {
        InitUI();
    }

    public void AddOne()
    {

    }
    public void DeleteOne()
    {

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
    }

    private void HandleFood()
    {
        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("create food");
            Vector3 pos = Input.mousePosition;
            pos.z = 0;// transform.position.z - Camera.main.transform.position.z;
            pos = GameManager.Instance.CameraMain.ScreenToWorldPoint(pos);
            pos.y = 0;
            //GameObject foodObject = Instantiate(prefabsFood, pos, Quaternion.identity, transform);
            GameObject foodObject = ObjectsPooler.Instance.SpawnFromPool(GameData.PoolTag.Food, pos, Quaternion.identity, transform);

            Food food = foodObject.GetComponent<Food>();
            StartCoroutine(MoveFood(foodObject));
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
