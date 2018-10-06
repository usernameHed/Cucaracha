using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACucaracha : MonoBehaviour
{
    private float randVal;

    [SerializeField]
    private int state = 0;
    public int State { get { return (state); } set { state = value; } }

    [SerializeField]
    private float sinVal = 0;
    public float sinValue { get { return (sinVal); } set { sinVal = value; } }

    
    public void Init()
    {
        sinVal = Random.Range(0.0f, 2 * Mathf.PI);
    }


    private void Start()
    {
        Init();
    }
}
