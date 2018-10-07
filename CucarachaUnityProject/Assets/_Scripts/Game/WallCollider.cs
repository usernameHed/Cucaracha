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

        return (normal);
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
