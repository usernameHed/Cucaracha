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
		if (Input.GetButtonDown ("Cancel"))
			Toggle ();
	}

	void Toggle () {
		visible = !visible;
		Panel.SetActive (visible);
	}
	
	public void Menu () {
		GameManager.Instance.SceneManagerLocal.PlayPrevious ();
	}

	public void Restart () {
		GameManager.Instance.SceneManagerLocal.PlayNext ();
	}

	public void Resume () {
		Toggle ();
	}

	public void Quit () {
		GameManager.Instance.SceneManagerLocal.Quit ();
	}
}
