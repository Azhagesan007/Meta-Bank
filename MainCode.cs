using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Unity.Netcode;
using UnityEngine.InputSystem.Composites;
using Unity.VisualScripting;

public class MainCode : NetworkBehaviour
{

    private string DOMAIN_URL = "http://azhagesan.pythonanywhere.com";
    //public GameObject OtherService;
    //public GameObject EnterBank;
    //public GameObject screen;

    public GameObject display;
    private GameObject loginscreen;
    private GameObject mainmenu;
    private GameObject balance_screen;
    private GameObject sentmoney;
    private GameObject sendmoneyout;
    private GameObject statement;
    private GameObject pinchangescreen;
    //public GameObject door1;
    //public GameObject door2;
    private GameObject gamecanvas;
    private GameObject keypadInput;
    private GameObject output;

    private GameObject withdrawScreen;
    private Text depamount;
    private Text loginid;
    private Text pinnumber;
    private Text balance_display_text;
    private Text sentaccdisplay_text;
    private Text sentmoneydisplay_text;
    private Text sentstatusdisplay_text;
    private Text statementtext;
    private Text OTP;
    private Text pinchangetext;
    private Text cashtext;
    private Dropdown DepOrWith;
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


    private Animator playerAnim;
    //public Rigidbody playerRigid;

    //public float w_speed, wb_speed, olw_speed, rn_speed, ro_speed;
    //public bool walking;

    private Transform playerTrans;
    private Transform playerbot;


    private Transform receptiontrans;
    private Transform logintrans;
    private Transform withdrawtrans;
    private Transform statementtrans;
    private Transform balancetrans;
    private Transform transfertrans;
    private Transform pintrans;

    private Transform loginbot;
    private Transform receptbot;
    private Transform transferbot;
    private Transform withdrawbot;
    private Transform statementbot;
    private Transform balancebot;
    private Transform pinbot;
    private Transform restpoint;

    //public Transform displaybot;

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

    
    //For buttons
    private Button one_btn;
    private Button two_btn;
    private Button three_btn;
    private Button four_btn;
    private Button five_btn;
    private Button six_btn;   
    private Button seven_btn;
    private Button eight_btn;
    private Button nine_btn;
    private Button zero_btn;
    private Button clc_btn;
    private Button cncl_btn;
    private Button login_btn;
    private Button withdraw_btn;
    private Button transfer_btn;
    private Button pin_btn;
    private Button withdraw_menu_btn;
    private Button transfer_menu_btn;
    private Button pin_menu_btn;
    private Button balance_menu_btn;
    private Button statement_menu_btn;
    private Button service_btn;
    private Button exit_btn;

    KeypadData keypad = new KeypadData();
    public void Start()
    {
        if (!IsOwner) return;

        transform.position = new Vector3(-927.48f, 13.21f, 412.74f);

        getMyGameObjects();

        gamecanvas.SetActive(false);


        //button1 = GameObject.FindWithTag("input_DeporWith").GetComponent<Button>();
        //button1.onClick.AddListener(Two);

        transform.Rotate(0f, 120f, 0f);
        

        startListeners();
        Starting();


    }

    private void startListeners()
    {
        if (!IsOwner) return;
        DepOrWith.onValueChanged.AddListener(OnDropdownValueChanged);
        one_btn.onClick.AddListener(One);
        two_btn.onClick.AddListener(Two);
        three_btn.onClick.AddListener(Three);
        four_btn.onClick.AddListener(Four);
        five_btn.onClick.AddListener(Five);
        six_btn.onClick.AddListener(Six);
        seven_btn.onClick.AddListener(Seven);
        eight_btn.onClick.AddListener(Eight);
        nine_btn.onClick.AddListener(Nine);
        zero_btn.onClick.AddListener(Zero);
        clc_btn.onClick.AddListener(Clearing);
        cncl_btn.onClick.AddListener(Cncl);
        login_btn.onClick.AddListener(Login);
        withdraw_btn.onClick.AddListener(submit);
        transfer_btn.onClick.AddListener(Sendingmoney);
        pin_btn.onClick.AddListener(Onclicksubmitchangepin);
        withdraw_menu_btn.onClick.AddListener(movewithdraw);
        transfer_menu_btn.onClick.AddListener(movetransfer);
        pin_menu_btn.onClick.AddListener(movepin);
        balance_menu_btn.onClick.AddListener(movebalance);
        statement_menu_btn.onClick.AddListener(movestatement);
        service_btn.onClick.AddListener(clickedservice);
        exit_btn.onClick.AddListener(clickedexit);

    }

