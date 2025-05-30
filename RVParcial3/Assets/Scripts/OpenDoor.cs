using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class OpenDoor : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        GetComponent<XRPushButton>().selectEntered.AddListener(e => DoorAction());
    }

    public void DoorAction()
    {
        Debug.Log("Click");
        bool isOpen = anim.GetBool("Open");
        anim.SetBool("Open", !isOpen);
    }
}
