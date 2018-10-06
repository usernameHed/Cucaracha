using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeFood : MonoBehaviour, IKillable {
  int lifeFood = 100;
  
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  private void OnTriggerStay(Collider collision)
  {
    if (collision.gameObject.CompareTag(GameData.Layers.Cucaracha.ToString())) {
      //GameData
      lifeFood--;
      Debug.Log("life Food : " + lifeFood);
      if (lifeFood <= 0) {
        lifeFood = 0;
        Kill();

      }
    }

  }

  public void Kill()
  {
    throw new System.NotImplementedException();
  }
}