    private void getMyGameObjects()
    {
        if (!IsOwner) return;

        //if (!IsOwner) return;
        receptiontrans = GameObject.FindGameObjectsWithTag("menu").Last().transform;
        logintrans = GameObject.FindGameObjectsWithTag("login").Last().transform;
        withdrawtrans = GameObject.FindGameObjectsWithTag("Withdraw").Last().transform;
        statementtrans = GameObject.FindGameObjectsWithTag("Statement").Last().transform;
        balancetrans = GameObject.FindGameObjectsWithTag("Balance").Last().transform;
        transfertrans = GameObject.FindGameObjectsWithTag("Transfer").Last().transform;
        pintrans = GameObject.FindGameObjectsWithTag("Change_pin").Last().transform;

        //For bots
        loginbot = GameObject.FindGameObjectsWithTag("statement_bot").Last().transform;
        receptbot = GameObject.FindGameObjectsWithTag("reception_bot").Last().transform;
        transferbot = GameObject.FindGameObjectsWithTag("transfer_bot").Last().transform;
        withdrawbot = GameObject.FindGameObjectsWithTag("withdraw_bot").Last().transform;
        statementbot = GameObject.FindGameObjectsWithTag("statement_bot").Last().transform;
        balancebot = GameObject.FindGameObjectsWithTag("balance_bot").Last().transform;
        pinbot = GameObject.FindGameObjectsWithTag("pin_bot").Last().transform;
        restpoint = GameObject.FindGameObjectsWithTag("rest").Last().transform;
        //loginbot = GameObject.FindGameObjectsWithTag("hi").Last().transform;

        //for player avatar
        playerTrans = GameObject.FindGameObjectsWithTag("Player").Last().transform;
        playerbot = GameObject.FindGameObjectsWithTag("Character").Last().transform;
        playerAnim = GameObject.FindGameObjectsWithTag("Character").Last().GetComponent<Animator>();

        //Getting all Screens
        display = GameObject.FindGameObjectsWithTag("screen").Last();
        loginscreen = GameObject.FindGameObjectsWithTag("login_screen").Last();
        mainmenu = GameObject.FindGameObjectsWithTag("mainmenu").Last();
        balance_screen = GameObject.FindGameObjectsWithTag("screen").Last();
        sentmoney = GameObject.FindGameObjectsWithTag("tranfer_screen").Last();
        sendmoneyout = GameObject.FindGameObjectsWithTag("screen").Last();
        statement = GameObject.FindGameObjectsWithTag("screen").Last();
        pinchangescreen = GameObject.FindGameObjectsWithTag("otp_screen").Last();
        gamecanvas = GameObject.FindGameObjectsWithTag("gamecanvas").Last();
        withdrawScreen = GameObject.FindGameObjectsWithTag("withdraw_screen").Last();
        keypadInput = GameObject.FindGameObjectsWithTag("keypad").Last();
        output = GameObject.FindGameObjectsWithTag("output").Last();


        //get all inputs
        depamount = GameObject.FindGameObjectsWithTag("input_dep_amount").Last().GetComponent<Text>();
        loginid = GameObject.FindGameObjectsWithTag("input_user").Last().GetComponent<Text>();
        pinnumber = GameObject.FindGameObjectsWithTag("input_pin").Last().GetComponent<Text>();
        balance_display_text = GameObject.FindGameObjectsWithTag("output").Last().GetComponent<Text>();
        sentaccdisplay_text = GameObject.FindGameObjectsWithTag("input_receiver_accountNo").Last().GetComponent<Text>();
        sentmoneydisplay_text = GameObject.FindGameObjectsWithTag("input_TransferAmount").Last().GetComponent<Text>();
        sentstatusdisplay_text = GameObject.FindGameObjectsWithTag("output").Last().GetComponent<Text>();
        statementtext = GameObject.FindGameObjectsWithTag("output").Last().GetComponent<Text>();
        OTP = GameObject.FindGameObjectsWithTag("input_OTP").Last().GetComponent<Text>();
        pinchangetext = GameObject.FindGameObjectsWithTag("input_PinChangeNewPin").Last().GetComponent<Text>();
        cashtext = GameObject.FindGameObjectsWithTag("cash").Last().GetComponent<Text>();
        DepOrWith = GameObject.FindGameObjectsWithTag("input_DeporWith").Last().GetComponent<Dropdown>();


        //get all buttons
        //keypad button
        one_btn = GameObject.FindGameObjectsWithTag("one").Last().GetComponent<Button>();
        two_btn = GameObject.FindGameObjectsWithTag("two").Last().GetComponent<Button>();
        three_btn = GameObject.FindGameObjectsWithTag("three").Last().GetComponent<Button>();
        four_btn = GameObject.FindGameObjectsWithTag("four").Last().GetComponent<Button>();
        five_btn = GameObject.FindGameObjectsWithTag("five").Last().GetComponent<Button>();
        six_btn = GameObject.FindGameObjectsWithTag("six").Last().GetComponent<Button>();
        seven_btn = GameObject.FindGameObjectsWithTag("seven").Last().GetComponent<Button>();
        eight_btn = GameObject.FindGameObjectsWithTag("eight").Last().GetComponent<Button>();
        nine_btn = GameObject.FindGameObjectsWithTag("nine").Last().GetComponent<Button>();
        zero_btn = GameObject.FindGameObjectsWithTag("zero").Last().GetComponent<Button>();
        clc_btn = GameObject.FindGameObjectsWithTag("cls").Last().GetComponent<Button>();
        cncl_btn = GameObject.FindGameObjectsWithTag("cncl").Last().GetComponent<Button>();

        //other button
        login_btn = GameObject.FindGameObjectsWithTag("login_btn").Last().GetComponent<Button>();
        withdraw_btn = GameObject.FindGameObjectsWithTag("withdraw_btn").Last().GetComponent<Button>();
        transfer_btn = GameObject.FindGameObjectsWithTag("transfer_btn").Last().GetComponent<Button>();
        pin_btn = GameObject.FindGameObjectsWithTag("pin_btn").Last().GetComponent<Button>();
        withdraw_menu_btn = GameObject.FindGameObjectsWithTag("withdraw_menu").Last().GetComponent<Button>();
        transfer_menu_btn = GameObject.FindGameObjectsWithTag("transfer_menu").Last().GetComponent<Button>();
        pin_menu_btn = GameObject.FindGameObjectsWithTag("pin_menu").Last().GetComponent<Button>();
        balance_menu_btn = GameObject.FindGameObjectsWithTag("balance_menu").Last().GetComponent<Button>();
        statement_menu_btn = GameObject.FindGameObjectsWithTag("statement_menu").Last().GetComponent<Button>();
        service_btn = GameObject.FindGameObjectsWithTag("service_btn").Last().GetComponent<Button>();
        exit_btn = GameObject.FindGameObjectsWithTag("exit_btn").Last().GetComponent<Button>();


    }

