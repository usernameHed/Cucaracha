using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour, IKillable
{
    [SerializeField]
    private bool lightActive = true;

    // Use this for initialization
    private void OnEnable()
    {
        CucarachaManager.Instance.AddLamp(this);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GameData.Layers.Cucaracha.ToString()))
        {

        }
    }

    public void Kill()
    {
        CucarachaManager.Instance.RemoveLamp(this);
    }
}
