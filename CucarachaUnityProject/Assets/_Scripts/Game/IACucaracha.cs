using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACucaracha : MonoBehaviour {

    [SerializeField]
    private CucarachaController cuca;
    public CucarachaController GetCucarachaController() { return (cuca); }


    private float randVal;
    public int state = 0;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (state)
        {
            //====---
            //IDLE
            case 0:
                {
                    cuca.ChangeDirectionIA(new Vector2(0, 0));
                    
                    if (light)
                    {
                        state = 4;
                    }
                    else if (food)
                    {
                        state = 2;
                    }
                    else
                    {
                        randVal = Random.Range(0, 2);

                        if (randVal == 1)
                        {
                            state = 1;
                        }
                    }                                       
                    break;
                }

            case 1:
                {
                    cuca.ChangeDirectionIA(new Vector2(Random.Range(-0.1f,0.1f), Random.Range(-0.1f, 0.1f)));

                    if (light)
                    {
                        state = 4;
                    }
                    else if (food)
                    {
                        state = 2;
                    }
                    else
                    {
                        randVal = Random.Range(0, 2);

                        if (randVal == 1)
                        {
                            state = 1;
                        }
                    }
                    break;
                }
            //====----

            case 2:
                {

                    break;
                }

        }
	}


    
}
