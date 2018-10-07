using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMono<SoundManager>
{
    

    private void Awake()
    {
        // Set audioListener Config
        Destroy(Camera.main.GetComponent<AudioListener>());
        gameObject.AddComponent<AudioListener>();
    }


}
