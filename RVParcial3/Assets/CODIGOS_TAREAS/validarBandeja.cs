using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class validarBandeja : MonoBehaviour
{
    public GameObject[] objetosLavables;
    private float puntaje = 0f;
    public GameObject[] objetosNoLavables;

    // Lista de objetos actualmente dentro de la bandeja
    private HashSet<GameObject> objetosEnBandeja = new HashSet<GameObject>();
    public Text textoTiempo;

    public Text textoPuntaje;     // Texto para mostrar el puntaje

    private float tiempoRestante = 40f;
    private bool tiempoTerminado = false;


    private void Start()
    {
        StartCoroutine(ContadorRegresivo());
    }

    private void Update()
    {
        if (!tiempoTerminado)
        {
            textoTiempo.text = "Tiempo: " + Mathf.CeilToInt(tiempoRestante) + "s";
        }
    }

    private IEnumerator ContadorRegresivo()
    {
        while (tiempoRestante > 0)
        {
            yield return new WaitForSeconds(1f);
            tiempoRestante -= 1f;
        }

        tiempoTerminado = true;
        textoTiempo.text = "¡Tiempo terminado!";
        EvaluarPuntaje();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject objeto = other.gameObject;

        if (EsObjetoEsperado(objeto))
        {
            if (!objetosEnBandeja.Contains(objeto))
            {
                objetosEnBandeja.Add(objeto);
                Debug.Log("Objeto válido entró en la bandeja: " + objeto.name);
            }
        }
        else
        {
            Debug.Log(" Objeto NO válido entró: " + objeto.name);
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
        puntaje = 0f;

        HashSet<GameObject> encontrados = new HashSet<GameObject>(objetosEnBandeja);

        // Evaluar objetos lavables
        foreach (GameObject obj in objetosLavables)
        {
            if (encontrados.Contains(obj))
            {
                instrumentos componente = obj.GetComponent<instrumentos>();
                if (componente != null && componente.estaLimpio)
                {
                    puntaje += 2f; // Correcto y limpio
                }
                else
                {
                    puntaje -= 1f; // Estaba pero sucio
                    Debug.Log(" Objeto lavable no estaba limpio: " + obj.name);
                }
            }
            else
            {
                puntaje -= 2f; // Faltó
                Debug.Log(" Objeto lavable no fue ingresado: " + obj.name);
            }
        }

        // Evaluar objetos no lavables
        foreach (GameObject obj in objetosNoLavables)
        {
            if (encontrados.Contains(obj))
            {
                puntaje += 1f; // Correcto
            }
            else
            {
                puntaje -= 1f; // Faltó
                Debug.Log(" Objeto no lavable no fue ingresado: " + obj.name);
            }
        }

        Debug.Log(" Evaluación completada.");
        Debug.Log(" Objetos en la bandeja: " + objetosEnBandeja.Count);
        Debug.Log(" Puntaje final: " + puntaje);
        textoPuntaje.text = puntaje.ToString();
    }
}
