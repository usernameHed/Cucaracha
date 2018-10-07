﻿using System.Collections;
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

    private void HandleFood()
    {
        if (Input.GetMouseButtonUp(1) && CanAdd())
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
