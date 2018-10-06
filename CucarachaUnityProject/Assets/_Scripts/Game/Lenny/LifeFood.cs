using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeFood : MonoBehaviour, IKillable
{
    [SerializeField]
    private float timeBetweenEat = 0.1f;

    int lifeFood = 100;

    [SerializeField]
    private Food food;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GameData.Layers.Cucaracha.ToString()) && lifeFood > 0)
        {
            CucarachaController cuca = other.gameObject.GetComponent<CucarachaController>();
            if (!cuca)
                cuca = other.gameObject.transform.parent.GetComponent<CucarachaController>();

            //GameData
            if (cuca.Eat())
            {
                lifeFood--;
                //Debug.Log("life food : " + lifeFood);
            }

            if (lifeFood <= 0)
            {
                lifeFood = 0;
                Kill();
            }
        }

    }

    public void Kill()
    {
        food.Kill();
    }
}
