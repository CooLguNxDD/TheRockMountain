using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Netcode;


public class HPHeartController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject UIHeartGroup;
    public GameObject HeartPrefab;

    public UnitStates unitStates;

    public List<GameObject> HeartPrefabList;

    NetworkObject playerObject;
    private bool PlayerFound;

    void Awake(){
        
        for(int i = 0 ; i < 10f; i++){
            GameObject newHeart = Instantiate(HeartPrefab, new Vector3(0f,0f,0f), Quaternion.identity);
            newHeart.transform.SetParent(UIHeartGroup.transform);
            HeartPrefabList.Add(newHeart);
        }
    }
    void Start()
    {
        PlayerFound = false;
    }


    public void performHeartBrokenVisual(object sender, EventArgs args){
        GameObject gameHeart = HeartPrefabList[(int)playerObject.GetComponent<UnitStates>().getHp() - 1];
        gameHeart.GetComponent<Animator>().SetBool("isBroken", true);
    }

    public void performWinUIPop(object sender, EventArgs args){

    }

    public void performLoseUIPop(object sender, EventArgs args){

    }


    // Update is called once per frame
    void Update()
    {
        if(!PlayerFound){
            if(NetworkManager.Singleton.LocalClient != null){
                if(NetworkManager.Singleton.LocalClient.PlayerObject){
                    playerObject = NetworkManager.Singleton.LocalClient.PlayerObject;
                    playerObject.GetComponent<UnitStates>().unitHurt += performHeartBrokenVisual;
                    playerObject.GetComponent<UnitStates>().loseEventHandler += performLoseUIPop;
                    playerObject.GetComponent<UnitStates>().WinEventHandler += performWinUIPop;
                    PlayerFound = true;
                }
            }
        }
    }
}
