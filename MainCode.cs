using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.IO;
using JetBrains.Annotations;
using System.Net.NetworkInformation;
using UnityEditor.PackageManager.Requests;
using System.Transactions;
using System.Reflection;

public class MainCode : MonoBehaviour
{
    public GameObject screen;
    public GameObject bank;
    public GameObject userlabel;
    public GameObject pinlabel;
    public GameObject loginbtn;
    public GameObject atmscreen;
    public GameObject mainmenu;
    public GameObject balance_screen;
    public GameObject sentmoney;
    public GameObject sendmoneyout;
    public GameObject statement;
    public GameObject pinchangescreen;
    public GameObject door1;
    public GameObject door2;
    public GameObject display;
    public GameObject gamecanvas;
    public GameObject OtherService;
    public GameObject withdrawScreen;
    public GameObject EnterBank;
    public Text depamount;
    public Text mainmenulabel;
    public Text loginid;
    public Text pinnumber;
    public Text balance_display_text;
    public Text sentaccdisplay_text;
    public Text sentmoneydisplay_text;
    public Text sentstatusdisplay_text;
    public Text statementtext;
    public Text OTP;
    public Text pinchangetext;
    public Text cashtext;
    public Dropdown DepOrWith;
    private string userid;
    private int flag = 0;
    private string response_data;
    private string pinno;
    private string sent_acc;
    private string sent_money;
    private string identity;
    private string otp;
    private string newpin;
    private string selectedOption;
    private string depam;
    private bool loginstay;


    //Player Animation
    public Animator playerAnim;
    public Rigidbody playerRigid;

    public float w_speed, wb_speed, olw_speed, rn_speed, ro_speed;
    public bool walking;

    public Transform playerTrans;
    public Transform playerbot;


    public Transform receptiontrans;
    public Transform logintrans;
    public Transform withdrawtrans;
    public Transform statementtrans;
    public Transform balancetrans;
    public Transform transfertrans;
    public Transform pintrans;

    public Transform loginbot;
    public Transform receptbot;
    public Transform transferbot;
    public Transform withdrawbot;
    public Transform statementbot;
    public Transform balancebot;
    public Transform pinbot;
    public Transform restpoint;

    public Transform displaybot;

    private float speed = 5;

    private bool login;
    private bool recept;
    private bool withdraw;
    private bool statements;
    private bool balance;
    private bool transfer;
    private bool pin;
    private bool rest;
    private bool exit;
    public void Start()
    {
        
        gamecanvas.SetActive(false);
        DepOrWith.onValueChanged.AddListener(OnDropdownValueChanged);

        Starting();

        playerbot.Rotate(0f, 180f, 0f);

    }
    public void Starting()
    {
        //Starting everything is cleared
        atmscreen.SetActive(false);
        mainmenu.SetActive(false);
        balance_screen.SetActive(false);
        sentmoney.SetActive(false);
        sendmoneyout.SetActive(false);
        statement.SetActive(false);
        pinchangescreen.SetActive(false);
        loginid.text = "";
        pinnumber.text = "";
        sentaccdisplay_text.text = "";
        sentmoneydisplay_text.text = "";
        sent_acc = sentaccdisplay_text.text;
        sent_money = sentmoneydisplay_text.text;
        sentstatusdisplay_text.text = "";
        pinchangetext.text = "";
        OTP.text = "";
        flag = 0;  
        loginstay = false;
        display.SetActive(false);
        depam = "";
        depamount.text = "";
        withdrawScreen.SetActive(false);
    }

    public void banking()
    {
        screen.SetActive(true);
        atmscreen.SetActive(true);
        userlabel.SetActive(true);
        pinlabel.SetActive(true);
        pinchangescreen.SetActive(false);
    }

    public void One()
    {
        if (flag == 1)
        {
            if (sentaccdisplay_text.text.Length < 10)
            {
                sentaccdisplay_text.text = sentaccdisplay_text.text + "1";
            }
            else
            {
                sentmoneydisplay_text.text = sentmoneydisplay_text.text + "1";
            }
        }
        
        else if (flag == 2)
        {
            if (OTP.text.Length < 4)
            {
                OTP.text = OTP.text + "1";
            }
            else
            {
                pinchangetext.text = pinchangetext.text + "1";
            }
        }

        else if (flag == 3)
        {
            
            depamount.text = depamount.text + "1";
            
        }

        else
        {
            if (loginid.text.Length < 4)
            {
                loginid.text = loginid.text + "1";
            }
            else
            {
                pinnumber.text = pinnumber.text + "X";
                pinno = pinno + "1";
            }
        }
    }

