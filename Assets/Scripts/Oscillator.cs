using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementVector;
    [SerializeField] float period = 2f;

    Vector3 startingPosition;
    Vector3 offset;

    const float tau = Mathf.PI * 2;
    float movementFactor;
    float cyclesNumber;


    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        Oscillate();
    }

    void Oscillate()
    {
        if (period <= Mathf.Epsilon) { return; }

        cyclesNumber = Time.time / period;
        movementFactor = Mathf.Sin(cyclesNumber * tau); // range from -1 to 1
        movementFactor = (movementFactor + 1f) / 2f; // make range from 0 to 1

        offset = movementVector * movementFactor;

        transform.position = startingPosition + offset;
    }
}
