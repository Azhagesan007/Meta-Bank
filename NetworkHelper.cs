using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Netcode;

public class NetworkHelper : NetworkBehaviour
{
    string domain_url;
    string data;
    public NetworkHelper(string domain_url)
    {
        this.domain_url = domain_url;
    }

    [System.Serializable]
    public class ApiResponse
    {
        public bool login;
        public string data;
        public int Code;
    }



    public IEnumerator RequestApi(string remaining_url, Action<string> callback)
    {
        string url = domain_url + remaining_url;

        Debug.Log(url);
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            callback("400");

        }
        else
        {

            string json = www.downloadHandler.text;
            ApiResponse response = JsonUtility.FromJson<ApiResponse>(json);
            Debug.Log(response.data);
            if (response.login)
            {
                callback(response.data);
            }
            else
            {
                callback("400");
            }
        }




    }



    public IEnumerator RequestStatement(string remaining_url, Action<List<Transaction>> callback)
    {
        string url = domain_url + remaining_url;

        Debug.Log(url);
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
           // callback("400");

        }
        else
        {

            string json = www.downloadHandler.text;
            StatementResponse response = JsonUtility.FromJson<StatementResponse>(json);
            Debug.Log(response.data);
            if (response.login)
            {

                callback(response.data);
            }
            else
            {
                Debug.Log("Failed: " + response.data);
            }
        }





    }

    [System.Serializable]
    public class StatementResponse
    {
        public bool login;
        public List<Transaction> data;
        public int Code;
    }

    [System.Serializable]
    public class Transaction
    {
        public string time;
        public string type;
        public float amount;
    }
}

