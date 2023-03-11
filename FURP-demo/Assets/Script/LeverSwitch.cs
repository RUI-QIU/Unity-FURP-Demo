using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class LeverSwitch : MonoBehaviour
{
    public HingeJoint hinge;
    public float leverErrorValue;
    private bool isSelecting = false;
    public bool triggered = false;
    

    // Update is called once per frame
    void Update()
    {

        if (!isSelecting
            && hinge.angle <= hinge.limits.min + leverErrorValue
            && hinge.angle >= hinge.limits.min - leverErrorValue)
        {
            //what to do after switch on
            triggered = true;
        }
        else if (!isSelecting
                 && hinge.angle <= hinge.limits.max + leverErrorValue
                 && hinge.angle >= hinge.limits.max - leverErrorValue)
        {
            triggered = false;
        }

        //Debug.Log(leverXRotationValue);
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
