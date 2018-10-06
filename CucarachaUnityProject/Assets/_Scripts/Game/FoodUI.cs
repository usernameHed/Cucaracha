using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;

public class FoodUI : MonoBehaviour
{
    [SerializeField]
    private bool isFilled = true;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Sprite sprite;

    public void Init()
    {
        FillFood(isFilled);
    }

    [Button]
    private void FillFood(bool fill)
    {
        if (fill)
        {
            image.sprite = sprite;
        }
        else
        {
            image.sprite = null;
        }
    }
}
