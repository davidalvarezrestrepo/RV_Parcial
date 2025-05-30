using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class instrumentos : MonoBehaviour
{
    public string nombreObjeto;
    public bool estaLimpio = false;
    public AudioSource audioLimpio;

    private void Start()
    {
        nombreObjeto = gameObject.name;
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("agua"))
        {
            if (!estaLimpio)
            {
                estaLimpio = true;         
                audioLimpio.Play();
                Debug.Log(" Objeto limpiado: " + nombreObjeto);                
            }
        }
    }
}
