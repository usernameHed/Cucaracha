using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingStuff : MonoBehaviour {

	void Start () {
		foreach (Animator a in gameObject.GetComponentsInChildren<Animator> ()) {
			a.SetFloat ("Offset", Random.Range (0.0f, 1.0f));
			a.SetFloat ("SpeedMultiplier", Random.Range (0.8f, 1.2f));
		}
		Destroy (this);
	}
}
