using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour {

    [SerializeField]
    CucarachaController CucaController;


    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall")
        {
            CucaController.InvertDirection();
            //print(collision.gameObject.tag);
        }
    }
}
