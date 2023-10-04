using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using System.Net;

public class ClientScript : MonoBehaviour
{

    public NetworkManager networkManager;
    public UnityTransport networkTransport;



    void Start()
    {

        StartClient();
    }

    void StartClient()
    {

        networkTransport.ConnectionData.Address = "64.227.144.107";
        networkTransport.ConnectionData.Port = 7777;
        networkManager.StartClient();

    }
}
