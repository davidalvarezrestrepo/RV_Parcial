using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class MoviCube : MonoBehaviour
{
    public XRLever lever;
    public XRKnob wheel;

    public float fSpeed;
    public float sSpeed;

    float forwardVelocity;
    float sidevelocity;

    private void Update()
    {
        if (lever.value)
        {
            forwardVelocity = fSpeed * 1;
            sidevelocity = sSpeed * 1 * Mathf.Lerp(-1, 1, wheel.value);
        }
        else
        {
            forwardVelocity = fSpeed * 0;
            sidevelocity = sSpeed * 0 * Mathf.Lerp(-1, 1, wheel.value);
        }

        Vector3 velocity = new Vector3(sidevelocity, 0, forwardVelocity);
        transform.position = velocity * Time.deltaTime;
    }
}
