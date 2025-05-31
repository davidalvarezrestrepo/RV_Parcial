using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using System.Collections.Generic;

public class CirujanoRecibeObjeto : MonoBehaviour
{
    public MoveSurgeonHand moveSurgeonHand; // Referencia al script que mueve la mano

    public float tiempoParaBajar = 2f;
    public float tiempoReactivar = 7f;
    public int puntaje = 0;

    private XRSocketInteractor socketInteractor;
    private IXRSelectInteractable ultimoObjetoRecibido;

    // Lista de nombres de objetos en orden
    private List<string> ordenHerramientas = new List<string>
    {
        "bisturí con hoja 10",
        "pinza de disección",
        "Separador tipo Farabeuf",
        "Pinza de Kocher curva",
        "Tijera de Mayo curva",
        "Tijera Potts-Smith curva grande",
        "Tijera de Metzenbaum larga",
        "Separador tipo Separador Senn",
        "Copa de muestra",
        "Gasas",
        "Desfibrilador",
        "Aguja curva",
        "Hilo"
    };

    private int indiceActual = 0; // índice del objeto esperado

    private bool isProcessing = false; // para evitar que se procese doble

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
        if (isProcessing) return;
        isProcessing = true;

        Debug.Log("Objeto recibido. Iniciando proceso de bajada de mano y verificación.");

        ultimoObjetoRecibido = args.interactableObject;
        StartCoroutine(BajarYReactivarMano());
    }

    private IEnumerator BajarYReactivarMano()
    {

        yield return new WaitForSeconds(tiempoParaBajar);

        Debug.Log("Bajando la mano (isToggled = false)");
        moveSurgeonHand.isToggled = false;

        // Verificar el objeto
        if (ultimoObjetoRecibido != null)
        {
            GameObject objetoGO = ultimoObjetoRecibido.transform.gameObject;

            // Verificar que sea una herramienta (por tag)
            if (objetoGO.CompareTag("herramientas"))
            {
                string nombreObjeto = objetoGO.name.Trim();
                string nombreEsperado = ordenHerramientas[indiceActual];

                Debug.Log("Verificando objeto: " + nombreObjeto + " | Esperado: " + nombreEsperado);

                if (nombreObjeto.Equals(nombreEsperado, System.StringComparison.OrdinalIgnoreCase))
                {
                    
                    PuntosManager.AgregarPuntos(2);
                    Debug.Log("Objeto correcto. ¡+2 puntos!");
                    indiceActual++;

                    if (indiceActual >= ordenHerramientas.Count)
                    {
                        Debug.Log("¡Todos los objetos entregados!");
                        indiceActual = ordenHerramientas.Count - 1;
                    }
                    else
                    {

                        PuntosManager.AgregarPuntos(-2);
                        Debug.Log("Objeto incorrecto. -2 puntos. Se espera el mismo objeto.");
                    }
                }
                else
                {
                    Debug.Log("El objeto recibido no tiene el tag 'herramientas'. No se contabiliza.");
                }

                AudioSource audio = objetoGO.GetComponent<AudioSource>();
                if (audio != null)
                {
                    audio.Play();
                    Debug.Log("Reproduciendo audio del objeto.");
                    yield return new WaitForSeconds(audio.clip.length);
                }

                Destroy(objetoGO);
                Debug.Log("Objeto destruido.");
            }

            // Esperar 7 segundos antes de volver a extender la mano
            Debug.Log("Esperando " + tiempoReactivar + " segundos para reactivar la mano...");
            yield return new WaitForSeconds(tiempoReactivar);

            Debug.Log("Reactivando la mano (isToggled = true).");
            moveSurgeonHand.isToggled = true;

            // Limpieza
            ultimoObjetoRecibido = null;
            isProcessing = false;
        }
    }
}
