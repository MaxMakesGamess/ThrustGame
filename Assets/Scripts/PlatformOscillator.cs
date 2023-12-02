using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlatformOscillator : MonoBehaviour
{
    UnityEngine.Vector3 startingPos;
    [SerializeField] UnityEngine.Vector3 movementVector;
    [SerializeField] float timeOffset = 0;
    [SerializeField] float period = 2f;
    float cycles;
    float movementFactor;

    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeOffset != 0){
        Invoke("Oscillate", timeOffset);
        } else{
            Oscillate();
        }
    }

    private void Oscillate()
    {
        if (period <= Mathf.Epsilon) { return; }

        cycles = Time.time / period;
        //continually growing over time

        const float tau = Mathf.PI * 2; //constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1

        movementFactor = (rawSinWave + 1f) / 2f; //recalculated to go from 0 to 1 so its cleaner

        UnityEngine.Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
