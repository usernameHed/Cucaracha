using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : SingletonMono<FoodManager>
{
    [SerializeField]
    private List<FoodUI> foodUI;

    public void Init()
    {
        InitUI();
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

        }
    }

    // Update is called once per frame
    void Update ()
    {
        HandleFood();
    }
}
