using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Canvas prompt = null;
    [SerializeField] Animator animator = null;

    private bool guiOpen = false;
    
    void Update()
    {
        if(guiOpen)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                animator.SetBool("Open", !animator.GetBool("Open"));
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            prompt.enabled = true;
            guiOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            prompt.enabled = false;
            guiOpen = false;
        }
    }
}
