using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IKillable
{
    private List<CucarachaController> cucaInside = new List<CucarachaController>();

    // Use this for initialization
    private void OnEnable()
    {
        CucarachaManager.Instance.AddFood(this);
        cucaInside.Clear();
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
        CucarachaManager.Instance.RemoveFood(this);
        ResetCuca();
        Destroy(gameObject);
    }
}
