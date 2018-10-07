using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCollider : MonoBehaviour
{

    [SerializeField]
    private CucarachaController CucaController;


    private Vector3 CalculateCollision(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;
        Debug.DrawRay(transform.position, normal, Color.red, 1f);

        Vector3 cucaSphere = gameObject.transform.forward;
        Debug.DrawRay(transform.position, cucaSphere, Color.green, 1f);

        Vector3 reflect = Vector3.Reflect(cucaSphere, normal);
        Debug.DrawRay(transform.position, reflect, Color.magenta, 1f);


        Vector3 halfWayVector = (reflect + normal) * 0.5f;
        Debug.DrawRay(transform.position, reflect, Color.blue, 1f);
        
        return (halfWayVector);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag(GameData.Layers.Wall.ToString()))
        {
            CucaController.InvertDirection(CalculateCollision(collision));
            //print(collision.gameObject.tag);
        }
    }
}
