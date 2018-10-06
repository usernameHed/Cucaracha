using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACucaManager : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    public void Machine()
    {
        List<CucarachaController> cucarachas = CucarachaManager.Instance.GetCurarachaList();

        int state;
        Food foodInfo;
        Lamp lightInfo;
        Vector3 vectDir;
        Vector2 stopVect;
        float sinVal;

        for (int i = 0; i < cucarachas.Count; i++)
        {
            CucarachaController cuca = cucarachas[i];
            state = cuca.GetIA().State;

            int randInt = 0;

           // Debug.Log(state);
           
            switch (state)
            {

                // IDLE
                //===---
                case 0:         //Don't move
                    {
                        stopVect = new Vector2(cuca.transform.forward.x, cuca.transform.forward.z);
                        cuca.ChangeDirectionIA(new Vector2(stopVect.x*0.001f, stopVect.y * 0.001f));

                        Debug.Log(stopVect.x);
                        

                        if (cuca.isInsideLight)
                        {
                            state = 4;
                            break;
                        }
                        else if (cuca.isInsideFood)
                        {
                            state = 2;
                            break;
                        }
                        else
                        {
                            randInt = Random.Range(0,5);

                            if (randInt == 1)
                            {
                                state = 1;
                                break;
                            }
                        }
                        break;
                    }

                case 1:         //Crawl
                    {

                        sinVal = cuca.GetIA().sinValue;
                        stopVect = new Vector2(cuca.transform.forward.x, cuca.transform.forward.z);

                        cuca.ChangeDirectionIA(new Vector2(stopVect.x + Mathf.Sin(sinVal), stopVect.y + Mathf.Sin(sinVal)));

                        if (cuca.isInsideLight)
                        {
                            state = 4;
                            break;
                        }
                        else if (cuca.isInsideFood)
                        {
                            state = 2;
                            break;
                        }

                        if (sinVal + (2*Mathf.PI)/10 > 2*Mathf.PI)
                        {
                            cuca.GetIA().sinValue = 0.0f ;
                        }
                        else
                        {
                            cuca.GetIA().sinValue += 0.01f;
                        }

                        state = 0;
                        break;
                        
                    }
                //===---

                // FOOD 
                case 2: // Forward food
                    {

                        foodInfo = cuca.GetFood(); //Get food info
                        if (!foodInfo)
                        {
                            state = 0;
                            break;
                        }
                        vectDir = -(cuca.transform.position - foodInfo.transform.position); // Create vector for direction
                        vectDir.Normalize();

                        cuca.ChangeDirectionIA(new Vector2(vectDir.x, vectDir.z)); // change dir

                        if (cuca.isInsideLight)
                        {
                            state = 4;
                            break;
                        }
                        else if (!cuca.isInsideFood)
                        {
                            state = 0;
                            break;
                        }
                        else
                        {
                            randInt = Random.Range(1, 6);
                            if (randInt == 1 || randInt == 2)
                            {
                                state = 3;
                                break;
                            }
                        }
                        break;
                    }

                case 3: // Forward food with variations in direction
                    {

                        Vector2 vectVariation = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f)); 

                        foodInfo = cuca.GetFood();
                        if (!foodInfo)
                        {
                            state = 0;
                            break;
                        }
                        vectDir = -(cuca.transform.position - foodInfo.transform.position);
                        vectDir.Normalize();
                                                
                        cuca.ChangeDirectionIA(new Vector2(vectDir.x + vectVariation.x, vectDir.z + vectVariation.y));

                        if (cuca.isInsideLight)
                        {
                            state = 4;
                            break;
                        }
                        else if (!cuca.isInsideFood)
                        {
                            state = 0;
                            break;
                        }
                        else
                        {                        
                            state = 2;
                            break;
                        }

                        break;
                    }
                
                //======-----


                // LIGHT
                //===---
                case 4:  // Flee the light
                    {
                        lightInfo = cuca.GetLamp();
                        if (!lightInfo)
                        {
                            state = 0;
                            break;
                        }

                        vectDir = (cuca.transform.position - lightInfo.transform.position);
                        vectDir.Normalize();

                        cuca.ChangeDirectionIA(new Vector2(vectDir.x, vectDir.z));

                        
                        if (!cuca.isInsideLight && cuca.isInsideFood)
                        {
                            state = 2;
                            break;
                        }
                        else if(!cuca.isInsideLight && !cuca.isInsideFood)
                        {
                            state = 6;
                            break;
                        }
                        else
                        {
                            randInt = Random.Range(1, 6);
                            if (randInt == 1 || randInt == 2)
                            {
                                state = 5;
                                break;
                            }
                        }
                        break;
                    }

                case 5: // Flee the light but with variations
                    {
                        Vector2 vectVariation = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));

                        lightInfo = cuca.GetLamp();
                        if (!lightInfo)
                        {
                            state = 0;
                            break;
                        }
                        vectDir = (cuca.transform.position - lightInfo.transform.position);
                        vectDir.Normalize();

                        cuca.ChangeDirectionIA(new Vector2(vectDir.x + vectVariation.x, vectDir.z + vectVariation.y));
                        
                        if (!cuca.isInsideLight && cuca.isInsideFood)
                        {
                            state = 2;
                            break;
                        }
                        else if (!cuca.isInsideLight && !cuca.isInsideFood)
                        {
                            state = 6;
                            break;
                        }
                        else
                        {
                            state = 4;
                            break;
                        }

                        break;
                    }

                case 6: // Keep the direction for one more step to be sure to not stay near the light
                    {

                        state = 0;

                        break;
                    }
                //====----
                  
            }

            cuca.GetIA().State = state;
        }
    }



}


