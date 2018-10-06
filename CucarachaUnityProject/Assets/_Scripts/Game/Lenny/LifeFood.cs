using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeFood : MonoBehaviour, IKillable {
  int lifeFood = 100;
  float timerFood;
	// Use this for initialization
	void Start () {
    timerFood = Time.deltaTime;

  }

  // Update is called once per frame
  void Update () {
    Debug.Log("timer Food : " + timerFood);

  }

  private void OnTriggerStay(Collider collision)
  {

    if (collision.gameObject.CompareTag(GameData.Layers.Cucaracha.ToString())) {

      //GameData
      if ( Time.deltaTime > timerFood + 0.5f ) {
        lifeFood--;
        timerFood = Time.deltaTime +0.5f;
        Debug.Log("life Food : " + lifeFood);

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