    public void Two()
    {
        if (flag == 1)
        {
            if (sentaccdisplay_text.text.Length < 10)
            {
                sentaccdisplay_text.text = sentaccdisplay_text.text + "2";
            }
            else
            {
                sentmoneydisplay_text.text = sentmoneydisplay_text.text + "2";
            }
        }

        else if (flag == 2)
        {
            if (OTP.text.Length < 4)
            {
                OTP.text = OTP.text + "2";
            }
            else
            {
                pinchangetext.text = pinchangetext.text + "2";
            }
        }
        else if (flag == 3)
        {

            depamount.text = depamount.text + "2";

        }

        else
        {
            if (loginid.text.Length < 4)
            {
                loginid.text = loginid.text + "2";
            }
            else
            {
                pinnumber.text = pinnumber.text + "X";
                pinno = pinno + "2";
            }
        }
    }

    public void Three()
    {
        if (flag == 1)
        {
            if (sentaccdisplay_text.text.Length < 10)
            {
                sentaccdisplay_text.text = sentaccdisplay_text.text + "3";
            }
            else
            {
                sentmoneydisplay_text.text = sentmoneydisplay_text.text + "3";
            }
        }

        else if (flag == 2)
        {
            if (OTP.text.Length < 4)
            {
                OTP.text = OTP.text + "3";
            }
            else
            {
                pinchangetext.text = pinchangetext.text + "3";
            }
        }
        else if (flag == 3)
        {

            depamount.text = depamount.text + "3";

        }

        else
        {
            if (loginid.text.Length < 4)
            {
                loginid.text = loginid.text + "3";
            }
            else
            {
                pinnumber.text = pinnumber.text + "X";
                pinno = pinno + "3";
            }
        }
    }

    public void Four()
    {
        if (flag == 1)
        {
            if (sentaccdisplay_text.text.Length < 10)
            {
                sentaccdisplay_text.text = sentaccdisplay_text.text + "4";
            }
            else
            {
                sentmoneydisplay_text.text = sentmoneydisplay_text.text + "4";
            }
        }

        else if (flag == 2)
        {
            if (OTP.text.Length < 4)
            {
                OTP.text = OTP.text + "4";
            }
            else
            {
                pinchangetext.text = pinchangetext.text + "4";
            }
        }
        else if (flag == 3)
        {

            depamount.text = depamount.text + "4";

        }

        else
        {
            if (loginid.text.Length < 4)
            {
                loginid.text = loginid.text + "4";
            }
            else
            {
                pinnumber.text = pinnumber.text + "X";
                pinno = pinno + "4";
            }
        }
    }

    public void Five()
    {
        if (flag == 1)
        {
            if (sentaccdisplay_text.text.Length < 10)
            {
                sentaccdisplay_text.text = sentaccdisplay_text.text + "5";
            }
            else
            {
                sentmoneydisplay_text.text = sentmoneydisplay_text.text + "5";
            }
        }

        else if (flag == 2)
        {
            if (OTP.text.Length < 4)
            {
                OTP.text = OTP.text + "5";
            }
            else
            {
                pinchangetext.text = pinchangetext.text + "5";
            }
        }

        else if (flag == 3)
        {

            depamount.text = depamount.text + "5";

        }

        else
        {
            if (loginid.text.Length < 4)
            {
                loginid.text = loginid.text + "5";
            }
            else
            {
                pinnumber.text = pinnumber.text + "X";
                pinno = pinno + "5";
            }
        }
    }

    public void Six()
    {
        if (flag == 1)
        {
            if (sentaccdisplay_text.text.Length < 10)
            {
                sentaccdisplay_text.text = sentaccdisplay_text.text + "6";
            }
            else
            {
                sentmoneydisplay_text.text = sentmoneydisplay_text.text + "6";
            }
        }

        else if (flag == 2)
        {
            if (OTP.text.Length < 4)
            {
                OTP.text = OTP.text + "6";
            }
            else
            {
                pinchangetext.text = pinchangetext.text + "6";
            }
        }

        else if (flag == 3)
        {

            depamount.text = depamount.text + "6";

        }

        else
        {
            if (loginid.text.Length < 4)
            {
                loginid.text = loginid.text + "6";
            }
            else
            {
                pinnumber.text = pinnumber.text + "X";
                pinno = pinno + "6";
            }
        }
    }

