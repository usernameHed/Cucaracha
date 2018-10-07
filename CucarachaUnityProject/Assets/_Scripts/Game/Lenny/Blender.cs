using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blender : MonoBehaviour
{
    [SerializeField, Range(1, 100)]
    private float percentToWin = 80.0f;

    int juiceQuantity = 0;
    private GameObject Slider;

    [SerializeField, ReadOnly]
    private float maximumScore;

    public Animator animator;
    private Slider sliderScript;

    private bool isWinned = false;
    private bool isLoosed = false;

    // Use this for initialization
    void Start()
    {
        Slider = CucarachaManager.Instance.Slider;

        sliderScript = Slider.GetComponent<Slider>();
        ChangeSlider();
        maximumScore = sliderScript.maxValue * (percentToWin / 100);
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


            juiceQuantity++;
            ChangeSlider();

            Debug.Log("Juice quantity : " + juiceQuantity);

            cuca.Kill(false);


            TestWin();
            TestLose();
        }
    }

    private void TestWin()
    {
        if (isLoosed)
            return;

        if (juiceQuantity >= maximumScore)
        {
            Debug.Log("It's over 9000 ! ");
            isWinned = true;
            EventManager.TriggerEvent(GameData.Event.GameWin);
        }
    }

    private void TestLose()
    {
        if (isWinned)
            return;

        int countCuca = CucarachaManager.Instance.GetCurarachaList().Count;

        if (sliderScript.value + countCuca < maximumScore)
        {
            isLoosed = true;
            EventManager.TriggerEvent(GameData.Event.GameOver);
            
        }
    }

    private void ChangeSlider()
    {
        sliderScript.value = juiceQuantity;
    }
}
