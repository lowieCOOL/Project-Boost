using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource thrustSound;
    KeyCode moveLeftKey = KeyCode.Q;
    KeyCode moveRightKey = KeyCode.D;
    [SerializeField] float thrustMagnitude = 10f;
    [SerializeField] float rotationThrustMagnitude = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thrustSound = GetComponent<AudioSource>();
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
            if (!thrustSound.isPlaying)
            {
                thrustSound.Play();
            } 

            rb.AddRelativeForce(Vector3.up * thrustMagnitude * Time.deltaTime);
        }
        else
        {
            thrustSound.Stop();
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
