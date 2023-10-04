using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using System.Net;

public class ServerScript : MonoBehaviour
{
    
    public NetworkManager networkManager;
    public UnityTransport networkTransport;
    
    

    public void Start()
    {

        StartServer();
    }

    void StartServer()
    {

        networkTransport.ConnectionData.Address = "64.227.144.107";
        networkTransport.ConnectionData.Port = 7777;
        networkManager.StartServer();
        
        

    }
}
