using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flujoAgu : MonoBehaviour
{
    public bool manoIzquierdaLimpia = false;
    public bool manoDerechaLimpia = false;

    public float tiempoLavadoRequerido = 2f;

    private float tiempoDentroIzquierda = 0f;
    private float tiempoDentroDerecha = 0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ManoIzquierda") && !manoIzquierdaLimpia)
        {
            tiempoDentroIzquierda += Time.deltaTime;
            if (tiempoDentroIzquierda >= tiempoLavadoRequerido)
            {
                manoIzquierdaLimpia = true;
                Debug.Log(" Mano izquierda lavada correctamente");
            }
        }

        if (other.CompareTag("ManoDerecha") && !manoDerechaLimpia)
        {
            tiempoDentroDerecha += Time.deltaTime;
            if (tiempoDentroDerecha >= tiempoLavadoRequerido)
            {
                manoDerechaLimpia = true;
                Debug.Log("Mano derecha lavada correctamente");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ManoIzquierda") && !manoIzquierdaLimpia)
            tiempoDentroIzquierda = 0f;

        if (other.CompareTag("ManoDerecha") && !manoDerechaLimpia)
            tiempoDentroDerecha = 0f;
    }
}
