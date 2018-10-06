﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucarachaController : MonoBehaviour, IPooledObject, IKillable
{
    [SerializeField]
    private float speedPlayer = 5;          //speed of player
    [SerializeField]
    private float rotationSpeed = 5;          //rotation speed of player

    [Space(10)]

    [SerializeField]
    private Rigidbody rb;                   //link to rigidbody
    [SerializeField]
    private Animator animator;

    private float horiz = 0;                //move horiz input
    private float verti = 0;

    private bool hasMoved = false;          //tell if player has input moved in the current frame
    private bool enabledScript = true;      //tell if this script should be active or not
    private bool firstMove = false;

    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.GameOver, GameOver);
        //EventManager.StartListening(GameData.Event.GameWin, GameOver);
    }

    public void OnObjectSpawn()
    {
        CucarachaManager.Instance.GetCurarachaList().Add(this);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        enabledScript = true;               //active this script at start
    }

    /// <summary>
    /// manage input player (in uptate)
    /// if we move: set hasMoved to true
    /// </summary>
    private void InputPlayer()
    {
        horiz = Input.GetAxisRaw("Horizontal");
        verti = Input.GetAxisRaw("Vertical");

        hasMoved = (horiz != 0 || verti != 0);
    }

    /// <summary>
    /// move in physics, according to input of player
    /// </summary>
    private void MovePlayer()
    {
       if (hasMoved)
       {
            UnityMovement.MoveByForcePushing_WithPhysics(rb, rb.transform.forward, speedPlayer * verti * Time.deltaTime);
            rb.transform.Rotate(new Vector3(0, horiz, 0) * (rotationSpeed * Time.deltaTime));
            //rb.transform.rotation = ExtQuaternion.DirObject(rb.transform.rotation, horiz, verti, rotationSpeed, ExtQuaternion.TurnType.Y);
       } 
    }

    private void Turn()
    {

    }

    /// <summary>
    /// called when the game is over: desactive player
    /// </summary>
    private void GameOver()
    {
        Debug.Log("game over !!");
        enabledScript = false;
    }

    /// <summary>
    /// handle input
    /// </summary>
    private void Update()
    {
        if (!enabledScript)
            return;

        InputPlayer();
    }

    /// <summary>
    /// handle move physics
    /// </summary>
    private void FixedUpdate()
    {
        if (!enabledScript)
            return;
        MovePlayer();
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameData.Event.GameOver, GameOver);
        //EventManager.StopListening(GameData.Event.GameOver, GameWin);
    }



    public void OnDesactivePool()
    {
        
    }

    public void Kill()
    {
        CucarachaManager.Instance.GetCurarachaList().Remove(this);
        transform.SetParent(ObjectsPooler.Instance.transform);
        gameObject.SetActive(false);
    }
}