    public void Starting()
    {
        if (!IsOwner) return;
        //Starting everything is cleared
        loginscreen.SetActive(false);
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
        keypadInput.SetActive(false);
        output.SetActive(false);
    }

    public void banking()
    {
        if (!IsOwner) return;
        Starting();
        //screen.SetActive(true);
        display.SetActive(true);
        keypadInput.SetActive(true);
        loginscreen.SetActive(true);
        //userlabel.SetActive(true);
        //pinlabel.SetActive(true);
        //pinchangescreen.SetActive(false);
    }


    

    public void One()
    {
        if (!IsOwner) return;

        keypad.getKeypadData(flag:flag, sentaccdisplay_text: ref sentaccdisplay_text,sentmoneydisplay_text: ref sentmoneydisplay_text,OTP:ref OTP,pinchangetext:ref pinchangetext, depamount: ref depamount,loginid:ref loginid,pinnumber: ref pinnumber, pinno:ref pinno, num: "1");
    }

    public void Two()
    {
        if (!IsOwner) return;
        keypad.getKeypadData(flag: flag, sentaccdisplay_text: ref sentaccdisplay_text, sentmoneydisplay_text: ref sentmoneydisplay_text, OTP: ref OTP, pinchangetext: ref pinchangetext, depamount: ref depamount, loginid: ref loginid, pinnumber: ref pinnumber, pinno: ref pinno, num: "2");
    }

