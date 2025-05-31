using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CirujanoSocketDetector : MonoBehaviour
{
    public MoveSurgeonHand moveHand;

    private XRSocketInteractor socketInteractor;

    private void Start()
    {
        socketInteractor = GetComponent<XRSocketInteractor>();

        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.AddListener(OnObjectReceived);
        }
    }

    private void OnDestroy()
    {
        if (socketInteractor != null)
        {
            socketInteractor.selectEntered.RemoveListener(OnObjectReceived);
        }
    }

    private void OnObjectReceived(SelectEnterEventArgs args)
    {
        GameObject receivedObject = args.interactableObject.transform.gameObject;

        if (receivedObject.CompareTag("Instrumento"))
        {
            Debug.Log("✅ Cirujano recibió el objeto en el SOCKET: " + receivedObject.name);

            if (moveHand != null)
            {
                moveHand.isToggled = false; // retraer mano
                moveHand.StartCoroutine(moveHand.StartCooldown()); // iniciar cooldown
            }
        }
        else
        {
            Debug.Log("❌ Objeto NO válido en el socket: " + receivedObject.name);
        }
    }
}
