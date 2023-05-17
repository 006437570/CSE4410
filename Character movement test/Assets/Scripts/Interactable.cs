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
    [SerializeField]
    private itemID itemID;
    
    void Awake()
    {
        itemInRange = false;
        itemSlotFull = false;
        itemObject = null;
        itemRb = null;
        itemCol =  null;
        numItemInRange = 0;
        itemHolder = transform.GetChild(0);
        itemID = null;
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
                    itemID.isHeld = true;
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
                    itemID.isHeld = false;
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
                itemID = col.GetComponent<itemID>();
            }
        }
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
                    itemID = null;
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
                itemID = col.GetComponent<itemID>();
            }
        }
    }
}