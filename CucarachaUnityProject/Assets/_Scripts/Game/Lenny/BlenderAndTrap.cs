using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlenderAndTrap : MonoBehaviour {
  public bool isBlender;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  private void OnTriggerEnter(Collider collision)
  {
    if (collision.gameObject.tag == "Cucaracha" && isBlender == true) {
      Debug.Log("Cucaracha is dead with Blender");
    }
    else if (collision.gameObject.tag == "Cucaracha" && isBlender == false) {
      Debug.Log("Cucaracha is dead with trap");
    }
  }
}
