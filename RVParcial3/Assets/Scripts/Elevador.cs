using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class Elevador : MonoBehaviour
{
    public XRLever elevador;

    public float speed;

    // Update is called once per frame
    void Update()
    {
        float upSpeed = speed * (elevador.value ? 1 : 0);

        Vector3 velocity = new Vector3(0, upSpeed, 0);

        if (velocity.y > 2)
        {
            velocity = Vector3.zero;
        }

        transform.position += velocity * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = transform;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.transform.parent = null;
        }
    }
}
