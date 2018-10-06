using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodAndLight : MonoBehaviour {

  public bool isFood;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

   
	}

  private void OnTriggerEnter(Collider collision)
  {
    if (collision.gameObject.CompareTag (GameData.Layers.Cucaracha.ToString()) && isFood == true) {
      //GameData
      Debug.Log("Cucaracha follow the food");
    }
    else if (collision.gameObject.CompareTag(GameData.Layers.Cucaracha.ToString()) && isFood == false) {
      Debug.Log("Cucaracha flees the light");
    }
  }
}
