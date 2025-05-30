using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecuenciaManager : MonoBehaviour
{
    public GameObject corrienteAgua;
    public AudioSource sonidoAgua;

    private bool aguaActiva = false;
    private bool bloqueoTemporal = false;

    public string[] tagsActivadores = { "ManoIzquierda", "ManoDerecha" };

    public float cooldown = 1f;

    private void Start()
    {
        corrienteAgua.SetActive(false);
        if (sonidoAgua.isPlaying) sonidoAgua.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (bloqueoTemporal) return;

        foreach (string tag in tagsActivadores)
        {
            if (other.CompareTag(tag))
            {
                StartCoroutine(AlternarAguaConCooldown());
                break;
            }
        }
    }
    IEnumerator AlternarAguaConCooldown()
    {
        bloqueoTemporal = true;

        aguaActiva = !aguaActiva;
        corrienteAgua.SetActive(aguaActiva);

        if (aguaActiva)
        {
            sonidoAgua.Play();
            Debug.Log(" Agua activada");
        }
        else
        {
            sonidoAgua.Stop();
            Debug.Log("Agua desactivada");
        }
        yield return new WaitForSeconds(cooldown);
        bloqueoTemporal = false;
    }
}