    public void Three()
    {
        if (!IsOwner) return;
        keypad.getKeypadData(flag: flag, sentaccdisplay_text: ref sentaccdisplay_text, sentmoneydisplay_text: ref sentmoneydisplay_text, OTP: ref OTP, pinchangetext: ref pinchangetext, depamount: ref depamount, loginid: ref loginid, pinnumber: ref pinnumber, pinno: ref pinno, num: "3");
    }

    public void Four()
    {
        if (!IsOwner) return;
        keypad.getKeypadData(flag: flag, sentaccdisplay_text: ref sentaccdisplay_text, sentmoneydisplay_text: ref sentmoneydisplay_text, OTP: ref OTP, pinchangetext: ref pinchangetext, depamount: ref depamount, loginid: ref loginid, pinnumber: ref pinnumber, pinno: ref pinno, num: "4");
    }

    public void Five()
    {
        if (!IsOwner) return;
        keypad.getKeypadData(flag: flag, sentaccdisplay_text: ref sentaccdisplay_text, sentmoneydisplay_text: ref sentmoneydisplay_text, OTP: ref OTP, pinchangetext: ref pinchangetext, depamount: ref depamount, loginid: ref loginid, pinnumber: ref pinnumber, pinno: ref pinno, num: "5");
    }

    public void Six()
    {
        if (!IsOwner) return;
        keypad.getKeypadData(flag: flag, sentaccdisplay_text: ref sentaccdisplay_text, sentmoneydisplay_text: ref sentmoneydisplay_text, OTP: ref OTP, pinchangetext: ref pinchangetext, depamount: ref depamount, loginid: ref loginid, pinnumber: ref pinnumber, pinno: ref pinno, num: "6");
    }

    public void Seven()
    {
        if (!IsOwner) return;
        keypad.getKeypadData(flag: flag, sentaccdisplay_text: ref sentaccdisplay_text, sentmoneydisplay_text: ref sentmoneydisplay_text, OTP: ref OTP, pinchangetext: ref pinchangetext, depamount: ref depamount, loginid: ref loginid, pinnumber: ref pinnumber, pinno: ref pinno, num: "7");
    }

    public void Eight()
    {
        if (!IsOwner) return;
        keypad.getKeypadData(flag: flag, sentaccdisplay_text: ref sentaccdisplay_text, sentmoneydisplay_text: ref sentmoneydisplay_text, OTP: ref OTP, pinchangetext: ref pinchangetext, depamount: ref depamount, loginid: ref loginid, pinnumber: ref pinnumber, pinno: ref pinno, num: "8");
    }

    public void Nine()
    {
        if (!IsOwner) return;
        keypad.getKeypadData(flag: flag, sentaccdisplay_text: ref sentaccdisplay_text, sentmoneydisplay_text: ref sentmoneydisplay_text, OTP: ref OTP, pinchangetext: ref pinchangetext, depamount: ref depamount, loginid: ref loginid, pinnumber: ref pinnumber, pinno: ref pinno, num: "9");
    }

    public void Zero()
    {
        if (!IsOwner) return;
        keypad.getKeypadData(flag: flag, sentaccdisplay_text: ref sentaccdisplay_text, sentmoneydisplay_text: ref sentmoneydisplay_text, OTP: ref OTP, pinchangetext: ref pinchangetext, depamount: ref depamount, loginid: ref loginid, pinnumber: ref pinnumber, pinno: ref pinno, num: "0");
    }

    public void Clearing()
    {
        if (!IsOwner) return;
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
        if (!IsOwner) return;
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
        if (!IsOwner) return;
        display.SetActive(false);
        keypadInput.SetActive(false);
        loginscreen.SetActive(false);
        userid = loginid.text;
        loginid.text = "";
        pinnumber.text = "";

        Debug.Log("Login Done");
        NetworkHelper loginHelper = new NetworkHelper(DOMAIN_URL);
        Debug.Log(userid);
        StartCoroutine(loginHelper.RequestApi(remaining_url:"/login?CustomerId=" + userid + "&Pin=" + pinno, callback: (response) => {
            Debug.Log(response);
            Debug.Log("Ithu  varaikum ok");
            if (response == "400")
            {
                loginstay = false;
                pinno = "";
                Debug.Log("Failed");
                banking();
            }
            else
            {
                
                //door1.SetActive(false);
                //door2.SetActive(false);

                StartCoroutine(Delaysecondafter(3));

                gamecanvas.SetActive(true);
                //OtherService.SetActive(false);
                StartCoroutine(CashReq(userid, pinno));
            }
        }));
    }
   
