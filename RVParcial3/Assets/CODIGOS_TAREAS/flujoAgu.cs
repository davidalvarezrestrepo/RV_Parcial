using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flujoAgu : MonoBehaviour
{
    public bool manoIzquierdaLimpia = false;
    public bool manoDerechaLimpia = false;

    public Text estado;

    public float tiempoLavadoRequerido = 2f;

    private float tiempoDentroIzquierda = 0f;
    private float tiempoDentroDerecha = 0f;

    public int puntaje;


    private void Start()
    {
        PuntosManager.CargarPuntaje();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ManoIzquierda") && !manoIzquierdaLimpia)
        {
            tiempoDentroIzquierda += Time.deltaTime;
            if (tiempoDentroIzquierda >= tiempoLavadoRequerido)
            {
                manoIzquierdaLimpia = true;
                puntaje += 3;
                PuntosManager.AgregarPuntos(puntaje);
                Debug.Log(" Mano izquierda lavada correctamente");
                StartCoroutine(MostrarEstadoTemporal(" Mano izquierda lavada correctamente", 2f));
            }
        }

        if (other.CompareTag("ManoDerecha") && !manoDerechaLimpia)
        {
            tiempoDentroDerecha += Time.deltaTime;
            if (tiempoDentroDerecha >= tiempoLavadoRequerido)
            {
                manoDerechaLimpia = true;
                puntaje += 3;
                PuntosManager.AgregarPuntos(puntaje);
                Debug.Log("Mano derecha lavada correctamente");
                StartCoroutine(MostrarEstadoTemporal("Mano derecha lavada correctamente", 2f));
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

    private IEnumerator MostrarEstadoTemporal(string mensaje, float duracion)
    {
        estado.text = mensaje;
        yield return new WaitForSeconds(duracion);
        estado.text = "";
    }
}
