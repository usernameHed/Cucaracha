using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [Serializable]
    public struct SpawnInfo
    {
        public Transform spawn;
        public float percentage;
    }

    [SerializeField]
    private float rangeSpawn = 5f;

    [SerializeField]
    private List<SpawnInfo> spawnPoint = new List<SpawnInfo>();

    private void Start()
    {
        /*
        spawnPoint.Clear();
        foreach (Transform child in transform)
        {
            spawnPoint.Add(child);
        }
        */
    }

    /// <summary>
    /// called for getting a player position
    /// </summary>
    /// <returns></returns>
    private Vector3 GetPosSpawn()
    {
        Pointf point = new Pointf(0.0f, 0.0f);

        bool isInside = false;
        int randomList = UnityEngine.Random.Range(0, 100);
        float actualPertentage = 0;
        for (int i = 0; i < spawnPoint.Count; i++)
        {
            if (randomList >= actualPertentage && randomList <= actualPertentage + spawnPoint[i].percentage)
            {
                //ici ok
                point.x = spawnPoint[i].spawn.position.x + (UnityEngine.Random.Range(0.0f, rangeSpawn) * ExtRandom.RandomNegative());
                point.y = spawnPoint[i].spawn.position.z + (UnityEngine.Random.Range(0.0f, rangeSpawn) * ExtRandom.RandomNegative());
                isInside = true;
                break;
            }
            actualPertentage += spawnPoint[i].percentage;
        }

        if (!isInside)
        {
            point.x = spawnPoint[0].spawn.position.x + (UnityEngine.Random.Range(0.0f, rangeSpawn) * ExtRandom.RandomNegative());
            point.y = spawnPoint[0].spawn.position.z + (UnityEngine.Random.Range(0.0f, rangeSpawn) * ExtRandom.RandomNegative());
        }

        
        return (new Vector3(point.x, 0, point.y));
    }

    /// <summary>
    /// called spawn
    /// </summary>
    public void SpawnCuca(int number)
    {
        List<CucarachaController> curaracha = new List<CucarachaController>();
        for (int i = 0; i < number; i++)
        {
            GameObject cuca = ObjectsPooler.Instance.SpawnFromPool(GameData.PoolTag.Cucaracha, GetPosSpawn(), Quaternion.identity, CucarachaManager.Instance.transform);
            //curaracha.Add(cuca.GetComponent<CucarachaController>());
        }
        //return (curaracha);
    }
    
}