    public void Seven()
    {
        if (flag == 1)
        {
            if (sentaccdisplay_text.text.Length < 10)
            {
                sentaccdisplay_text.text = sentaccdisplay_text.text + "7";
            }
            else
            {
                sentmoneydisplay_text.text = sentmoneydisplay_text.text + "7";
            }
        }

        else if (flag == 2)
        {
            if (OTP.text.Length < 4)
            {
                OTP.text = OTP.text + "7";
            }
            else
            {
                pinchangetext.text = pinchangetext.text + "7";
            }
        }

        else if (flag == 3)
        {

            depamount.text = depamount.text + "7";

        }

        else
        {
            if (loginid.text.Length < 4)
            {
                loginid.text = loginid.text + "7";
            }
            else
            {
                pinnumber.text = pinnumber.text + "X";
                pinno = pinno + "7";
            }
        }
    }

    public void Eight()
    {
        if (flag == 1)
        {
            if (sentaccdisplay_text.text.Length < 10)
            {
                sentaccdisplay_text.text = sentaccdisplay_text.text + "8";
            }
            else
            {
                sentmoneydisplay_text.text = sentmoneydisplay_text.text + "8";
            }
        }

        else if (flag == 2)
        {
            if (OTP.text.Length < 4)
            {
                OTP.text = OTP.text + "8";
            }
            else
            {
                pinchangetext.text = pinchangetext.text + "8";
            }
        }

        else if (flag == 3)
        {

            depamount.text = depamount.text + "8";

        }

        else
        {
            if (loginid.text.Length < 4)
            {
                loginid.text = loginid.text + "8";
            }
            else
            {
                pinnumber.text = pinnumber.text + "X";
                pinno = pinno + "8";
            }
        }
    }

    public void Nine()
    {
        if (flag == 1)
        {
            if (sentaccdisplay_text.text.Length < 10)
            {
                sentaccdisplay_text.text = sentaccdisplay_text.text + "9";
            }
            else
            {
                sentmoneydisplay_text.text = sentmoneydisplay_text.text + "9";
            }
        }

        else if (flag == 2)
        {
            if (OTP.text.Length < 4)
            {
                OTP.text = OTP.text + "9";
            }
            else
            {
                pinchangetext.text = pinchangetext.text + "9";
            }
        }

        else if (flag == 3)
        {

            depamount.text = depamount.text + "9";

        }

        else
        {
            if (loginid.text.Length < 4)
            {
                loginid.text = loginid.text + "9";
            }
            else
            {
                pinnumber.text = pinnumber.text + "X";
                pinno = pinno + "9";
            }
        }
    }

    public void Zero()
    {
        if (flag == 1)
        {
            if (sentaccdisplay_text.text.Length < 10)
            {
                sentaccdisplay_text.text = sentaccdisplay_text.text + "0";
            }
            else
            {
                sentmoneydisplay_text.text = sentmoneydisplay_text.text + "0";
            }
        }

        else if (flag == 2)
        {
            if (OTP.text.Length < 4)
            {
                OTP.text = OTP.text + "0";
            }
            else
            {
                pinchangetext.text = pinchangetext.text + "0";
            }
        }

        else if (flag == 3)
        {

            depamount.text = depamount.text + "0";

        }

        else
        {
            if (loginid.text.Length < 4)
            {
                loginid.text = loginid.text + "0";
            }
            else
            {
                pinnumber.text = pinnumber.text + "X";
                pinno = pinno + "0";
            }
        }
    }

    public void Clearing()
    {
        loginid.text = "";
        pinnumber.text = "";
        sentmoneydisplay_text.text = "";
        sentaccdisplay_text.text = "";
        pinchangetext.text = "";
        OTP.text = "";
        depamount.text = "";
        pinno = "";
    }

    public void Cncl()
    {
        loginid.text = "";
        pinnumber.text = "";
        userid = "";
        sentaccdisplay_text.text = "";
        sentmoneydisplay_text.text = "";
        sent_acc = sentaccdisplay_text.text;
        sent_money = sentmoneydisplay_text.text;
        sentstatusdisplay_text.text = "";
        pinchangetext.text = "";
        OTP.text = "";
        depamount.text = "";
        depam = "";
        pinno = "";
        Starting();
    }

