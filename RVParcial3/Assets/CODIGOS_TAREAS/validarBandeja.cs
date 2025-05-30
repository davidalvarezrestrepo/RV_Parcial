using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class validarBandeja : MonoBehaviour
{
    public float tiempoTotal = 120f; // 2 minutos
    private float tiempoRestante;

    private HashSet<string> objetosEnBandeja = new HashSet<string>();

    private bool tiempoFinalizado = false;

    // Lista de objetos lavables
    private readonly string[] objetosLavables = new string[]
    {
        "bisturí con hoja 10", "pinza de disección", "Separador tipo Farabeuf",
        "Pinza de Kocher curva", "Tijera de Mayo curva", "Tijera Potts-Smith curva grande",
        "Tijera de Metzenbaum larga", "Separador tipo Separador Senn", "Copa de muestra",
        "Aguja curva"
    };

    // Lista de objetos no lavables
    private readonly string[] objetosNoLavables = new string[]
    {
        "Gasas", "Hilo"
    };

    private void Start()
    {
        tiempoRestante = tiempoTotal;
    }

    private void Update()
    {
        if (!tiempoFinalizado)
        {
            tiempoRestante -= Time.deltaTime;

            // Puedes mostrar el tiempo en consola o UI
            Debug.Log("Tiempo restante: " + Mathf.CeilToInt(tiempoRestante) + "s");

            if (tiempoRestante <= 0f)
            {
                tiempoFinalizado = true;
                EvaluarPuntaje();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        string nombreObjeto = other.gameObject.name;

        if (!objetosEnBandeja.Contains(nombreObjeto))
        {
            objetosEnBandeja.Add(nombreObjeto);
        }
    }

    private bool EsObjetoEsperado(string nombre)
    {
        foreach (string obj in objetosLavables)
            if (obj == nombre) return true;

        foreach (string obj in objetosNoLavables)
            if (obj == nombre) return true;

        return false;
    }

    public void EvaluarPuntaje()
    {
        float puntaje = 0f;

        HashSet<string> encontrados = new HashSet<string>(objetosEnBandeja);

        // Validar objetos lavables
        foreach (string obj in objetosLavables)
        {
            if (encontrados.Contains(obj))
            {
                GameObject encontrado = GameObject.Find(obj);
                instrumentos componente = encontrado.GetComponent<instrumentos>();

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
        foreach (string obj in objetosNoLavables)
        {
            if (encontrados.Contains(obj))
            {
                puntaje += 1f;
            }
            else
            {
                puntaje -= 1f;
            }
        }

        // Penalizar objetos incorrectos
        foreach (string obj in objetosEnBandeja)
        {
            if (!EsObjetoEsperado(obj))
            {
                puntaje -= 1.5f;
            }
        }

        Debug.Log("Tiempo terminado");
        Debug.Log(" Puntaje final: " + puntaje);
    }
}
