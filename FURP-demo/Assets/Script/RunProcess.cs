using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class RunProcess : MonoBehaviour
{
    [SerializeField]
    LeverSwitch lever;

    [SerializeField]
    KnobSwitch headKnob, numberKnob;

    [SerializeField]
    StickBlockOnBoard blockBoard;

    
    // Start is called before the first frame update
    public GameObject headScreen, numberScreen, firstLineScreen, secondLineScreen;
    private TextMeshProUGUI headScreenTextComponent,numberScreenTextComponent,firstLineScreenTextComponent,secondLineScreenTextComponent;
    private bool headChanged = false, numberChanged=false,leverTriggered = false;

    private static char[] HEAD_LIST = { 'R', 'C' };
    private static int[] NUMBER_LIST = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
    private int[] lineValue= new int[2];
    private int[] RAM = new int[10];


    private int indexOfHead=0,indexOfNumber=0,pointerLocation=0;
    void Start()
    {
        headScreenTextComponent = headScreen.GetComponent<TextMeshProUGUI>();
        numberScreenTextComponent = numberScreen.GetComponent<TextMeshProUGUI>();
        firstLineScreenTextComponent = firstLineScreen.GetComponent<TextMeshProUGUI>();
        secondLineScreenTextComponent = secondLineScreen.GetComponent<TextMeshProUGUI>();

        DisplayLine();

    }
    
    // Update is called once per frame
    void Update()
    {
        if (lever.triggered && !leverTriggered)
        {
            Debug.Log("triggered");
            Process();
            leverTriggered = true;
        }

        if (!lever.triggered && leverTriggered)
        {
            leverTriggered = false;
        }

        if (!headChanged)
        {
            DisplayHead();
        }
        else if (headKnob.knobReturnValue == -1)
        {
            headChanged = false;
        }

        if (!numberChanged)
        {
            DisplayNumnber();
        }
        else if (numberKnob.knobReturnValue == -1)
        {
            numberChanged = false;
        }
    }

    void Process()
    {
        if (blockBoard.currentAttachBlockTag.Equals("ADD"))
        {
            //run add test
            if (!TwioLineTest())                
                return;

            //processadd
            lineValue[0] = lineValue[0] + lineValue[1];
            lineValue[1] = 0;
            pointerLocation--;
        }
        else if (blockBoard.currentAttachBlockTag.Equals("SUB"))
        {
            if (!TwioLineTest())   
                return;
            //process sub
            lineValue[0] = lineValue[0] - lineValue[1];
            lineValue[1] = 0;
            pointerLocation--;
        }
        else if (blockBoard.currentAttachBlockTag.Equals("PUSH"))
        {
            if (!PushTest())
                return;

            //process push
            Debug.Log("process push");
            ProcessPUSH();
        }
        else if (blockBoard.currentAttachBlockTag.Equals("PULL"))
        {
            if (!PullTest())
                return;

            ProcessPULL();
        }
        else
        {
            Debug.Log("No Block");
        }
        DisplayLine();
    }

    void DisplayHead()
    {
        if (headKnob.knobReturnValue == 0)
        {
            if (indexOfHead == 0 )
            {
                indexOfHead = HEAD_LIST.Length - 1;
            }
            else
            {
                indexOfHead--;
            }
            headChanged = true;
        }

        if (headKnob.knobReturnValue == 1)
        {
            if (indexOfHead == HEAD_LIST.Length-1)
            {
                indexOfHead = 0;
            }
            else
            {
                indexOfHead++;
            }
            headChanged = true;
        }

        headScreenTextComponent.text = HEAD_LIST[indexOfHead].ToString();
    }

    void DisplayNumnber()
    {
        if (numberKnob.knobReturnValue == 0)
        {
            if (indexOfNumber == 0)
            {
                indexOfNumber = NUMBER_LIST.Length - 1;
            }
            else
            {
                indexOfNumber--;
            }
            numberChanged = true;
         
        }

        if (numberKnob.knobReturnValue == 1)
        {
            if (indexOfNumber == NUMBER_LIST.Length - 1)
            {
                indexOfNumber = 0;
            }
            else
            {
                indexOfNumber++;
            }
            numberChanged = true;
        }
        numberScreenTextComponent.text = NUMBER_LIST[indexOfNumber].ToString();
    }

    void DisplayLine () 
    {
        if (lineValue[0] == 0)
        {
            firstLineScreenTextComponent.text = "";
        }
        else
        {
            firstLineScreenTextComponent.text = lineValue[0].ToString();
        }

        if (lineValue[1] == 0)
        {
            secondLineScreenTextComponent.text = "";           
        }
        else
        {
            secondLineScreenTextComponent.text = lineValue[1].ToString();
        }
    }
    
    bool TwioLineTest()
    {
        if (pointerLocation <=1)
        {
            Debug.Log("Process Fail\nless than two number in RAM");
            return false;
        }

        return true;
    }
    
    bool PushTest()
    {
        if (pointerLocation == 2)
        {
            Debug.Log("Fail PUSH, no more line left");
            return false;
        }
        return true;
    }

    bool PullTest()
    {
        if (pointerLocation == 0)
        {
            Debug.Log("Fail PULL, nothing in line" );
            return false;
        }
        else if (HEAD_LIST[indexOfHead].Equals('C'))
        {
            Debug.Log("Fail PULL\n invalid RAM location");
            return false;
        }
        return true;
    }

    private void ProcessPUSH()
    {
        if (HEAD_LIST[indexOfHead].Equals('C'))
        {
            lineValue[pointerLocation] = NUMBER_LIST[indexOfNumber];
            pointerLocation++;
        }
        else
        {
            //when Head = R
            lineValue[pointerLocation] = RAM[NUMBER_LIST[indexOfNumber]];
            pointerLocation++;
        }
    }

    private void ProcessPULL()
    {
        //when Head = R
        pointerLocation--;
        RAM[NUMBER_LIST[indexOfNumber]] = lineValue[pointerLocation];
        lineValue[pointerLocation] = 0;

    }
}
