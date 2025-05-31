using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class instrumentos : MonoBehaviour
{
    public string nombreObjeto;
    public bool estaLimpio = false;
    public AudioSource audioLimpio;

    public Text estado;

    public int puntaje;
    private void Start()
    {
        nombreObjeto = gameObject.name;
        PuntosManager.CargarPuntaje();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("agua"))
        {
            if (!estaLimpio)
            {
                estaLimpio = true;
                puntaje += 1;
                PuntosManager.AgregarPuntos(puntaje);
                audioLimpio.Play();
                Debug.Log(" Objeto limpiado: " + nombreObjeto);
                StartCoroutine(MostrarEstadoTemporal("Objeto limpiado: " + nombreObjeto, 6f));
            }
        }
    }
    private IEnumerator MostrarEstadoTemporal(string mensaje, float duracion)
    {
        estado.text = mensaje;
        yield return new WaitForSeconds(duracion);
        estado.text = "";
    }
}
