using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class validarBandeja : MonoBehaviour
{
    public Button botonContinuar;
    private HashSet<string> objetosEnBandeja = new HashSet<string>();

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
        botonContinuar.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        string nombreObjeto = other.gameObject.name;

        // Si ya estaba registrado, no lo agregamos otra vez
        if (!objetosEnBandeja.Contains(nombreObjeto))
        {
            objetosEnBandeja.Add(nombreObjeto);
            VerificarMostrarBoton();
        }
    }

    private void VerificarMostrarBoton()
    {
        foreach (string obj in objetosEnBandeja)
        {
            if (EsObjetoEsperado(obj))
            {
                botonContinuar.gameObject.SetActive(true);
                return;
            }
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
                    puntaje += 2f; //  Correcto y limpio
                }
                else
                {
                    puntaje -= 1f; //  Correcto pero sucio
                }
            }
            else
            {
                puntaje -= 2f; //  No está en la bandeja
            }
        }

        // Validar objetos no lavables
        foreach (string obj in objetosNoLavables)
        {
            if (encontrados.Contains(obj))
            {
                puntaje += 1f; //  Correcto
            }
            else
            {
                puntaje -= 1f; //  No está
            }
        }

        // Penalizar objetos incorrectos
        foreach (string obj in objetosEnBandeja)
        {
            if (!EsObjetoEsperado(obj))
            {
                puntaje -= 1.5f; //  Objeto que no pertenece a la lista
            }
        }

        Debug.Log(" Puntaje final: " + puntaje);
    }
}
