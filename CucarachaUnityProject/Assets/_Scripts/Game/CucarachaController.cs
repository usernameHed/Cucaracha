﻿using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucarachaController : MonoBehaviour, IPooledObject, IKillable
{
    [SerializeField]
    private float speedPlayer = 5;          //speed of player
    [SerializeField]
    private float rotationSpeed = 5;          //rotation speed of player
    [SerializeField, FoldoutGroup("Eat")]
    private float radiusWhenEating = 0.1f;
    [SerializeField, FoldoutGroup("Eat")]
    private float valueToGrow = 0.05f;

    [SerializeField, ReadOnly]
    private bool isEating = false;

    [ReadOnly]
    public bool isInsideFood = false;
    [ReadOnly]
    public bool isInsideLight = false;
    [SerializeField, ReadOnly]
    public Food refFood = null;
    [SerializeField, ReadOnly]
    public Lamp refLamp = null;

    public Food GetFood() { return (refFood); }
    public Lamp GetLamp() { return (refLamp); }

    [SerializeField]
    private FrequencyCoolDown eatFrequency = new FrequencyCoolDown();

    private SphereCollider sphereCollider;

    [Space(10)]

    
    public Rigidbody rb;                   //link to rigidbody
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private IACucaracha ia;
    public IACucaracha GetIA() { return (ia); }

    private float horiz = 0;                //move horiz input
    private float verti = 0;

    private bool hasMoved = false;          //tell if player has input moved in the current frame
    private bool enabledScript = true;      //tell if this script should be active or not
    private bool firstMove = false;

    private Vector3 dirCura = new Vector3(0, 0, 0);

    private float radiusSphere = 0.3f;
    private bool isGrowing = false;

    /// <summary>
    /// called by IA at each frame
    /// </summary>
    [Button]
    public void ChangeDirectionIA(Vector2 dir)
    {
        dirCura = new Vector3(dir.x, dir.y, 0);
    }

    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.GameOver, GameOver);
        sphereCollider = rb.GetComponent<SphereCollider>();

        radiusSphere = sphereCollider.radius;
        //EventManager.StartListening(GameData.Event.GameWin, GameOver);
    }

    /// <summary>
    /// set if the cuca is inside food or not, and if yes: set the reference
    /// </summary>
    public void SetInsideFood(bool inside, Food _food)
    {
        isInsideFood = inside;
        IsEating(inside);
        if (inside)
            refFood = _food;
        else
            refFood = null;
    }

    private void IsEating(bool eating)
    {
        if (eating)
        {
            isEating = true;
            isGrowing = false;
            //sphereCollider.radius = radiusWhenEating;
        }
        else if (!isGrowing)
        {
            isGrowing = true;
            isEating = false;
            //rb.GetComponent<SphereCollider>().radius = radiusSphere;
        }
            
    }
    /*
    private void AddRadiusIfTinyAndNotEating()
    {
        if (sphereCollider.radius < radiusSphere && !isEating)
        {
            sphereCollider.radius += valueToGrow;
        }
        if (sphereCollider.radius > radiusSphere)
        {
            sphereCollider.radius = radiusSphere;
        }
    }
    */

    /// <summary>
    /// set if the cuca is inside food or not, and if yes: set the reference
    /// </summary>
    public void SetInsideLamp(bool inside, Lamp _lamp)
    {
        isInsideLight = inside;
        if (inside)
            refLamp = _lamp;
        else
            refLamp = null;
    }

    public void OnObjectSpawn()
    {
        //private Vector3 dirCura = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
        //Debug.Log("spawned !!");
        CucarachaManager.Instance.AddCucaracha(this);
        ChangeDirectionIA(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
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
        /*if (hasMoved)
        {
            dirCura = new Vector3(horiz, verti, 0);
        }*/
    }

    private float GetOnlyForward()
    {
        float horizAbs = Mathf.Abs(dirCura.x);
        float vertiAbs = Mathf.Abs(dirCura.y);

        return (new Vector3(horizAbs, vertiAbs, 0).magnitude);
    }

    /// <summary>
    /// move in physics, according to input of player
    /// </summary>
    private void MovePlayer()
    {
       //if (hasMoved)
       //{
            UnityMovement.MoveByForcePushing_WithPhysics(rb, rb.transform.forward, speedPlayer * GetOnlyForward() * Time.deltaTime);
            rb.transform.rotation = ExtQuaternion.DirObject(rb.transform.rotation, dirCura, rotationSpeed, ExtQuaternion.TurnType.Y);
       //} 
    }

    /// <summary>
    /// eat !
    /// </summary>
    public bool Eat()
    {
        if (eatFrequency.IsReady())
        {
            eatFrequency.StartCoolDown();
            return (true);
        }
        return (false);
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
        //AddRadiusIfTinyAndNotEating();
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

    [Button]
    public void Kill()
    {
        CucarachaManager.Instance.RemoveCuca(this);
        transform.SetParent(ObjectsPooler.Instance.transform);
        gameObject.SetActive(false);
    }
}
