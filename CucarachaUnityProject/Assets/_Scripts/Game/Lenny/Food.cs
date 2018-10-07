using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IKillable, IPooledObject
{
    public bool addedFromLevel = false;

    [SerializeField]
    private LifeFood life;



    private List<CucarachaController> cucaInside = new List<CucarachaController>();


    public bool isDeadCuca = true;

    public float weight = 1.0f;

    public bool isKilled = false;

    public void OnObjectSpawn()
    {
        Init();
    }

    private void Init()
    {
        gameObject.SetActive(true);
        life.Init();
        Debug.Log("spawn: " + gameObject.name);
        isKilled = false;

        if (!isDeadCuca)
            FoodManager.Instance.AddOne(this);

        CucarachaManager.Instance.AddFood(this);
        cucaInside.Clear();
    }

    // Use this for initialization
    private void Start()
    {
        if (addedFromLevel)
        {
            Init();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GameData.Layers.Cucaracha.ToString()))
        {
            CucarachaController cuca = other.gameObject.GetComponentInParent<CucarachaController>();
            if (cuca == null)
            {
                cuca = other.transform.parent.GetComponent<CucarachaController>();
            }

            if (cuca.isInsideFood)
                return;

            //if ()
            //Debug.Log("Cucaracha follow the food");
            cuca.SetInsideFood(true, this);
            if (!cucaInside.Contains(cuca))
                cucaInside.Add(cuca);
        }
    }

    /// <summary>
    /// when cuca exit the food (OR FOOD is destroyed ??)
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(GameData.Layers.Cucaracha.ToString()))
        {
            CucarachaController cuca = other.gameObject.GetComponent<CucarachaController>();
            if (cuca == null)
            {
                cuca = other.transform.parent.GetComponent<CucarachaController>();
            }
            cuca.SetInsideFood(false, null);
            cucaInside.Remove(cuca);
        }
    }

    private void ResetCuca()
    {
        for (int i = 0; i < cucaInside.Count; i++)
        {
            cucaInside[i].SetInsideFood(false, null);
        }
        cucaInside.Clear();
    }

    [Button]
    public void Kill()
    {
        if (isKilled)
            return;

        isKilled = true;
        CucarachaManager.Instance.RemoveFood(this);
        ResetCuca();

        if (!isDeadCuca)
            FoodManager.Instance.DeleteOne(this);

        transform.SetParent(ObjectsPooler.Instance.transform);
        //Destroy(gameObject);
        Debug.Log("ici desactive !");
        gameObject.SetActive(false);
    }

    public void OnDesactivePool()
    {
        
    }
}
