using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JuiceGauge : MonoBehaviour {

	public float maxInput;

	[Range (0.0f, 1.0f)]
	public float maxOutput;

	public Image liquid;
	public Image bottle;

	private float parentHeight = 100;

	void Start () {
		SetValue (0.0f);
	}

	void OnRectTransformDimensionsChange () {
		parentHeight = (liquid.rectTransform.parent as RectTransform).rect.height;
	}

	public void SetValue (float value) {
		float normalizedHeight = (value / maxInput) * maxOutput;
		//Debug.Log (parentHeight + " ; " + normalizedHeight);
		liquid.rectTransform.localPosition = new Vector3 (liquid.rectTransform.localPosition.x, normalizedHeight * parentHeight);
	}
}
