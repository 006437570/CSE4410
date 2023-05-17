using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemChecker : MonoBehaviour
{
    [SerializeField]
    private itemID itemID;

    private void Awake()
    {
        itemID = null;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Item"))
        {
            itemID = col.GetComponent<itemID>();
            if(itemID.itemIDNum == 0 && itemID.isHeld == false)
            {
                Destroy(col.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Item"))
        {
            itemID = col.GetComponent<itemID>();
            if(itemID.itemIDNum == 0 && itemID.isHeld == false)
            {
                Destroy(col.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Item"))
        {
            itemID = null;
        }
    }
}
