using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
  private void OnTriggerEnter(Collider collision)
  {
    if (collision.gameObject.CompareTag(GameData.Layers.Cucaracha.ToString())) {
      Debug.Log("Cucaracha is dead with Blender");
    }
    
  }
}
