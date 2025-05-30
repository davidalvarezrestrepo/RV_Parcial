using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ponerGuantes : MonoBehaviour
{
    public Material texturaGuante;
    public SkinnedMeshRenderer manoIzquierdaRenderer;
    public SkinnedMeshRenderer manoDerechaRenderer;

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
            gameObject.SetActive(false);
        }
    }

}
