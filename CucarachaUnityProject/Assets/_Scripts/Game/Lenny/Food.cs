using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour, IKillable{

  // Use this for initialization
  
  private void OnEnable()
  {
    CucarachaManager.Instance.AddFood(this);
  }

  private void OnTriggerEnter(Collider collision)
  {
    if (collision.gameObject.CompareTag(GameData.Layers.Cucaracha.ToString())) {
      //GameData
      CucarachaController cuca = collision.gameObject.GetComponent<CucarachaController>();

      Debug.Log("Cucaracha follow the food");
    }

  }

  

  public void Kill()
  {
    throw new System.NotImplementedException();
  }
}