    public void BalanceClicked()
    {
        if (!IsOwner) return;
        mainmenu.SetActive(false);
        Debug.Log(userid);
        Debug.Log(pinno);
        Debug.Log("Credentials");
        NetworkHelper balanceHelper = new NetworkHelper(DOMAIN_URL);
        string remaining_url = "/balance?CustomerId=" + userid + "&Pin=" + pinno;
        StartCoroutine(balanceHelper.RequestApi(remaining_url: remaining_url, callback: (response) =>
        {
            if (response == "400")
            {
                Debug.Log("Access denied. Error message: " + response);
                balance_screen.SetActive(true);
                balance_display_text.text = "Error displaying message";
            }
            else
            {
                balance_display_text.text = "";   
                balance_screen.SetActive(true);
                //screen.SetActive(true);
                output.SetActive(true);
                balance_display_text.text = "Balance: " + float.Parse(response);
                display.SetActive(true);
                StartCoroutine(Delaysecond(3));
                StartCoroutine(outputTimer(3));
            }
        }));
    }


    public void SentMoneyclicked()
    {
        if (!IsOwner) return;
        sentmoney.SetActive(true);
        mainmenu.SetActive(false);
        flag = 1;

    }

    public void Sendingmoney()
    {
        if (!IsOwner) return;
        keypadInput.SetActive(false);
        sentmoney.SetActive(false );
        sent_acc = sentaccdisplay_text.text;
        sent_money = sentmoneydisplay_text.text;
        NetworkHelper sendMoneyHelper = new NetworkHelper(DOMAIN_URL);
        //StartCoroutine(SendMoney(receiverAccount: sent_acc, pin: pinno, amount: sent_money, customerId: userid));
        string remaining_url = "/sendmoney?ReceiverAccount=" + sent_acc + "&pin=" + pinno + "&amount=" + sent_money+ "&CustomerId=" + userid;
        StartCoroutine(sendMoneyHelper.RequestApi(remaining_url:remaining_url, callback: (response)=>
        {
            if (response == "400")
            {
                Debug.Log("Failed");
            }
            else
            {
                    Debug.Log("Money Sent Successful");
                    display.SetActive(true);
                output.SetActive(true);

                sentstatusdisplay_text.text = response;
                    sentmoney.SetActive(false);
                    StartCoroutine(Delaysecond(3)); 

                StartCoroutine(outputTimer(3));

            }
        }));
    }


    


    public void StatementClicked()
    {
        if (!IsOwner) return;
        mainmenu.SetActive(false);
        //StartCoroutine(GetStatement(customerId: userid, pin:pinno));
        NetworkHelper statementHelper = new NetworkHelper(DOMAIN_URL);
        StartCoroutine(statementHelper.RequestStatement(remaining_url: "/change_pin/req_otp?CustomerId=" + userid+ "&Pin=" + pinno, callback: (response) =>
        {
            statement.SetActive(true);
            display.SetActive(true);
            statementtext.text = "";
            Debug.Log(response.Count);
            if (response.Count > 6)
            {
                for (int i = response.Count - 1; i > response.Count - 6; i--)
                {
                    statementtext.text = statementtext.text + response[i].time + "|  Amount: " + response[i].amount + "|  Type of transaction: " + response[i].type + "\n\n";

                    Debug.Log(response[i].time + "Amount: " + response[i].amount + "Type of transaction" + response[i].type);
                }
            }
            else
            {
                for (int i = response.Count - 1; i < response.Count; i++)
                {
                    statementtext.text = statementtext.text + response[i].time + "|  Amount: " + response[i].amount + "|  Type of transaction: " + response[i].type + "\n\n";

                    Debug.Log(response[i].time + "Amount: " + response[i].amount + "Type of transaction" + response[i].type);
                }
            }
            output.SetActive(true);
            StartCoroutine(Delaysecond(3));
            StartCoroutine(outputTimer(2));


        }));
    }


