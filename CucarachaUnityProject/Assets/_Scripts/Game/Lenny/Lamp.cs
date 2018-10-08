using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour, IKillable
{
    [SerializeField]
    private bool lightOn = false;

    public float weight = 1f;

    [SerializeField]
    private GameObject lightObject;

    private List<CucarachaController> cucaInside = new List<CucarachaController>();

    [SerializeField]
    AudioSource m_sourceOn, m_sourceOff;

    private bool enableScript = true;

    // Use this for initialization
    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.GameWin, DesactiveScript);
        EventManager.StartListening(GameData.Event.GameOver, DesactiveScript);

        ActiveLight(false);
        CucarachaManager.Instance.AddLamp(this);
        cucaInside.Clear();
        enableScript = true;
    }

    private void DesactiveScript()
    {
        enableScript = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GameData.Layers.Cucaracha.ToString()) && lightOn)
        {
            CucarachaController cuca = other.gameObject.GetComponent<CucarachaController>();
            if (cuca == null)
            {
                cuca = other.transform.parent.GetComponent<CucarachaController>();
            }
            cuca.SetInsideLamp(true, this);
            if (!cucaInside.Contains(cuca))
                cucaInside.Add(cuca);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(GameData.Layers.Cucaracha.ToString()) && lightOn)
        {
            CucarachaController cuca = other.gameObject.GetComponent<CucarachaController>();
            if (cuca == null)
            {
                cuca = other.transform.parent.GetComponent<CucarachaController>();
            }
            cuca.SetInsideLamp(false, null);
            cucaInside.Remove(cuca);
        }
    }

    private void ResetCuca()
    {
        for (int i = 0; i < cucaInside.Count; i++)
        {
            cucaInside[i].SetInsideLamp(false, null);
        }
        cucaInside.Clear();
    }

    /// <summary>
    /// set to mouse position
    /// </summary>
    private void PosMouse()
    {
        Vector3 pos = Input.mousePosition;
        pos.z = 0;// transform.position.z - Camera.main.transform.position.z;
        pos = GameManager.Instance.CameraMain.ScreenToWorldPoint(pos);
        transform.position = new Vector3(pos.x, 0, pos.z);
    }

    private void ActiveLight(bool active)
    {
        lightOn = active;
        if (!active)
            ResetCuca();
        //Debug.Log("light changed");
        lightObject.SetActive(active);
    }

    /// <summary>
    /// test input mouse
    /// </summary>
    private void InputMouse()
    {
        if (Input.GetMouseButton(0))
        {
            if (!lightOn)
            {
                ActiveLight(true);
                m_sourceOn.Play();
            }
        }
        else if (!Input.GetMouseButton(0))
        {
            if (lightOn)
            {
                ActiveLight(false);
                m_sourceOff.Play();
            }
        }
    }

    private void Update()
    {
        if (!enableScript || CucarachaManager.Instance.gamePaused)
            return;

        PosMouse();
        InputMouse();
    }

    public void Kill()
    {
        CucarachaManager.Instance.RemoveLamp(this);
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameData.Event.GameWin, DesactiveScript);
        EventManager.StopListening(GameData.Event.GameOver, DesactiveScript);
    }
}
