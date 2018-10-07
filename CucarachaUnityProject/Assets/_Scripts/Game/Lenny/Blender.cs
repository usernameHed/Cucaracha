using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blender : MonoBehaviour
{
  int juiceQuantity = 0;
  private GameObject Slider;
  private float maximumScore;
  public Animator animator;
  private Slider sliderScript;

    // Use this for initialization
    void Start()
    {
        Slider = CucarachaManager.Instance.Slider;

        sliderScript = Slider.GetComponent<Slider>();
        ChangeSlider();
        maximumScore = sliderScript.maxValue;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag(GameData.Layers.Cucaracha.ToString()))
        {

            CucarachaController cuca = collision.gameObject.GetComponent<CucarachaController>();
            if (!cuca)
                cuca = collision.transform.parent.GetComponent<CucarachaController>();

            if (cuca.IsDying)
                return;
            animator.SetTrigger("RoachCollision");
            CameraShake.Shake(0.1f, 0.05f);
            //GameManager.Instance.CameraObject.GetComponent<CameraShake>().cShake(1.0f, 1.0f);

            juiceQuantity++;
            ChangeSlider();

            Debug.Log("Juice quantity : " + juiceQuantity);

            cuca.Kill(false);

            if (juiceQuantity > maximumScore)
            {
                Debug.Log("It's over 9000 ! ");
            }

        }
    }

    void ChangeSlider()
    {
        sliderScript.value = juiceQuantity;
    }
}