    public void Onclickchangepin()
    {
        if (!IsOwner) return;
        mainmenu.SetActive(false);
        flag = 2;
        pinchangescreen.SetActive(true);
        //StartCoroutine(RequestOTP(customerId: userid, pin: pinno));
        NetworkHelper reqOtp = new NetworkHelper(DOMAIN_URL);
        StartCoroutine(reqOtp.RequestApi(remaining_url:"/change_pin/req_otp?CustomerId=" + userid + "&Pin=" + pinno, callback: (response) =>
        {
            if (response != "400")
            {
                Debug.Log("Request failed");
            }
            else
            {
                display.SetActive(true);
                identity = response;
                pinchangescreen.SetActive(true);
            }
        }));
    }
    public void Onclicksubmitchangepin()
    {
        if (!IsOwner) return;
        keypadInput.SetActive(false);
        newpin = pinchangetext.text;
        otp = OTP.text;
        //StartCoroutine(VerifyChangePin(otp: otp, id: identity, newPin: newpin));
        NetworkHelper changePin = new NetworkHelper(DOMAIN_URL);
        StartCoroutine(changePin.RequestApi(remaining_url: "/change_pin?OTP=" + otp + "&ID=" + identity, callback: (response) =>
        {
            if (response == "400")
            {
                Debug.Log("Request failed:");
            }
            else
            {
                pinchangescreen.SetActive(false);
                display.SetActive(true);
                output.SetActive(true);
                balance_display_text.text = response;
                Debug.Log(response);
                StartCoroutine(outputTimer(2));


            }
        }));
    }
    private void OnClickWithdraw(string Amount)
    {
        if (!IsOwner) return;
        //StartCoroutine(WithdrawMoney(customerId:userid, amount:Amount));
        NetworkHelper withdrawHelper = new NetworkHelper(DOMAIN_URL);
        StartCoroutine(withdrawHelper.RequestApi(remaining_url: "withdraw?CustomerId=" + userid + "&Amount=" + Amount, callback: (response) =>
        {
            if (response == "400")
            {
                Debug.Log("Failed");
            }
            else
            {
                //screen.SetActive(true);
                balance_screen.SetActive(true);
                display.SetActive(true);
                balance_display_text.text = response;
                StartCoroutine(CashReq(userid, pinno));
                StartCoroutine(Delaysecond(3));
                withdrawScreen.SetActive(false);
                StartCoroutine(outputTimer(2));

            }
        })) ;
    }


    private void OnClickDeposit(string Amount)
    {
        if (!IsOwner) return;
        //StartCoroutine(DepositMoney(customerId: userid, amount: Amount));
        NetworkHelper depositHelper = new NetworkHelper(DOMAIN_URL);
        StartCoroutine(depositHelper.RequestApi(remaining_url: "deposit?CustomerId=" + userid + "&Amount=" + Amount, callback: (response) =>
        {
            if (response == "400")
            {
                Debug.Log("Failed");
            }
            else
            {
                //screen.SetActive(true);
                balance_screen.SetActive(true);
                display.SetActive(true);
                balance_display_text.text = response;
                //balance_display_text.text = response;
                StartCoroutine(CashReq(userid, pinno));
                StartCoroutine(Delaysecond(3));
                withdrawScreen.SetActive(false);
                StartCoroutine(outputTimer(2));

            }
        })) ;
    }

    public void submit()
    {
        if (!IsOwner) return;
        keypadInput.SetActive(false);
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
        if (!IsOwner) return;
        selectedOption = DepOrWith.options[index].text;
    }


    void OnTriggerEnter(Collider other)
    {
        if (!IsOwner) return;
        if (other.CompareTag("Transfer"))
        {
            Starting();
            display.SetActive(true);
            keypadInput.SetActive(true);
            Debug.Log("Transfer");
            SentMoneyclicked();
            
        }
        else if(other.CompareTag("Change_pin"))
        {
            Starting();
            display.SetActive(true);
            Debug.Log("Change pin");
            keypadInput.SetActive(true);
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
            display.SetActive(true);
            Debug.Log("Withdraw");
            keypadInput.SetActive(true);
            flag = 3;
            withdrawScreen.SetActive(true);

        }
        else if (other.CompareTag("Statement"))
        {
            Starting();
            Debug.Log("Statement");
            StatementClicked();
        }
        /*else if (other.CompareTag("Enter"))
        {
            Starting();
            Debug.Log("Enter");
            EnterBank.SetActive(true);
        }*/
    }

