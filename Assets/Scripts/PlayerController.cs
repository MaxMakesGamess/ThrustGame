using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{  //delte this comment
    [SerializeField] float mainThrust = 1000;
    [SerializeField] float rotateSpeed = 100;
    [SerializeField] AudioClip mainEngine;

     Rigidbody rb;
     AudioSource audioSource;

     bool isAlive;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        processThrust();
        ProcessRotation();
    }

    void processThrust(){
        if(Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0)){
            rb.AddRelativeForce(UnityEngine.Vector3.up * Time.deltaTime * mainThrust);
            if(!audioSource.isPlaying)
            {
            audioSource.PlayOneShot(mainEngine);
            }
             
        }
        else
        {
           audioSource.Stop();
        }
    }
    void ProcessRotation(){
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateSpeed);
        }
        else if(Input.GetKey(KeyCode.D)){
            ApplyRotation(-rotateSpeed);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //Freeze rotation so can manually rotate
        transform.Rotate(UnityEngine.Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //Lets physics system take over rotation
    }
}
