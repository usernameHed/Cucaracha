using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeFood : MonoBehaviour, IKillable
{
    [SerializeField]
    private float timeBetweenEat = 0.1f;

    [SerializeField]
    private int lifeFood = 100;

    private int realLifeFood = 100;

    public void Init()
    {
        realLifeFood = lifeFood;
    }

    [SerializeField]
    private Food food;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GameData.Layers.Cucaracha.ToString()) && realLifeFood >= 0)
        {
            CucarachaController cuca = other.gameObject.GetComponent<CucarachaController>();
            if (!cuca)
                cuca = other.gameObject.transform.parent.GetComponent<CucarachaController>();

            //GameData
            if (cuca.Eat())
            {
                realLifeFood--;
                //Debug.Log("life food : " + realLifeFood);
            }

            if (realLifeFood <= 0)
            {
                realLifeFood = 0;
                Kill();
            }
        }

    }

    public void Kill()
    {
        food.Kill();
    }
}
