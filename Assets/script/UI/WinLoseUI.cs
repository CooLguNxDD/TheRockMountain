using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using System;
using Unity.Netcode;
using TMPro;
using UnityEngine.SceneManagement;

public class WinLoseUI : MonoBehaviour
{
    public UnitStates unitStates;
    NetworkObject playerObject;
    private bool PlayerFound;

    public GameObject FinalScreen;
    public GameObject MainUI;
    public string TitleText;
    public TMP_Text Title;
    public TMP_Text Time;
    public TMP_Text Peak;
    
    void Start()
    {
        PlayerFound = false;
    }

    public void performWinUIPop(object sender, EventArgs args){
        FinalScreen.SetActive(true);
        Title.SetText("You Win!");
        Time.SetText("Your Playtime: "+ (int)GameManager.Instance.GameTime + "s");
        Peak.SetText("Your Peak: "+ (int)GameManager.Instance.PlayerHeight + "m");
    }

    public void performLoseUIPop(object sender, EventArgs args){
        FinalScreen.SetActive(true);
        Title.SetText("You Died");
        Time.SetText("Your Playtime: "+ (int)GameManager.Instance.GameTime + "s");
        Peak.SetText("Your Peak: "+ (int)GameManager.Instance.PlayerHeight + "m");

    }

    public void Replay(){
        NetworkManager.Singleton.Shutdown();
        SceneManager.LoadScene("LV1");
    }


    // Update is called once per frame
    void Update()
    {
        if(!PlayerFound){
            if(NetworkManager.Singleton.LocalClient != null){
                if(NetworkManager.Singleton.LocalClient.PlayerObject){
                    playerObject = NetworkManager.Singleton.LocalClient.PlayerObject;
                    playerObject.GetComponent<UnitStates>().loseEventHandler += performLoseUIPop;
                    playerObject.GetComponent<UnitStates>().WinEventHandler += performWinUIPop;
                    PlayerFound = true;
                }
            }
        }
    }
}
