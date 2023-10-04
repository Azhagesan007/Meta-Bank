using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class KeypadData : NetworkBehaviour
{
    public void getKeypadData(int flag,ref Text sentaccdisplay_text, ref Text sentmoneydisplay_text, ref Text OTP,ref Text pinchangetext, ref Text depamount, ref Text loginid, ref Text pinnumber, ref string pinno, string num)
    {
        if (flag == 1)
        {
            if (sentaccdisplay_text.text.Length < 10)
            {
                sentaccdisplay_text.text = sentaccdisplay_text.text + num;
            }
            else
            {
                sentmoneydisplay_text.text = sentmoneydisplay_text.text + num;
            }
        }

        else if (flag == 2)
        {
            if (OTP.text.Length < 4)
            {
                OTP.text = OTP.text + num;
            }
            else
            {
                pinchangetext.text = pinchangetext.text + num;
            }
        }

        else if (flag == 3)
        {

            depamount.text = depamount.text + num;

        }

        else
        {
            if (loginid.text.Length < 4)
            {
                loginid.text = loginid.text + num;
            }
            else
            {
                pinnumber.text = pinnumber.text + "X";
                pinno = pinno + num;
            }
        }
       
    }
    
}
