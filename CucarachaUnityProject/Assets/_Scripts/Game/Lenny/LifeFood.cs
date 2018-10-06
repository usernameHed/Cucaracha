using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeFood : MonoBehaviour, IKillable {
  int lifeFood = 100;
  float timerFood;
	// Use this for initialization
	void Start () {
    timerFood = Time.fixedTime;

  }

  // Update is called once per frame
  void Update () {
   // Debug.Log("Time delta time : " + Time.deltaTime);

  }

  private void OnTriggerStay(Collider collision)
  {

    if (collision.gameObject.CompareTag(GameData.Layers.Cucaracha.ToString())) {

      //GameData
      if ( Time.fixedTime > timerFood + 0.5f ) {
        lifeFood--;
        timerFood = Time.fixedTime + 0.5f;
        Debug.Log("life food : " + lifeFood);

      }


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
