//This file is responsible for player movement using WASD if there is no Virtual headset
using UnityEngine;
using Unity.Netcode;

public class PlayerScript : NetworkBehaviour
{
    public Animator playerAnim;
    public Rigidbody playerRigid;

    public float w_speed, wb_speed, olw_speed, rn_speed, ro_speed;
    public bool walking;
    public bool forward;
    public bool reverse;
    public bool zero;

    public Transform playerTrans;

    public Camera playerCamera;

    private void Start()
    {
        
        if (IsLocalPlayer)
        {
            // Enable the camera for the local player
            playerCamera.gameObject.SetActive(true);
        }
        else
        {
            // Disable the camera for remote players
            playerCamera.gameObject.SetActive(false);
        }
        

    }

    void FixedUpdate()
    {
        if (!IsOwner) return;
        

            if (Input.GetKey(KeyCode.W))
            {
                playerRigid.velocity = transform.forward * w_speed * Time.deltaTime;
                forward = true;
                reverse = false;
                zero = false;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                playerRigid.velocity = -transform.forward * wb_speed * Time.deltaTime;
                forward = false;
                reverse = true;
                zero = false;
            }
            else
            {
                playerRigid.velocity = transform.forward * 0 * Time.deltaTime;
                forward = false;
                reverse = false;
                zero = true;
            }
            //CameraScript cam = new CameraScript();

//            cam.FollowPlayer(playerTrans);;
        }
    
    void Update()
    {
        if (!IsOwner) return;
        

            if (Input.GetKeyDown(KeyCode.W))
            {
                playerAnim.SetTrigger("walk");
                playerAnim.ResetTrigger("idle");
                walking = true;
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                playerAnim.ResetTrigger("walk");
                playerAnim.SetTrigger("idle");
                walking = false;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                playerAnim.SetTrigger("walkback");
                playerAnim.ResetTrigger("idle");
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                playerAnim.ResetTrigger("walkback");
                playerAnim.SetTrigger("idle");
            }
            if (Input.GetKey(KeyCode.A))
            {
                playerTrans.Rotate(0, -ro_speed * Time.deltaTime, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                playerTrans.Rotate(0, ro_speed * Time.deltaTime, 0);
            }
            if (walking == true)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    w_speed = w_speed + rn_speed;
                    playerAnim.SetTrigger("run");
                    playerAnim.ResetTrigger("walk");
                }
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    w_speed = olw_speed;
                    playerAnim.ResetTrigger("run");
                    playerAnim.SetTrigger("walk");
                }
            }
        }

    }
   

