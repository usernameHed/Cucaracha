using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour, IKillable{
  [SerializeField]
  private bool lightActive = true;

  
  private void OnTriggerStay(Collider other)
  {
    if (other.gameObject.CompareTag(GameData.Layers.Cucaracha.ToString()) ) {
      Debug.Log("Cucaracha flees the light");
    }
    
  }

  public void Kill()
  {
    throw new System.NotImplementedException();
  }
}
