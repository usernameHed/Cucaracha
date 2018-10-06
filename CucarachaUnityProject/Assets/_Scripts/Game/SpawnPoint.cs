using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private float rangeSpawn = 5f;

    [SerializeField]
    private List<Transform> spawnPoint = new List<Transform>();

    /// <summary>
    /// called for getting a player position
    /// </summary>
    /// <returns></returns>
    public Pointf GetPosSpawn()
    {
        Pointf point = new Pointf(0.0f, 0.0f);

        int randomList = UnityEngine.Random.Range(0, spawnPoint.Count);
        point.x = spawnPoint[randomList].position.x + (UnityEngine.Random.Range(0.0f, rangeSpawn) * ExtRandom.RandomNegative());
        point.y = spawnPoint[randomList].position.z + (UnityEngine.Random.Range(0.0f, rangeSpawn) * ExtRandom.RandomNegative());

        return (point);
    }

    
}
