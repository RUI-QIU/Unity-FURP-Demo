using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RotationPlateControl: MonoBehaviour
{
    public GameObject rightHand;
    private Transform self;
    private float initialAngle,initialHandx,initialHandz, nowAngle;
    private bool isSelecting;

    private void Start()
    {
        self = GetComponent<Transform>();
        initialAngle = GetHandAngle(initialHandx, initialHandz);
        Debug.Log(initialAngle);
    }

    private void Update()
    {
       if(true)
        {
            ChangeAngle();
            Debug.Log(nowAngle);
        }
    }

    void ChangeAngle()
    {
        nowAngle = GetHandAngle(rightHand.transform.position.x, rightHand.transform.position.z);
        float finalAngle = nowAngle - initialAngle;
        self.rotation = Quaternion.Euler(0.0f, nowAngle, 0.0f);
    }

    float GetHandAngle(float x,float z)
    {
        x = rightHand.transform.position.x;
        z = rightHand.transform.position.z;
        float result = Mathf.Atan(z/x);


        if (Mathf.Sign(z)== 1 && Mathf.Sign(x)==1)
        {
            result = 360- (result*360/2/Mathf.PI);
        }else if (Mathf.Sign(z) == -1 && Mathf.Sign(x) == 1)
        {
            result = -1* (result * 360 / 2 / Mathf.PI);
        }else if (Mathf.Sign(z) == -1 && Mathf.Sign(x) == -1)
        {
            result = 180- (result * 360 / 2 / Mathf.PI);
        }
        else
        {
            result = 180 + -1 * (result * 360 / 2 / Mathf.PI);
        }
        return result;
    }

    public void SetSelectOn()
    {
        isSelecting = true;
    }

    public void SetSelectOff()
    {
        isSelecting = false;
    }

}