    public void EnterbankClicked()
    {
        if (!IsOwner) return;
        Delaysecondatstartin();
        login = true;
        //EnterBank.SetActive(false);
    }

    private void Delaysecondatstartin()
    {
        if (!IsOwner) return;
      
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
                cashtext.text ="Wallet: " + response8.data;

            }
            else
            {
                Debug.Log("Request Failed: " + response8.data);
            }
        }
    }


    private void OnTriggerStay(Collider other)
    {
        if (!IsOwner) return;
        if (other.CompareTag("login") && loginstay == false)
        {
            Debug.Log("Login");
            banking();
            loginstay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsOwner) return;

        if (other.CompareTag("login") && loginstay == false)
        {
            Debug.Log("Login");
            loginstay= false;
        }
    }

    //Player Movement script
    private void Update()
    {
        if (!IsOwner) return;
        if (recept)
        {
            if (playerTrans.position == receptiontrans.position)
            {
                playerAnim.ResetTrigger("walk");
                playerAnim.SetTrigger("idle");
                recept = false;
                transform.LookAt(receptbot);
                transform.Rotate(0f, transform.rotation.y, 0f);
                mainmenu.SetActive(true);
                //OtherService.SetActive(false);
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
                transform.Rotate(0f, transform.rotation.y, 0f);
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
                transform.Rotate(0f, transform.rotation.y, 0f);
                //OtherService.SetActive(true);
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
                //OtherService.SetActive(true);
                transform.Rotate(0f, transform.rotation.y, 0f);
                

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
                transform.Rotate(0f, transform.rotation.y, 0f);
                //OtherService.SetActive(true);
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
                transform.Rotate(0f, transform.rotation.y, 0f);
                //OtherService.SetActive(true);
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
                transform.Rotate(0f, transform.rotation.y, 0f);
                //OtherService.SetActive(true);
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
                transform.LookAt(transfertrans);
                transform.Rotate(0f, transform.rotation.y, 0f);
                //OtherService.SetActive(true);
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
                //door1.SetActive(true);
                //door2.SetActive(true);
                Cncl();
                gamecanvas.SetActive(false);
                transform.LookAt(transfertrans);
                transform.Rotate(0f, transform.rotation.y, 0f);

                //OtherService.SetActive(false);
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
        if (!IsOwner) return;
        recept = true;
        display.SetActive(false);
        loginscreen.SetActive(false);
        mainmenu.SetActive(false);
        //balance_screen.SetActive(false);
        sentmoney.SetActive(false);
        sendmoneyout.SetActive(false);
        statement.SetActive(false);
        pinchangescreen.SetActive(false);
    }

    public void clickedexit()
    {
        if (!IsOwner) return;
        exit = true;
    }

    public void movebalance()
    {
        if (!IsOwner) return;
        balance = true;
    }

    public void movewithdraw()
    {
        if (!IsOwner) return;
        withdraw = true;
    }

    public void movestatement()
    {
        if (!IsOwner) return;
        statements = true;
    }

    public void movetransfer()
    {
        if (!IsOwner) return;
        transfer = true;
    }
    public void movepin()
    {
        if (!IsOwner) return;
        pin = true;
    }
    
    public void moverest()
    {
        if (!IsOwner) return;
        rest = true;
        display.SetActive(false);
        loginscreen.SetActive(false);
        mainmenu.SetActive(false);
        //balance_screen.SetActive(false);
        sentmoney.SetActive(false);
        sendmoneyout.SetActive(false);
        statement.SetActive(false);
        pinchangescreen.SetActive(false);

    }
    
    IEnumerator Delaysecond(int i)
    {
        
        yield return new WaitForSeconds(i);
        Debug.Log("Waited");
        display.SetActive(false);
        moverest();
    }
    IEnumerator Delaysecondafter(int i)
    {
        
        yield return new WaitForSeconds(i);
        Debug.Log("Waited");
        recept = true;
    }

    IEnumerator outputTimer(int i)
    {
        
        yield return new WaitForSeconds(i+4);
        output.SetActive(false);
        display.SetActive(false);
    }

}

