using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndManager : MonoBehaviour {

	public Animator LiquidEndAnimator;
	public CanvasGroup GameOverPanel;
	public CanvasGroup NextLevelPanel;

	private void OnEnable () {
		EventManager.StartListening (GameData.Event.GameOver, GameOver);
		EventManager.StartListening (GameData.Event.GameWin, GameWin);

		GameOverPanel.alpha = 0;
		NextLevelPanel.alpha = 0;
        GameOverPanel.blocksRaycasts = false;
        NextLevelPanel.blocksRaycasts = false;
        GameOverPanel.interactable = false;
        NextLevelPanel.interactable = false;
	}

	private void GameOver () {
		Debug.Log ("GAME OVER");
		ShowLiquid (true);
		StartCoroutine (ShowCanvasGroup (GameOverPanel));
	}

	private void GameWin () {
		Debug.Log ("GAME WIN");
		ShowLiquid (true);

		//if (CucarachaManager.Instance.isLastLevel) {
		//NextLevel ();
		//}else 
		{
			StartCoroutine (ShowCanvasGroup (NextLevelPanel));
		}

	}

	private IEnumerator ShowCanvasGroup (CanvasGroup group, float delay = 1.0f) {
		group.interactable = true;
        group.blocksRaycasts = true;
        float start = Time.unscaledTime;
		do {
			yield return null;
			group.alpha = (Time.unscaledTime - start) / delay;
		} while (group.alpha < 1.0f);
	}

	public void Menu () {
		GameManager.Instance.SceneManagerLocal.PlayPrevious ();
	}

	public void Restart () {
		GameManager.Instance.SceneManagerLocal.PlayIndex(2);
	}

	public void NextLevel () {
		GameManager.Instance.SceneManagerLocal.PlayNext();
	}

	public void Quit () {
		GameManager.Instance.SceneManagerLocal.Quit ();
	}

	private void OnDisable () {
		EventManager.StopListening (GameData.Event.GameOver, GameOver);
		EventManager.StopListening (GameData.Event.GameWin, GameWin);
	}

	private void ShowLiquid (bool show) {
		LiquidEndAnimator.SetTrigger (show ? "End" : "Restart");
	}
}
