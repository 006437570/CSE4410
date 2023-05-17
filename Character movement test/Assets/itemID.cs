using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemID : MonoBehaviour
{
    public int itemIDNum;
    public bool isHeld;

    public void Awake()
    {
        isHeld = false;
    }
}
