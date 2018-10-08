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

    float soundVolume = 0;

    public void Init()
    {
        realLifeFood = lifeFood;
    }

    [SerializeField]
    private Food food;

    [SerializeField]
    AudioSource audio;

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
                soundVolume = .5f;
                realLifeFood--;
                if (Random.Range(0, 10) < 1)
                {
                    GameObject bump = ObjectsPooler.Instance.SpawnFromPool(GameData.PoolTag.Bump, transform.position, Quaternion.identity, ObjectsPooler.Instance.transform);
                }
                    
                //Debug.Log("life food : " + realLifeFood);
            }

            if (realLifeFood <= 0)
            {
                realLifeFood = 0;
                Kill();
            }
        }

    }

    private void Update()
    {
        if(soundVolume > 0)
            soundVolume -= Time.deltaTime;

        audio.volume = soundVolume;
    }

    public void Kill()
    {
        food.Kill();
    }
}
