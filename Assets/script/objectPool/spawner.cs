using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class SpawningBaseController : NetworkBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private ObjectPool objectPool;
    [SerializeField]
    private string spawnObjectTag;

    private float spawnerCounter = 0f;

    public float spawnerCounterUpper = 5f;
    public float spawnerCounterLower = 20f;

    public GameObject rocks;

    [SerializeField]
    // private BuildingSetting buildingSetting;

    void Start()
    {
        // if (!objectPool)
        // {
        //     objectPool = GameObject.Find("ObjectPool").GetComponent<ObjectPool>();
        // }
        // if(isClickControl){
        //     MouseController.Instance.MouseOnLeftClickEvent += SpawnOnLeftClick;
        //     MouseController.Instance.MouseOnRightClickEvent += SpawnOnRightClick;
        // }

    }
    void Update(){
        spawnerCounter -= Time.deltaTime;
        if(spawnerCounter < 0f){
            SpawnObject();
            spawnerCounter = Random.Range(spawnerCounterLower, spawnerCounterUpper);
        }
    }
    private void SpawnObject()
    {
        if(IsHost || IsServer){
            if(NetworkManager.IsListening){
            Vector3 pos = new Vector3(transform.position.x + Random.Range(-2f, 2f), transform.position.y + Random.Range(-2f, 2f), transform.position.z);
            GameObject newRocks = Instantiate(rocks, pos, Quaternion.identity);
            newRocks.GetComponent<NetworkObject>().Spawn();
        }
        }

    }

}
