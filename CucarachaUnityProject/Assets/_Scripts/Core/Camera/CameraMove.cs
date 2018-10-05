using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Collections;

/// <summary>
/// Manage camera
/// Smoothly move camera to m_DesiredPosition
/// m_DesiredPosition is the barycenter of target list
/// </summary>
public class CameraMove : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    // Time before next camera move
    [FoldoutGroup("GamePlay"), Tooltip("Le smooth de la caméra"), SerializeField]
    private float smoothTime = 0.2f;

    [FoldoutGroup("GamePlay"), Tooltip("Le smooth de la caméra"), SerializeField]
    private float smoothTimeZ = 0.2f;


    //Fallback target if target list is empty
    [FoldoutGroup("GamePlay"), Space(10), Tooltip("objet que la caméra doit focus s'il n'y a plus de target"), SerializeField]
    private Transform fallBackTarget;


	private Vector3 currentVelocity;

    private Vector3 posLisener;
    private float holdSmooth = 0;


    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.GameOver, GameOver);
    }

    /// <summary>
    /// fonction appelé lorsque la partie est fini
    /// </summary>
    private void GameOver()
    {
        
    }


    /// <summary>
	/// Smoothly move camera toward averageTargetPosition
    /// </summary>
    private void FixedUpdate()
    {
        Vector3 posFallBack = new Vector3(fallBackTarget.position.x, transform.position.y, fallBackTarget.position.z);
		transform.position = Vector3.SmoothDamp(transform.position, posFallBack, ref currentVelocity, smoothTime);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, 0, Time.deltaTime * smoothTimeZ);
        //posLisener = transform.position;    //change listenerPosition
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameData.Event.GameOver, GameOver);
    }
}