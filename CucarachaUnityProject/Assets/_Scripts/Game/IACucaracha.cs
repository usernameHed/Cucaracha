using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACucaracha : MonoBehaviour
{
    [SerializeField]
    private CucarachaController cuca;
    public CucarachaController GetCucarachaController() { return (cuca); }

    private float randVal;

    [SerializeField]
    private int state = 0;
    public int State { get { return (state); } set { state = value; } }
}
