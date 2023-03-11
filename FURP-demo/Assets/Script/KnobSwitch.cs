using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class KnobSwitch : MonoBehaviour
{
    public HingeJoint hinge;
    public float KnobErrorValue;
    private bool isSelecting = false;
    public int knobReturnValue = -1;
   
    


    // Update is called once per frame
    void Update()
    {
        

        if (isSelecting
            && hinge.angle <= hinge.limits.max + KnobErrorValue
            && hinge.angle >= hinge.limits.max - KnobErrorValue)
        {
            //what to do after switch on
            knobReturnValue = 0;
            
        }
        else if (isSelecting
                  && hinge.angle <= hinge.limits.min + KnobErrorValue
                  && hinge.angle >= hinge.limits.min - KnobErrorValue)
        {
            knobReturnValue = 1;
        }
        
        if (!isSelecting || (isSelecting && hinge.angle< hinge.limits.max - KnobErrorValue && hinge.angle> hinge.limits.min + KnobErrorValue))
        {
            knobReturnValue = -1;
        }
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
