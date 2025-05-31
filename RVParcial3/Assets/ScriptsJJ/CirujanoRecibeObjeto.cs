using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class CirujanoRecibeObjeto : MonoBehaviour
{
    public MoveSurgeonHand moveSurgeonHand; // Referencia al script que mueve la mano

    public float tiempoParaBajar = 2f;
    public float tiempoReactivar = 7f;

    private XRSocketInteractor socketInteractor;
    private IXRSelectInteractable ultimoObjetoRecibido;

    private void Awake()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();
        socketInteractor.selectEntered.AddListener(OnObjetoColocado);
    }

    private void OnDestroy()
    {
        socketInteractor.selectEntered.RemoveListener(OnObjetoColocado);
    }

    private void OnObjetoColocado(SelectEnterEventArgs args)
    {
        Debug.Log("🟡 Objeto recibido. Iniciando proceso de bajada de mano y limpieza.");
        ultimoObjetoRecibido = args.interactableObject;
        StartCoroutine(BajarYReactivarMano());
    }

    private IEnumerator BajarYReactivarMano()
    {
        // 1. Esperar 2 segundos antes de bajar la mano
        yield return new WaitForSeconds(tiempoParaBajar);

        Debug.Log("🔽 Bajando la mano (isToggled = false).");
        moveSurgeonHand.isToggled = false;

        // 2. Destruir el objeto que se entregó
        if (ultimoObjetoRecibido != null)
        {
            GameObject objetoGO = ultimoObjetoRecibido.transform.gameObject;
            Destroy(objetoGO);
            Debug.Log("🗑️ Objeto destruido.");
        }

        // 3. Esperar 7 segundos antes de volver a extender la mano
        Debug.Log("⏳ Esperando " + tiempoReactivar + " segundos para reactivar la mano...");
        yield return new WaitForSeconds(tiempoReactivar);

        Debug.Log("🔼 Reactivando la mano (isToggled = true).");
        moveSurgeonHand.isToggled = true;

        // Limpieza del objeto anterior
        ultimoObjetoRecibido = null;
    }
}
