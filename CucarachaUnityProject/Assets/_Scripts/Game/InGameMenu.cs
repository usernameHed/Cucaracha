using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour {
	private bool visible = false;

	public GameObject Panel;

	void OnEnable () {
		visible = true;
		Toggle ();
	}

	void Update () {
		if (Panel == null)
			return;
		if (Input.GetButtonDown ("Cancel"))
			Toggle ();
	}

	void Toggle () {
		if (Panel == null)
			return;
		visible = !visible;
		Panel.SetActive (visible);
	}

	public void Menu () {
		GameManager.Instance.SceneManagerLocal.PlayPrevious ();
	}

	public void Restart () {
		GameManager.Instance.SceneManagerLocal.PlayIndex (2);
	}

	public void Resume () {
		Toggle ();
	}

	public void Quit () {
		GameManager.Instance.SceneManagerLocal.Quit ();
	}
}
