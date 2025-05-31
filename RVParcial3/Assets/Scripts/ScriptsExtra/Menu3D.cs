using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class Menu3D : MonoBehaviour
{
    private bool opened;

    public void OpenMenu()
    {        
        Debug.Log("Abriendo Menu");
    }

    public void CloseMenu()
    {
        Debug.Log("Cerrando Menu");
    }
}
