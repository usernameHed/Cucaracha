using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACucaracha : MonoBehaviour
{
    private float randVal;

    [SerializeField]
    private int state = 0;
    public int State { get { return (state); } set { state = value; } }
}
