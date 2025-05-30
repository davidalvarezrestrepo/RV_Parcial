using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class validarBandeja : MonoBehaviour
{
    public float tiempoTotal = 120f;
    private float tiempoRestante;
    public Text tiempo;

    private HashSet<GameObject> objetosEnBandeja = new HashSet<GameObject>();
    private bool tiempoFinalizado = false;

    [Header("Referencias a objetos lavables")]
    public GameObject[] objetosLavables;

    [Header("Referencias a objetos no lavables")]
    public GameObject[] objetosNoLavables;

    private void Start()
    {
        tiempoRestante = tiempoTotal;
    }

    private void Update()
    {
        if (!tiempoFinalizado)
        {
            tiempoRestante -= Time.deltaTime;

            if (tiempoRestante <= 0f)
            {
                tiempoFinalizado = true;
                EvaluarPuntaje();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject objeto = other.gameObject;

        if (!objetosEnBandeja.Contains(objeto))
        {
            objetosEnBandeja.Add(objeto);
        }
    }

    private bool EsObjetoEsperado(GameObject objeto)
    {
        foreach (GameObject obj in objetosLavables)
            if (obj == objeto) return true;

        foreach (GameObject obj in objetosNoLavables)
            if (obj == objeto) return true;

        return false;
    }

    public void EvaluarPuntaje()
    {
        float puntaje = 0f;

        Debug.Log(" Objetos en la bandeja:");
        foreach (GameObject obj in objetosEnBandeja)
        {
            Debug.Log( obj.name);
        }

        // Validar objetos lavables
        foreach (GameObject obj in objetosLavables)
        {
            if (objetosEnBandeja.Contains(obj))
            {
                instrumentos componente = obj.GetComponent<instrumentos>();
                if (componente != null && componente.estaLimpio)
                {
                    puntaje += 2f;
                }
                else
                {
                    puntaje -= 1f;
                }
            }
            else
            {
                puntaje -= 2f;
            }
        }

        // Validar objetos no lavables
        foreach (GameObject obj in objetosNoLavables)
        {
            if (objetosEnBandeja.Contains(obj))
            {
                puntaje += 1f;
            }
            else
            {
                puntaje -= 1f;
            }
        }

        // Penalizar objetos incorrectos
        foreach (GameObject obj in objetosEnBandeja)
        {
            if (!EsObjetoEsperado(obj))
            {
                puntaje -= 1.5f;
            }
        }

        Debug.Log(" Tiempo terminado");
        Debug.Log(" Puntaje final: " + puntaje);
    }
}
