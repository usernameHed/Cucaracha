using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IKillable
{
    // Use this for initialization
    private void OnEnable()
    {
        CucarachaManager.Instance.AddFood(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameData.Layers.Cucaracha.ToString()))
        {
            CucarachaController cuca = other.gameObject.GetComponentInParent<CucarachaController>();
            if (cuca == null)
            {
                cuca = other.transform.parent.GetComponent<CucarachaController>();
            }

            //if ()
            //Debug.Log("Cucaracha follow the food");
            cuca.SetInsideFood(true, this);
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
        }
    }

    public void Kill()
    {
        CucarachaManager.Instance.RemoveFood(this);
        Destroy(gameObject);
    }
}
