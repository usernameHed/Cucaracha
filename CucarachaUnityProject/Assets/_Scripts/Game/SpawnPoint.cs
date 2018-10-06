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
    public Vector3 GetPosSpawn()
    {
        Pointf point = new Pointf(0.0f, 0.0f);

        int randomList = UnityEngine.Random.Range(0, spawnPoint.Count);
        point.x = spawnPoint[randomList].position.x + (UnityEngine.Random.Range(0.0f, rangeSpawn) * ExtRandom.RandomNegative());
        point.y = spawnPoint[randomList].position.z + (UnityEngine.Random.Range(0.0f, rangeSpawn) * ExtRandom.RandomNegative());

        return (new Vector3(point.x, 0, point.y));
    }

    /// <summary>
    /// called spawn
    /// </summary>
    public List<CucarachaController> SpawnToList(int number)
    {
        List<CucarachaController> curaracha = new List<CucarachaController>();
        for (int i = 0; i < number; i++)
        {
            ObjectsPooler.Instance.SpawnFromPool(GameData.PoolTag.Cucaracha, GetPosSpawn(), Quaternion.identity, CucarachaManager.Instance.transform);
        }
        return (curaracha);
    }
    
}
