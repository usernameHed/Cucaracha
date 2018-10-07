using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blender : MonoBehaviour
{
    
    //private GameObject Slider;

  AudioSource source;

    public Animator animator;
    //private Slider sliderScript;

    private bool isWinned = false;
    private bool isLoosed = false;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        //Slider = CucarachaManager.Instance.Slider;

        //sliderScript = Slider.GetComponent<Slider>();
        ChangeSlider();
        
        isWinned = false;
        isLoosed = false;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (isWinned || isLoosed)
            return;

        if (collision.gameObject.CompareTag(GameData.Layers.Cucaracha.ToString()))
        {

            CucarachaController cuca = collision.gameObject.GetComponent<CucarachaController>();
            if (!cuca)
                cuca = collision.transform.parent.GetComponent<CucarachaController>();

            if (cuca.IsDying)
                return;
            animator.SetTrigger("RoachCollision");
            CameraShake.Shake(0.1f, 0.05f);

            CucarachaManager.Instance.AddJuice();
            ChangeSlider();
            source.volume = .6f;

            Debug.Log("Juice quantity : " + CucarachaManager.Instance.GetJuice());

            cuca.Kill(false);
        }
    }

    private void Update()
    {
        if(source.volume > 0)
            source.volume -= Time.deltaTime * 2;
    }

    void ChangeSlider()
    {
        CucarachaManager.Instance.SetJuice();
        //sliderScript.value = CucarachaManager.Instance.GetJuice(); //juiceQuantity;
    }
}
