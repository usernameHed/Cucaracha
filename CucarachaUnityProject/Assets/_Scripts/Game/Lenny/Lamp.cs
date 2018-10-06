using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour, IKillable
{
    [SerializeField]
    private bool lightOn = false;

    [SerializeField]
    private GameObject lightObject;

    // Use this for initialization
    private void OnEnable()
    {
        ActiveLight(false);
        CucarachaManager.Instance.AddLamp(this);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GameData.Layers.Cucaracha.ToString()))
        {

        }
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
        Debug.Log("light changed");
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
            }
        }
        else if (!Input.GetMouseButton(0))
        {
            if (lightOn)
            {
                ActiveLight(false);
            }
        }
    }

    private void Update()
    {
        PosMouse();
        InputMouse();
    }

    public void Kill()
    {
        CucarachaManager.Instance.RemoveLamp(this);
    }
}
