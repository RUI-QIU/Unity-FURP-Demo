using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StickBlockOnBoard : MonoBehaviour
{
    private Transform block;
    private bool isAttaching;
    public Transform self;

    private string[] blockList = { "ADD", "SUB", "PUSH", "PULL" };
    private string currentSelectBlockTag;
    public string currentAttachBlockTag= "NOATTACHBLOCK";

    // Update is called once per frame
    void Update()
    {
        if (isAttaching && !currentAttachBlockTag.Equals(currentSelectBlockTag))
        {
            block.position = self.position;
            block.rotation = self.rotation;
        }

        Debug.Log(currentAttachBlockTag);

    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < blockList.Length; i++)
        {
            if (other.CompareTag(blockList[i]) && !isAttaching )
            {
                block = other.GetComponent<Transform>();
                isAttaching = true;
                currentAttachBlockTag = blockList[i];
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(currentAttachBlockTag) && isAttaching && currentSelectBlockTag.Equals(currentAttachBlockTag))
        {
            Debug.Log("unattach");
                isAttaching = false;
                currentAttachBlockTag = "NOATTACHBLOCK";
        }

    }



    public void GetSelectBlockTag(string tag)
    {
        currentSelectBlockTag = tag;
    }

}
