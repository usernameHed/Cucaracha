using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuiceGauge : MonoBehaviour
{

    public float maxInput;

    [Range(0.0f, 1.0f)]
    public float maxOutput;

    public Image liquid;
    public Image bottle;

    private float parentHeight = 100;

    private void OnEnable()
    {
        SetValue(0);
    }

    void OnRectTransformDimensionsChange()
    {
        parentHeight = (liquid.rectTransform.parent as RectTransform).rect.height;
    }

    public void SetValue(float value)
    {
        float normalizedHeight = (value / maxInput) * maxOutput;
        if (normalizedHeight > 1)
            normalizedHeight = 1;
        //Debug.Log (parentHeight + " ; " + normalizedHeight);
        liquid.rectTransform.localPosition = new Vector3(liquid.rectTransform.localPosition.x, normalizedHeight * parentHeight);
    }
}
