using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuenciaManager : MonoBehaviour
{
    private List<string> pasosCorrectos = new List<string>
    {
        "LavarseManos", "PonerseGuantes", "PonerseTapabocas", "AbrirInventario",
        "LavarObjetos", "ColocarObjetosBandeja"
    };

    private int pasoActual = 0;
    public int Puntaje { get; private set; }

    public void ValidarPaso(string paso)
    {
        if (paso == pasosCorrectos[pasoActual])
        {
            Debug.Log($" Paso correcto: {paso}");
            pasoActual++;
            Puntaje += 10;
        }
        else
        {
            Debug.Log($" Paso fuera de orden: {paso}");
        }
    }

    public void ReiniciarSecuencia()
    {
        pasoActual = 0;
        Puntaje = 0;
    }
}

