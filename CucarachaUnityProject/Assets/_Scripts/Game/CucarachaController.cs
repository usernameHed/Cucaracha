using Sirenix.OdinInspector;
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

    [SerializeField]
    private float magnitudeRun = 0.3f;
    [SerializeField]
    private float timeOfDeath = 0.5f;

    [SerializeField, FoldoutGroup("OnWall")]
    private float speedTurnOnWall = 10f;
    [SerializeField, FoldoutGroup("OnWall")]
    private float speedMoveOnWall = 5f;

    [SerializeField]
    private Animator animator;

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

    [SerializeField]
    private bool isDying = false;
    public bool IsDying { get { return (isDying); } }

    public Food GetFood() { return (refFood); }
    public Lamp GetLamp() { return (refLamp); }

    [SerializeField]
    private FrequencyCoolDown eatFrequency = new FrequencyCoolDown();

    private SphereCollider sphereCollider;

    [Space(10)]

    
    public Rigidbody rb;                   //link to rigidbody
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


    private Vector3 bloodVect = new Vector3(0, 0, 0);

    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.GameOver, GameOver);
        sphereCollider = rb.GetComponent<SphereCollider>();

        
    }

    public void OnObjectSpawn()
    {
        radiusSphere = sphereCollider.radius;
        rb.transform.localPosition = Vector3.zero;
        isDying = false;
        animator.SetTrigger("Idle");
        enabledScript = true;
        //EventManager.StartListening(GameData.Event.GameWin, GameOver);
        //EventManager.StartListening(GameData.Event.GameOver, GameOver);
        //private Vector3 dirCura = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
        //Debug.Log("spawned !!");
        animator.SetTrigger("Idle");
        CucarachaManager.Instance.AddCucaracha(this);
        ChangeDirectionIA(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
    }

    /// <summary>
    /// called by IA at each frame
    /// </summary>
    [Button]
    public void ChangeDirectionIA(Vector2 dir)
    {
        float oldMagnitude = dirCura.magnitude;
        dirCura = new Vector3(dir.x, dir.y, 0);
        float newMagnitude = dirCura.magnitude;

        if (newMagnitude >= magnitudeRun && oldMagnitude < magnitudeRun)
        {
            animator.SetTrigger("Walking");
        }
        else if (newMagnitude < magnitudeRun && oldMagnitude >= magnitudeRun)
        {
            if (isInsideFood)
                animator.SetTrigger("Eating");
            else
                animator.SetTrigger("Idle");
        }
    }

    public void InvertDirection(Vector3 dir)
    {
        dirCura = dir * speedTurnOnWall;
        UnityMovement.MoveByForcePushing_WithPhysics(rb, dirCura, speedPlayer * GetOnlyForward() * speedMoveOnWall * Time.deltaTime);
        //dirCura = Quaternion.Euler(dir) * dirCura;
        //print(dirCura);
        //dirCura = new Vector3(-dirCura.x, -dirCura.y, 0);
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
        bool oldHasMoved = hasMoved;

        horiz = Input.GetAxisRaw("Horizontal");
        verti = Input.GetAxisRaw("Vertical");

        hasMoved = (horiz != 0 || verti != 0);
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
        //Debug.Log("game over !!");
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
        if (!enabledScript || isDying)
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
        InactiveAll();
    }

    public void Kill()
    {
        Kill(true);
    }

    [Button]
    public void Kill(bool addCadavre)
    {
        if (isDying)
            return;

        isDying = true;

        bloodVect = rb.transform.position + new Vector3(0.0f,0.0f,-0.1f);
        
        
        ObjectsPooler.Instance.SpawnFromPool(GameData.PoolTag.BloodSplash, bloodVect, rb.transform.rotation * Quaternion.Euler(90,0,0), ObjectsPooler.Instance.transform);

        animator.SetTrigger("Death");
        StartCoroutine(RealyKill(addCadavre));
    }

    private IEnumerator RealyKill(bool addCadavre)
    {
        yield return new WaitForSeconds(timeOfDeath);

        if (addCadavre)
            ObjectsPooler.Instance.SpawnFromPool(GameData.PoolTag.DeadCuca, rb.transform.position, rb.transform.rotation, ObjectsPooler.Instance.transform);

        //

        InactiveAll();
    }

    private void InactiveAll()
    {
        CucarachaManager.Instance.RemoveCuca(this);
        transform.SetParent(ObjectsPooler.Instance.transform);

        CucarachaManager.Instance.TestEndLevel();

        gameObject.SetActive(false);
    }
}
