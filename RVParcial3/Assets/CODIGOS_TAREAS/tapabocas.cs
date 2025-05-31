using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tapabocas : MonoBehaviour
{
    public Text estado;
    public int puntaje;

    private void Start()
    {
        PuntosManager.CargarPuntaje();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            estado.text = "tapabocas puesto";
            puntaje = 5;
            PuntosManager.AgregarPuntos(puntaje);


        }
    }
}
