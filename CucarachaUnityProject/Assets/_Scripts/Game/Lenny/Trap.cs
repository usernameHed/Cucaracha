using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(GameData.Layers.Cucaracha.ToString()))
        {
            CucarachaController cuca = collision.gameObject.GetComponent<CucarachaController>();
            if (!cuca)
                cuca = collision.transform.parent.GetComponent<CucarachaController>();

            //Debug.Log("Cucaracha is dead with Trap");
            cuca.Kill();
        }
    }
}
