using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using TMPro;
using System;
using Cinemachine;
public class UnityNetCodeUI : NetworkBehaviour
{
    // Start is called before the first frame update
    public Button startHostButton;
    public Button startClientButton;
    public Button joinOffButton;

    public TMP_Text HostIpText;
    public TMP_InputField ClientIpText0;
    public TMP_InputField ClientIpText1;
    public TMP_InputField ClientIpText2;
    public TMP_InputField ClientIpText3;

    public TMP_InputField PlayerName;
    public GameManager gameManager;
    public GameObject HPHeart;

    NetworkObject playerObject;
    private bool Spawned;
    public CinemachineVirtualCamera cinemachineVirtualCamera;

    
    void Start()
    {

        startClientButton.onClick.AddListener(()=>{
        
            string theParsedIp = "";

            theParsedIp += System.Convert.ToInt32(ClientIpText0.text.Trim()) +".";
            theParsedIp += System.Convert.ToInt32(ClientIpText1.text.Trim()) +".";
            theParsedIp += System.Convert.ToInt32(ClientIpText2.text.Trim()) +".";
            theParsedIp += System.Convert.ToInt32(ClientIpText3.text.Trim());
            

            GameManager.Instance.setClientIP(theParsedIp);
            Debug.Log("start Client on ipZZZ: "+ theParsedIp);
            NetworkManager.Singleton.StartClient();
            GameManager.Instance.playerName = PlayerName.text;
            HPHeart.SetActive(true);


        });
        startHostButton.onClick.AddListener(()=>{
            GameManager.Instance.setHostIP();
            Debug.Log("start host on ip: "+ GameManager.Instance.HostIp);
            NetworkManager.Singleton.StartHost();
            GameManager.Instance.playerName = PlayerName.text;
            HPHeart.SetActive(true);

        });

        joinOffButton.onClick.AddListener(()=>{
            GameManager.Instance.setOfficalIP();
            Debug.Log("joining Server on ip: "+ GameManager.Instance.HostIp);
            NetworkManager.Singleton.StartClient();
            GameManager.Instance.playerName = PlayerName.text;
            HPHeart.SetActive(true);
            
        });
        
        HostIpText.SetText("Host ip: " + GameManager.Instance.HostIp);

    }

    // Update is called once per frame
    void Update()
    {
        if(NetworkManager.Singleton.LocalClient != null){
            if(NetworkManager.Singleton.LocalClient.PlayerObject){
                playerObject = NetworkManager.Singleton.LocalClient.PlayerObject; 
                cinemachineVirtualCamera.Follow = playerObject.transform;
                cinemachineVirtualCamera.LookAt = playerObject.transform;
                gameObject.SetActive(false);
            }

        }

    }
}
