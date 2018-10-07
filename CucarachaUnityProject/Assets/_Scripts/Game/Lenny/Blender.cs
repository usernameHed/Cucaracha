using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blender : MonoBehaviour
{
    int juiceQuantity = 0;
    public GameObject Slider;
    private float maximumScore;

    private Slider sliderScript;

    // Use this for initialization
    void Start()
    {
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


            juiceQuantity++;
            ChangeSlider();

            Debug.Log("Juice quantity : " + juiceQuantity);

            cuca.Kill();

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
