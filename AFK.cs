using UnityEngine;

public class AFK : MonoBehaviour
{
    private float awayTime;
    public float awayTimeLimit;
    private bool isAway = false;

    void Start()
    {
        awayTime = 0f;
    }

    void Update()
    {
        Debug.Log(awayTime);
        if (Input.anyKey || Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            awayTime = 0f;
        }
        else
        {
            awayTime += Time.deltaTime;

            if (awayTime >= awayTimeLimit)
            {
                isAway = true;
            }
        }

        if (isAway)
        {
            Debug.Log("lOGOUT");
        }
    }
}
