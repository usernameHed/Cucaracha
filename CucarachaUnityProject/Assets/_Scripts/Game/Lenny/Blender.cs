using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blender : MonoBehaviour
{
  int juiceQuantity = 0;
  public GameObject Slider;
  private float maximumScore;
  public Animator animator;
  // Use this for initialization
  void Start()
  {
    changeSlider();
    maximumScore = Slider.GetComponent<Slider>().maxValue;
    
  }

  // Update is called once per frame
  void Update()
  {

  }
  private void OnTriggerEnter(Collider collision)
  {
    if (collision.gameObject.CompareTag(GameData.Layers.Cucaracha.ToString())) {
      animator.SetTrigger("RoachCollision");
      juiceQuantity++;
      changeSlider();

      Debug.Log("Juice quantity : " + juiceQuantity);



      if (juiceQuantity > maximumScore) {
        Debug.Log("It's over 9000 ! ");

      }


      Debug.Log("Cucaracha is dead with Blender");
    }

  }

  void changeSlider()
  {
    Slider.GetComponent<Slider>().value = juiceQuantity;

  }
}
