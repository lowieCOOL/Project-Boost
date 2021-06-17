using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    KeyCode moveLeftKey = KeyCode.Q;
    KeyCode moveRightKey = KeyCode.D;
    [SerializeField] float thrustMagnitude = 10f;
    [SerializeField] float rotationThrustMagnitude = 10f;
    [SerializeField] AudioClip engineThrust;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();

    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(engineThrust);
            } 

            rb.AddRelativeForce(Vector3.up * thrustMagnitude * Time.deltaTime);
        }
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(moveLeftKey))
        {
            ApplyRotation(rotationThrustMagnitude);
        }
        else if (Input.GetKey(moveRightKey))
        {
            ApplyRotation(-rotationThrustMagnitude);
        }

    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freeze rotation so that when we rotate we aren't rotated by obstacles
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
