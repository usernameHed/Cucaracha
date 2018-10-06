using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : SingletonMono<FoodManager>
{
    [SerializeField]
    private List<FoodUI> foodUI;
    [SerializeField]
    private GameObject prefabsFood;

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
            Debug.Log("create food");
            Vector3 pos = Input.mousePosition;
            pos.z = 0;// transform.position.z - Camera.main.transform.position.z;
            pos = GameManager.Instance.CameraMain.ScreenToWorldPoint(pos);
            pos.y = 0;
            GameObject foodObject = Instantiate(prefabsFood, pos, Quaternion.identity, transform);
            Food food = foodObject.GetComponent<Food>();
        }
    }

    // Update is called once per frame
    void Update ()
    {
        HandleFood();
    }
}
