using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairController : MonoBehaviour
{
    public bool isChanged;
    public Animator animator;

    public void ChangeChair()
    {
        if(!isChanged)
        {
            isChanged = true;
            Debug.Log("Chair is now changed...");
            animator.SetBool("isChanged", isChanged);
        }
    }
}
