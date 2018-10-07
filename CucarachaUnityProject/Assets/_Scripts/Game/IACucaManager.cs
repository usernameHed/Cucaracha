using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IACucaManager : MonoBehaviour
{

    private static float generateNormalRandom(float mu, float sigma)
    {
        float rand1 = Random.Range(0.0f, 1.0f);
        float rand2 = Random.Range(0.0f, 1.0f);

        float n = Mathf.Sqrt(-2.0f * Mathf.Log(rand1)) * Mathf.Cos((2.0f * Mathf.PI) * rand2);

        return (mu + sigma * n);
    }


    /// <summary>
    /// STATE MACHINE FOR CUCA AI
    /// </summary>
    public void Machine()
    {
        List<CucarachaController> cucarachas = CucarachaManager.Instance.GetCurarachaList();

        
        Food foodInfo;
        Lamp lightInfo;
        Vector3 vectDir;
        Vector2 stopVect;
        int count = 0;
        int state;
        float sinVal;
        float x, y;

        

        for (int i = 0; i < cucarachas.Count; i++)
        {
            CucarachaController cuca = cucarachas[i];
            state = cuca.GetIA().State;

            int randInt = 0;

            if (cuca.IsDying)
            {
                continue;
            }
           
            switch (state)
            {

                // IDLE
                //===---
                case 0:         //Don't move
                    {
                        stopVect = new Vector2(cuca.rb.transform.forward.x, cuca.rb.transform.forward.z);
                        cuca.ChangeDirectionIA(new Vector2(stopVect.x*0.0001f, stopVect.y * 0.0001f));

                        //Debug.Log(stopVect.x + " " + stopVect.y);
                        //Debug.DrawRay(cuca.rb.transform.position, stopVect, Color.red, 1f);
                        

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
                        stopVect = new Vector2(cuca.rb.transform.forward.x, cuca.rb.transform.forward.z);
                        x = generateNormalRandom(0.0f, 0.5f);
                        y = generateNormalRandom(0.0f, 0.5f);

                        

                        if (Random.Range(0, 100) == 1)
                        {
                            cuca.ChangeDirectionIA(new Vector2(stopVect.x + x + 5.0f, stopVect.y + y + 5.0f));
                        }
                        else
                        {
                            cuca.ChangeDirectionIA(new Vector2(stopVect.x + x, stopVect.y + y));
                        }

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

                        //if (sinVal + (2.0f*Mathf.PI)/10.0f > 2.0f*Mathf.PI)
                        //{
                        //    cuca.GetIA().sinValue += 2.0f;
                        //}
                        //else
                        //{
                        //    cuca.GetIA().sinValue += 2.0f;
                        //}

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

                        
                        vectDir = -(cuca.rb.transform.position - foodInfo.transform.position); // Create vector for direction
                        vectDir.Normalize();

                        cuca.ChangeDirectionIA(new Vector2(vectDir.x * foodInfo.weight, vectDir.z * foodInfo.weight)); // change dir

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
                            randInt = Random.Range(0, 2);
                            if (randInt == 1)
                            {
                                state = 3;
                                break;
                            }
                        }
                        break;
                    }

                case 3: // Forward food with variations in direction
                    {

                        //Vector2 vectVariation = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
                                                
                        foodInfo = cuca.GetFood();
                        if (!foodInfo)
                        {
                            state = 0;
                            break;
                        }
                        
                        vectDir = -(cuca.rb.transform.position - foodInfo.transform.position);
                        vectDir.Normalize();
                        x = generateNormalRandom(0.0f, 0.3f);
                        y = generateNormalRandom(0.0f, 0.3f);

                        cuca.ChangeDirectionIA(new Vector2((vectDir.x + x) * foodInfo.weight, (vectDir.y + y) * foodInfo.weight));

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

                        vectDir = (cuca.rb.transform.position - lightInfo.transform.position);
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
                            randInt = Random.Range(0, 2);
                            if (randInt == 1 )
                            {
                                state = 5;
                                break;
                            }
                        }
                        break;
                    }

                case 5: // Flee the light but with variations
                    {
                        //Vector2 vectVariation = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));

                        lightInfo = cuca.GetLamp();
                        if (!lightInfo)
                        {
                            state = 0;
                            break;
                        }
                        vectDir = (cuca.rb.transform.position - lightInfo.transform.position);
                        vectDir.Normalize();
                        x = generateNormalRandom(0.0f, 0.5f);
                        y = generateNormalRandom(0.0f, 0.5f);

                        cuca.ChangeDirectionIA(new Vector2((vectDir.x + x), (vectDir.y + y)));

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
                    }

                case 6: // Keep the direction for one more step to be sure to not stay near the light
                    {

                        if (count <= 5)
                        {
                            count++;

                            break;
                        }
                        else
                        {
                            count = 0;
                            state = 0;
                            break;
                        }
                    }
                //====----
                  
            }

            cuca.GetIA().State = state;
        }
    }



}


