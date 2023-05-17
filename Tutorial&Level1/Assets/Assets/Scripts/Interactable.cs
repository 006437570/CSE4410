using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    [SerializeField]
    private bool itemInRange, itemSlotFull;
    [SerializeField]
    private Transform itemHolder;
    [SerializeField]
    private GameObject itemObject;
    [SerializeField]
    private Rigidbody2D itemRb;
    [SerializeField]
    private Collider2D itemCol;
    [SerializeField]
    private int numItemInRange;

/*
    [SerializeField]
    private bool interactItemBool;*/


    /* UI STUFF
    [SerializeField]
    private GameObject uiObject;
    [SerializeField]
    private PopUpUI PopUpScript;
    [SerializeField]
    private bool interactUIBool;
    [SerializeField]
    private bool inRangeUI
    */
    
    void Awake()
    {
        itemInRange = false;
        itemSlotFull = false;
        itemObject = null;
        itemRb = null;
        itemCol =  null;
        numItemInRange = 0;
        itemHolder = transform.GetChild(0);


        /* UI STUFF FOR LATER
        inRangeUI = false;
        interactUIBool = false;
        interactItemBool = false;
        itemObject = null;
        uiObject = null;
        numItemInRange = 0;
        */
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            {
                if(itemInRange && !itemSlotFull)
                {
                    itemSlotFull = true;
                    itemObject.transform.position = Vector3.zero;
                    itemObject.transform.rotation = Quaternion.identity;
                    itemObject.transform.SetParent(itemHolder.transform, false);
                    if(itemRb != null)
                    {
                        itemRb.isKinematic = true;
                    }
                    itemCol.enabled = false;
                    return;
                }
                if(itemSlotFull)
                {
                    itemSlotFull = false;
                    itemObject.transform.SetParent(null);
                    itemObject = null;
                    if(itemRb != null)
                    {
                        itemRb.isKinematic = false;
                    }
                    itemCol.enabled = true;
                    return;
                }
            }
        }
        if(itemSlotFull)
        {
            if(itemObject.transform.position != itemHolder.transform.position || itemObject.transform.rotation != itemHolder.transform.rotation)
            {
                itemObject.transform.position = itemHolder.transform.position;
                itemObject.transform.rotation = itemHolder.transform.rotation;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Item"))
        {
            itemInRange = true;
            numItemInRange++;
            if(!itemSlotFull)
            {
                itemObject = col.gameObject;
                itemRb = col.GetComponent<Rigidbody2D>();
                itemCol = col.GetComponent<BoxCollider2D>();
            }
        }
        /*
        else if(col.gameObject.CompareTag("UIPopUp"))
        {
            interactUIBool = true;
            inRangeUI = true;
            uiObject = col.gameObject;
            PopUpScript = col.GetComponent<PopUpUI>();
            Debug.Log("Player now in range of UI interactable");
        }*/
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Item"))
        {
            numItemInRange--;
            if(numItemInRange <= 0)
            {
                itemInRange = false;
                if(!itemSlotFull)
                {
                    itemObject = null;
                    itemRb = null;
                    itemCol = null;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Item"))
        {
            if(numItemInRange > 0 && !itemSlotFull)
            {
                itemObject = col.gameObject;
                itemRb = col.GetComponent<Rigidbody2D>();
                itemCol = col.GetComponent<BoxCollider2D>();
            }
        }
    }
        /*
        if(col.gameObject.CompareTag("UIPopUp"))
        {
            interactUIBool = true;
            inRangeUI = false;
            uiObject = null;
            PopUpScript = null;
            Debug.Log("Out of range of UI interactable");
        }*/
}