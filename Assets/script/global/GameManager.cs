using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using Unity.Netcode;
using Unity.Netcode.Transports;
using Unity.Netcode.Transports.UTP;
using Cinemachine;

using System.Net;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public GameObject networkManager;

    public string playerName;

    public UnityEvent OnGameDeath;
    public string HostIp = "127.0.0.1";
    public string ClientIp = " ";

    public float GameTime = 0;
    public float PlayerHeight = 0;

    public float SecondChance = 0;

    public void Awake(){
        if (Instance == null)
        {
            Debug.Log("only one GameManager instance available");

        }
        Instance = this;
    }
    void Start(){
        HostIp = getCurrentIp();
        Debug.Log("ip: " + HostIp);
    }

    public string getCurrentIp(){
        string UriHostName = System.Net.Dns.GetHostName();
        IPHostEntry entry = System.Net.Dns.GetHostEntry(UriHostName);
        IPAddress[] addr = entry.AddressList;
        
        return addr[addr.Length-1].ToString();
    }

    public void setHostIP(){
        Debug.Log(HostIp);
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(HostIp.ToString(), 13333);
    }

    public void setOfficalIP(){
        HostIp = "192.168.1.1";
        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(HostIp.ToString(), 13333);
    }

    public void setClientIP(string Ip){
        
        Debug.Log(Ip);
        Debug.Log(Ip.Equals("192.168.1.102"));

        NetworkManager.Singleton.GetComponent<UnityTransport>().SetConnectionData(Ip, 13333);

    }

    void Update()
    {
        GameTime+=Time.deltaTime;
    }
}

