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
    if (collision.gameObject.tag == "Cucaracha" && isFood == true) {
      Debug.Log("Cucaracha follow the food");
    }
    else if (collision.gameObject.tag == "Cucaracha" && isFood == false) {
      Debug.Log("Cucaracha flees the light");
    }
  }
}
