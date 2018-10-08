using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class EndManager : MonoBehaviour {

	public Animator LiquidEndAnimator;
	public CanvasGroup GameOverPanel;
	public CanvasGroup NextLevelPanel;

	[SerializeField]
	AudioMixerSnapshot m_audioOn, m_audioOff;

	[SerializeField]
	AudioClip m_clipLoose, m_clipWin;

	[SerializeField]
	AudioSource m_source;

	private void OnEnable () {
        m_audioOn.TransitionTo(1);
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
		m_source.clip = m_clipLoose;
		m_source.Play();
        m_audioOff.TransitionTo(3);
    }

	private void GameWin () {
		Debug.Log ("GAME WIN");
		ShowLiquid (true);

		m_source.clip = m_clipWin;
		m_source.Play();

		//if (CucarachaManager.Instance.isLastLevel) {
		//NextLevel ();
		//}else 
		{
			StartCoroutine (ShowCanvasGroup (NextLevelPanel));
		}
        m_audioOff.TransitionTo(3);
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
        m_audioOn.TransitionTo(1);
        GameManager.Instance.SceneManagerLocal.PlayPrevious ();
	}

	public void Restart () {
        m_audioOn.TransitionTo(1);
        GameManager.Instance.SceneManagerLocal.PlayIndex(2);
	}

	public void NextLevel () {
        m_audioOn.TransitionTo(1);
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