    public void Login()
    {
        atmscreen.SetActive(false);
        userid = loginid.text;
        loginid.text = "";
        pinnumber.text = "";

        Debug.Log("Login Done");

        StartCoroutine(loginrequest(userid, pinno));

    }

    IEnumerator loginrequest(string data, string pin)
    {
        string url = "http://azhagesan.pythonanywhere.com/login?CustomerId=" + data + "&Pin=" + pin;
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            response_data = request.downloadHandler.text;
            Debug.Log(response_data);
            LoginResponse received_one = JsonUtility.FromJson<LoginResponse>(response_data);
            if (received_one.Code != 2)
            {
                loginstay = false;
                Debug.Log("Failed");
            }
            else
            {
                mainmenulabel.text = "Welcome " + received_one.data;
                Debug.Log(mainmenulabel.text);
                door1.SetActive(false);
                door2.SetActive(false);

                StartCoroutine(Delaysecondafter(3));
                
                gamecanvas.SetActive(true);
                OtherService.SetActive(false);
                StartCoroutine(CashReq(userid, pinno));
            }


        }
        else
        {
            Debug.LogError("API request failed: " + request.error);
            banking();
        }
    }

    [System.Serializable]

    public class LoginResponse
    {
        public bool login;
        public string data;
        public int Code;
    }

    public class BalanceResponse
    {
        public bool login;
        public string data;
        public int Code;
    }
    IEnumerator GetBalance(string data, string pin)
    {
        string url = "http://azhagesan.pythonanywhere.com/balance?CustomerId=" + data + "&Pin=" + pin;
        Debug.Log(url);
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string json = www.downloadHandler.text;
            BalanceResponse response = JsonUtility.FromJson<BalanceResponse>(json);
            Debug.Log(response.data);
            if (response.login)
            {
                balance_display_text.text = "";
                balance_screen.SetActive(true);
                screen.SetActive(true);
                balance_display_text.text = "Balance: " + float.Parse(response.data);
                display.SetActive(true);
                StartCoroutine(Delaysecond(3));
                
            }
            else
            {
                Debug.Log("Access denied. Error code: " + response.Code + " Error message: " + response.data);
                balance_screen.SetActive(true);
                balance_display_text.text = "Error displaying message";
            }
        }
    }

    public void BalanceClicked()
    {
        mainmenu.SetActive(false);
        Debug.Log(userid);
        Debug.Log(pinno);
        Debug.Log("Credentials");
        StartCoroutine(GetBalance(userid, pinno));
    }


    public void SentMoneyclicked()
    {
        sentmoney.SetActive(true);
        mainmenu.SetActive(false);
        flag = 1;

    }

    public void Sendingmoney()
    {
        sentmoney.SetActive(false );
        sent_acc = sentaccdisplay_text.text;
        sent_money = sentmoneydisplay_text.text;
        StartCoroutine(SendMoney(receiverAccount: sent_acc, pin: pinno, amount: sent_money, customerId: userid));

    }


    public class SendMoneyResponse
    {
        public bool login;
        public string data;
        public int Code;
    }


    IEnumerator SendMoney(string receiverAccount, string pin, string amount, string customerId)
    {
        string url = "http://azhagesan.pythonanywhere.com/sendmoney?ReceiverAccount=" + receiverAccount
                    + "&pin=" + pin
                    + "&amount=" + amount
                    + "&CustomerId=" + customerId;

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();
        sendmoneyout.SetActive(true);

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            string responseText = request.downloadHandler.text;
            SendMoneyResponse response1 = JsonUtility.FromJson<SendMoneyResponse>(responseText);

            if (response1.login)
            {
                Debug.Log("Money Sent Successful");
                display.SetActive(true);
                sentstatusdisplay_text.text = response1.data;
                sentmoney.SetActive(false);
                StartCoroutine(Delaysecond(3));
            }
            else
            {
                sentstatusdisplay_text.text = response1.data;
                Debug.Log(response1.data);
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

    IEnumerator GetStatement(string customerId, string pin)
    {
        string url = "http://azhagesan.pythonanywhere.com/statement?CustomerId=" + customerId
                    + "&Pin=" + pin;

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            string responseText = request.downloadHandler.text;
            StatementResponse response2 = JsonUtility.FromJson<StatementResponse>(responseText);

            if (response2.login)
            {
                statement.SetActive(true);
                display.SetActive(true);
                statementtext.text = "";
                Debug.Log(response2.data.Count);
                if (response2.data.Count > 6)
                {
                    for (int i = response2.data.Count - 1; i > response2.data.Count - 6; i--)
                    {
                        statementtext.text = statementtext.text + response2.data[i].time + "|  Amount: " + response2.data[i].amount + "|  Type of transaction: " + response2.data[i].type + "\n\n";

                        Debug.Log(response2.data[i].time + "Amount: " + response2.data[i].amount + "Type of transaction" + response2.data[i].type);
                    }
                }
                else
                {
                    for (int i = response2.data.Count - 1; i < response2.data.Count; i++)
                    {
                        statementtext.text = statementtext.text + response2.data[i].time + "|  Amount: " + response2.data[i].amount + "|  Type of transaction: " + response2.data[i].type + "\n\n";

                        Debug.Log(response2.data[i].time + "Amount: " + response2.data[i].amount + "Type of transaction" + response2.data[i].type);
                    }
                }
                StartCoroutine(Delaysecond(3));

            }
            else
            {
                Debug.Log("Failed: " + response2.data);
            }
        }
    }
    public void StatementClicked()
    {
        mainmenu.SetActive(false);
        StartCoroutine(GetStatement(customerId: userid, pin:pinno));
    }


    public void Onclickchangepin()
    {
        mainmenu.SetActive(false);
        flag = 2;
        StartCoroutine(RequestOTP(customerId: userid, pin: pinno));
    }

    public class RequestOTPResponse
    {
        public bool login;
        public string data;
        public int Code;
    }

    IEnumerator RequestOTP(string customerId, string pin)
    {
        string url = "http://azhagesan.pythonanywhere.com/change_pin/req_otp?CustomerId=" + customerId + "&Pin=" + pin;
        Debug.Log(url);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();
            


            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Request failed: " + webRequest.error);
            }
            else
            {
                string responseText = webRequest.downloadHandler.text;
                Debug.Log(responseText);
                RequestOTPResponse response5 = JsonUtility.FromJson<RequestOTPResponse>(responseText);
                display.SetActive(true);
                identity = response5.data;
                pinchangescreen.SetActive(true);
            }
        }
    }


    public void Onclicksubmitchangepin()
    {
        newpin = pinchangetext.text;
        otp = OTP.text;
        StartCoroutine(VerifyChangePin(otp: otp, id: identity, newPin: newpin));
    }

    public class ChangePin
    {
        public bool login;
        public string data;
        public int Code;
    }

    IEnumerator VerifyChangePin(string otp, string id, string newPin)
    {
        string url = "http://azhagesan.pythonanywhere.com/change_pin?OTP=" + otp + "&ID=" + id + "&PIN=" + newPin;

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Request failed: " + webRequest.error);
            }
            else
            {
                pinchangescreen.SetActive(false);
                string responseText = webRequest.downloadHandler.text;
                ChangePin response6 = JsonUtility.FromJson<ChangePin>(responseText);
                Debug.Log(response6.data);
                
            }
        }
    }


    public class DepositResponse
    {
        public bool login;
        public string data;
        public int Code;
    }

    IEnumerator DepositMoney(string customerId, string amount)
    {
        string url = "http://azhagesan.pythonanywhere.com/deposit?CustomerId=" + customerId
                    + "&Amount=" + amount;

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            string responseText = request.downloadHandler.text;
            DepositResponse response6 = JsonUtility.FromJson<DepositResponse>(responseText);

            if (response6.login)
            {
                screen.SetActive(true);
                balance_screen.SetActive(true);
                display.SetActive(true);
                balance_display_text.text = response6.data;
                balance_display_text.text = response6.data;
                StartCoroutine(CashReq(userid, pinno));
                StartCoroutine(Delaysecond(3));

            }
            else
            {
                Debug.Log("Request Failed: "+ response6.data);
            }
        }
    }


    private void OnClickWithdraw(string Amount)
    {
        StartCoroutine(WithdrawMoney(customerId:userid, amount:Amount));
    }


    public class WithdrawResponse
    {
        public bool login;
        public string data;
        public int Code;
    }

    IEnumerator WithdrawMoney(string customerId, string amount)
    {
        string url = "http://azhagesan.pythonanywhere.com/withdraw?CustomerId=" + customerId
                    + "&Amount=" + amount;
        

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            string responseText = request.downloadHandler.text;
            WithdrawResponse response7 = JsonUtility.FromJson<WithdrawResponse>(responseText);

            if (response7.login)
            {
                screen.SetActive(true);
                balance_screen.SetActive(true);
                display.SetActive(true);
                balance_display_text.text = response7.data;
                StartCoroutine(CashReq(userid, pinno));
                StartCoroutine(Delaysecond(3));

            }
            else
            {
                Debug.Log("Request Failed: "+ response7.data);
            }
        }
    }


    private void OnClickDeposit(string Amount)
    {
        StartCoroutine(DepositMoney(customerId: userid, amount: Amount));
    }

    public void submit()
    {
        if (selectedOption == "Deposit")
        {
            depam = depamount.text;
            Debug.Log(depam);
            OnClickDeposit(Amount: depam);
        }
        if (selectedOption == "Withdraw")
        {
            depam = depamount.text;
            OnClickWithdraw(Amount: depam);
        }
    }

    private void OnDropdownValueChanged(int index)
    {
        selectedOption = DepOrWith.options[index].text;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Transfer"))
        {
            Starting();
            Debug.Log("Transfer");
            SentMoneyclicked();
            
        }
        else if(other.CompareTag("Change_pin"))
        {
            Starting();
            Debug.Log("Change pin");
            Onclickchangepin();
        }
        else if (other.CompareTag("Balance"))
        {
            Starting();
            Debug.Log("Balance");
            BalanceClicked();
        }
        else if (other.CompareTag("Withdraw"))
        {
            Starting();
            Debug.Log("Withdraw");
            flag = 3;
            withdrawScreen.SetActive(true);

        }
        else if (other.CompareTag("Statement"))
        {
            Starting();
            Debug.Log("Statement");
            StatementClicked();
        }
        else if (other.CompareTag("Enter"))
        {
            Starting();
            Debug.Log("Enter");
            EnterBank.SetActive(true);
        }
    }

    public void EnterbankClicked()
    {
        Delaysecondatstartin();
        login = true;
        EnterBank.SetActive(false);
    }

    private void Delaysecondatstartin()
    {
      
            playerbot.Rotate(0f, 180f, 0f);
        
        
    }

    public class cashResponse
    {
        public bool login;
        public string data;
        public int Code;
    }

    IEnumerator CashReq(string customerId, string pin)
    {
        string url = "http://azhagesan.pythonanywhere.com/cash?CustomerId=" + customerId
                    + "&Pin=" + pin;

        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log(request.error);
        }
        else
        {
            string responseText = request.downloadHandler.text;
            cashResponse response8 = JsonUtility.FromJson<cashResponse>(responseText);

            if (response8.login)
            {
                cashtext.text ="Cash in wallet: " + response8.data;

            }
            else
            {
                Debug.Log("Request Failed: " + response8.data);
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("login") && loginstay == false)
        {
            Debug.Log("Login");
            banking();
            loginstay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("login") && loginstay == false)
        {
            Debug.Log("Login");
            loginstay= false;
        }
    }

    //Player Movement script
    private void Update()
    {
        if (recept)
        {
            if (playerTrans.position == receptiontrans.position)
            {
                playerAnim.ResetTrigger("walk");
                playerAnim.SetTrigger("idle");
                recept = false;
                transform.LookAt(receptbot);
                mainmenu.SetActive(true);
                OtherService.SetActive(false);
            }
            else
            {
                playerAnim.SetTrigger("walk");
                playerAnim.ResetTrigger("idle");
                transform.LookAt(receptiontrans);
                transform.position = Vector3.MoveTowards(transform.position, receptiontrans.position, speed * Time.deltaTime);
            }
        }

        if (login)
        {
            if (playerTrans.position == logintrans.position)
            {
                playerAnim.ResetTrigger("walk");
                playerAnim.SetTrigger("idle");
                login = false;
            }
            else
            {
                playerAnim.SetTrigger("walk");
                playerAnim.ResetTrigger("idle");
                transform.LookAt(logintrans);
                transform.position = Vector3.MoveTowards(transform.position, logintrans.position, speed * Time.deltaTime);
            }
        }

        if (balance)
        {
            if (playerTrans.position == balancetrans.position)
            {
                playerAnim.ResetTrigger("walk");
                playerAnim.SetTrigger("idle");
                balance = false;
                transform.LookAt(balancebot);
                OtherService.SetActive(true);
            }
            else
            {
                playerAnim.SetTrigger("walk");
                playerAnim.ResetTrigger("idle");
                transform.LookAt(balancetrans);
                transform.position = Vector3.MoveTowards(transform.position, balancetrans.position, speed * Time.deltaTime);
            }
        }

        if (withdraw)
        {
            if (playerTrans.position == withdrawtrans.position)
            {
                playerAnim.ResetTrigger("walk");
                playerAnim.SetTrigger("idle");
                withdraw = false;
                transform.LookAt(withdrawbot);
                OtherService.SetActive(true);
            }
            else
            {
                playerAnim.SetTrigger("walk");
                playerAnim.ResetTrigger("idle");
                transform.LookAt(withdrawtrans);
                transform.position = Vector3.MoveTowards(transform.position, withdrawtrans.position, speed * Time.deltaTime);
            }
        }

        if (statements)
        {
            if (playerTrans.position == statementtrans.position)
            {
                playerAnim.ResetTrigger("walk");
                playerAnim.SetTrigger("idle");
                statements = false;
                transform.LookAt(statementbot);
                OtherService.SetActive(true);
            }
            else
            {
                playerAnim.SetTrigger("walk");
                playerAnim.ResetTrigger("idle");
                transform.LookAt(statementtrans);
                transform.position = Vector3.MoveTowards(transform.position, statementtrans.position, speed * Time.deltaTime);
            }
        }

        if (transfer)
        {
            if (playerTrans.position == transfertrans.position)
            {
                playerAnim.ResetTrigger("walk");
                playerAnim.SetTrigger("idle");
                transfer = false;
                transform.LookAt(transferbot);
                OtherService.SetActive(true);
            }
            else
            {
                playerAnim.SetTrigger("walk");
                playerAnim.ResetTrigger("idle");
                transform.LookAt(transfertrans);
                transform.position = Vector3.MoveTowards(transform.position, transfertrans.position, speed * Time.deltaTime);
            }
        }

        if (pin)
        {
            if (playerTrans.position == pintrans.position)
            {
                playerAnim.ResetTrigger("walk");
                playerAnim.SetTrigger("idle");
                pin = false;
                transform.LookAt(pinbot);
                OtherService.SetActive(true);
            }
            else
            {
                playerAnim.SetTrigger("walk");
                playerAnim.ResetTrigger("idle");
                transform.LookAt(pintrans);
                transform.position = Vector3.MoveTowards(transform.position, pintrans.position, speed * Time.deltaTime);
            }
        }
        if (rest)
        {
            if (playerTrans.position == restpoint.position)
            {
                playerAnim.ResetTrigger("walk");
                playerAnim.SetTrigger("idle");
                rest = false;
                transform.LookAt(displaybot);
                OtherService.SetActive(true);
            }
            else
            {
                playerAnim.SetTrigger("walk");
                playerAnim.ResetTrigger("idle");
                transform.LookAt(restpoint);
                transform.position = Vector3.MoveTowards(transform.position, restpoint.position, speed * Time.deltaTime);
            }
        }

        if (exit)
        {
            if (playerTrans.position == logintrans.position)
            {
                playerAnim.ResetTrigger("walk");
                playerAnim.SetTrigger("idle");
                exit = false;
                door1.SetActive(true);
                door2.SetActive(true);
                Cncl();
                gamecanvas.SetActive(false);
                transform.LookAt(displaybot);
                OtherService.SetActive(false);
            }
            else
            {
                playerAnim.SetTrigger("walk");
                playerAnim.ResetTrigger("idle");
                transform.LookAt(logintrans);
                transform.position = Vector3.MoveTowards(transform.position, logintrans.position, speed * Time.deltaTime);
            }
        }

    }

    public void clickedservice()
    {
        recept = true;
    }

    public void clickedexit()
    {
        exit = true;
    }

    public void movebalance()
    {
        balance = true;
    }

    public void movewithdraw()
    {
        withdraw = true;
    }

    public void movestatement()
    {
        statements = true;
    }

    public void movetransfer()
    {
        transfer = true;
    }
    public void movepin()
    {
        pin = true;
    }
    
    public void moverest()
    {
        rest = true;
    }
    
    IEnumerator Delaysecond(int i)
    {
        yield return new WaitForSeconds(i);
        Debug.Log("Waited");
        moverest();
    }
    IEnumerator Delaysecondafter(int i)
    {
        yield return new WaitForSeconds(i);
        Debug.Log("Waited");
        recept = true;
    }

}

