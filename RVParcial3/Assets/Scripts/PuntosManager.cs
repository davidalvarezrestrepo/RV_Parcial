using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuntosManager : MonoBehaviour
{
    private static int puntajeTotal = 0;

    // Llamar a esta función para agregar puntos (positivos o negativos)
    public static void AgregarPuntos(int puntos)
    {
        puntajeTotal += puntos;
        Debug.Log("Se han agregado " + puntos + " puntos. Total actual: " + puntajeTotal);

        // Guardar el nuevo valor en PlayerPrefs
        PlayerPrefs.SetInt("PuntajeTotal", puntajeTotal);
        PlayerPrefs.Save();
    }

    // Llamar a esta función para obtener el puntaje total actual
    public static int ObtenerPuntajeTotal()
    {
        return puntajeTotal;
    }

    // Llamar a esta función al iniciar para cargar los puntos guardados anteriormente
    public static void CargarPuntaje()
    {
        puntajeTotal = PlayerPrefs.GetInt("PuntajeTotal", 0); // Si no hay guardado, empieza en 0
    }

    // Opción adicional para reiniciar el puntaje (si lo necesitas)
    public static void ReiniciarPuntaje()
    {
        puntajeTotal = 0;
        PlayerPrefs.SetInt("PuntajeTotal", puntajeTotal);
        PlayerPrefs.Save();
    }
}
