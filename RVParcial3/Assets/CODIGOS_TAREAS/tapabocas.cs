using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tapabocas : MonoBehaviour
{
    public Text estado;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            estado.text = "tapabocas puesto";

        }
    }
}
