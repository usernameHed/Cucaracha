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

        for (int i = 0; i < cucarachas.Count; i++)
        {
            CucarachaController cuca = cucarachas[i];
            state = cuca.GetIA().State;

            int randInt = 0;
            
          
            switch(state)
            {

                // IDLE
                //===---
                case 0:         //Don't move
                    {
                        cuca.ChangeDirectionIA(new Vector2(0, 0));

                        if (cuca.isInsideLight)
                        {
                            state = 4;
                        }
                        else if (cuca.isInsideFood)
                        {
                            state = 2;
                        }
                        else
                        {
                            randInt = Random.Range(0, 2);

                            if (randInt == 1)
                            {
                                state = 1;
                            }
                        }
                        break;
                    }

                case 1:         //Crawl
                    {

                        cuca.ChangeDirectionIA(new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f)));

                        if (cuca.isInsideLight)
                        {
                            state = 4;
                        }
                        else if (cuca.isInsideFood)
                        {
                            state = 2;
                        }
                        else
                        {
                           state = 0;                          
                        }
                        break;
                    }
                //===---

                // FOOD 
                case 2: // Forward food
                    {

                        foodInfo = cuca.GetFood(); //Get food info
                        vectDir = cuca.transform.position - foodInfo.transform.position; // Create vector for direction
                        vectDir.Normalize();

                        cuca.ChangeDirectionIA(new Vector2(vectDir.x, vectDir.z)); // change dir

                        if (cuca.isInsideLight)
                        {
                            state = 4;
                        }
                        else if (!cuca.isInsideFood)
                        {
                            state = 0;
                        }
                        else
                        {
                            randInt = Random.Range(1, 6);
                            if (randInt == 1 || randInt == 2)
                            {
                                state = 3;
                            }
                        } 

                        break;
                    }

                case 3: // Forward food with variations in direction
                    {

                        Vector2 vectVariation = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f)); 

                        foodInfo = cuca.GetFood();
                        vectDir = cuca.transform.position - foodInfo.transform.position;
                        vectDir.Normalize();
                                                
                        cuca.ChangeDirectionIA(new Vector2(vectDir.x + vectVariation.x, vectDir.z + vectVariation.y));

                        if (cuca.isInsideLight)
                        {
                            state = 4;
                        }
                        else if (!cuca.isInsideFood)
                        {
                            state = 0;
                        }
                        else
                        {                        
                            state = 2;
                        }

                        break;
                    }
                
                //======-----


                // LIGHT
                //===---
                case 4:  // Flee the light
                    {
                        lightInfo = cuca.GetLamp();
                        vectDir = -(cuca.transform.position - lightInfo.transform.position);
                        vectDir.Normalize();

                        cuca.ChangeDirectionIA(new Vector2(vectDir.x, vectDir.z));

                        
                        if (!cuca.isInsideLight && cuca.isInsideFood)
                        {
                            state = 2;
                        }
                        else if(!cuca.isInsideLight && !cuca.isInsideFood)
                        {
                            state = 6;
                        }
                        else
                        {
                            randInt = Random.Range(1, 6);
                            if (randInt == 1 || randInt == 2)
                            {
                                state = 5;
                            }
                        }
                        break;
                    }

                case 5: // Flee the light but with variations
                    {
                        Vector2 vectVariation = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));

                        lightInfo = cuca.GetLamp();
                        vectDir = -(cuca.transform.position - lightInfo.transform.position);
                        vectDir.Normalize();

                        cuca.ChangeDirectionIA(new Vector2(vectDir.x + vectVariation.x, vectDir.z + vectVariation.y));
                        
                        if (!cuca.isInsideLight && cuca.isInsideFood)
                        {
                            state = 2;
                        }
                        else if (!cuca.isInsideLight && !cuca.isInsideFood)
                        {
                            state = 6;
                        }
                        else
                        {
                            state = 4;
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


