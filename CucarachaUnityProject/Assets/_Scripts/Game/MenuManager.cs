using Sirenix.OdinInspector;
using UnityEngine;

[TypeInfoBox ("[ILevelLocal] Manage Setup Scene behaviour")]
public class MenuManager : MonoBehaviour, ILevelLocal {

	public GameObject MenuPanel;
	public GameObject CreditsPanel;

	public void InitScene () {
		Debug.Log ("INIT MenuManager !!");
		ShowMenu ();
	}

	public void Play () {
		GameManager.Instance.SceneManagerLocal.PlayNext ();
	}

	public void ShowCredits () {
		CreditsPanel.SetActive (true);
		MenuPanel.SetActive (false);
	}

	public void ShowMenu () {
		MenuPanel.SetActive (true);
		CreditsPanel.SetActive (false);
	}

	public void Quit () {
		GameManager.Instance.SceneManagerLocal.Quit ();
	}
}
