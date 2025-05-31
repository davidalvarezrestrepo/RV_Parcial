using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ponerGuantes : MonoBehaviour
{
    public Material texturaGuante;
    public SkinnedMeshRenderer manoIzquierdaRenderer;
    public SkinnedMeshRenderer manoDerechaRenderer;

    public Text estado;

    public enum ManoAsignada { Izquierda, Derecha }
    public ManoAsignada manoAsignada;

    private void OnTriggerEnter(Collider other)
    {
        if (manoAsignada == ManoAsignada.Izquierda && other.CompareTag("ManoIzquierda"))
        {
            AplicarGuante(manoIzquierdaRenderer, "izquierda");
        }
        else if (manoAsignada == ManoAsignada.Derecha && other.CompareTag("ManoDerecha"))
        {
            AplicarGuante(manoDerechaRenderer, "derecha");
        }
    }

    private void AplicarGuante(SkinnedMeshRenderer renderer, string mano)
    {
        if (renderer != null && texturaGuante != null)
        {
            renderer.material = texturaGuante;
            
            StartCoroutine(MostrarEstadoTemporal("Guante puesto", 3f));
        }
    }

    private IEnumerator MostrarEstadoTemporal(string mensaje, float duracion)
    {
        estado.text = mensaje;
        gameObject.SetActive(false);
        yield return new WaitForSeconds(duracion);
        estado.text = "";
    }

}
