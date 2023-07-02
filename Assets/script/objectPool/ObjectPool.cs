using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Start is called before the first frame update

    public Dictionary<string, Queue<GameObject>> PoolDictionary;

    public static ObjectPool Instance { get; set; } //need to be private

    [System.Serializable]
    public class Pool
    {
        public string new_tag;
        public GameObject prefeb;
        public int size;
    }
    public List<Pool> Pools;
    private void Awake()
    {
       if(Instance == null)
        {
            Debug.Log("only one Object pool instance available");
        }
       Instance = this;
    }

    // object pooling to Instantiate the object before the game start

    //usage:
    //add the prefab and tag(for dictionary) to the Pool in editor
    // functions:
    //
    // ObjectPool.Instance.SpawnFromPool(string target_tag, Vector3 position, Quaternion rotation)
    void Start()
    {
        Instance = this;
        PoolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in Pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject gameObject = Instantiate(pool.prefeb, transform, true);
                gameObject.SetActive(false);
                objectPool.Enqueue(gameObject);
            }
            // Debug.Log("tag: "+pool.new_tag);
            PoolDictionary.Add(pool.new_tag, objectPool);
            // Debug.Log(PoolDictionary.ContainsKey("bullet"));
        }
    }


    public GameObject SpawnFromPool(string target_tag, Vector3 position, Quaternion rotation)
    {
        if (!PoolDictionary.ContainsKey(target_tag))
        {
            Debug.Log(target_tag + "not exist");
            return null;
        }
        // Debug.Log(target_tag + "is spawning");

        GameObject spawnObj = PoolDictionary[target_tag].Dequeue();
        spawnObj.SetActive(true);
        spawnObj.transform.position = position;
        spawnObj.transform.rotation = rotation;
        
        PoolDictionary[target_tag].Enqueue(spawnObj);
        return spawnObj;
    }
}
