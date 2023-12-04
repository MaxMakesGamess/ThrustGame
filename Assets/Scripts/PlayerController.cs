using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{  //delte this comment
    [SerializeField] float mainThrust = 1000;
    [SerializeField] float rotateSpeed = 100;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainBooster;
    [SerializeField] ParticleSystem leftBoosterParticle;
    [SerializeField] ParticleSystem rightBoosterParticle;

     Rigidbody rb;

     
     AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationZ;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void FixedUpdate()
    {
        // Reset the Z rotation to 0 (or any desired value)
        transform.rotation = UnityEngine.Quaternion.Euler(0, 0, 
                                            transform.rotation.eulerAngles.z);
    }

    void ProcessThrust(){
        if(Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            StartThrusting();

        }
        else
        {
            mainBooster.Stop();
            audioSource.Stop();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(UnityEngine.Vector3.up * Time.deltaTime * mainThrust);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }

        if (!mainBooster.isPlaying)
        {
            mainBooster.Play();
        }
    }

    void ProcessRotation(){
        if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            rightBoosterParticle.Stop();
            leftBoosterParticle.Stop();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotateSpeed);
        if (!leftBoosterParticle.isPlaying)
        {
            leftBoosterParticle.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rotateSpeed);
        if (!rightBoosterParticle.isPlaying)
        {
            rightBoosterParticle.Play();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //Freeze rotation so can manually rotate
        transform.Rotate(UnityEngine.Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; //Lets physics system take over rotation
    }
}
