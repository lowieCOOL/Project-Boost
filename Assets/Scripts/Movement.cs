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

    [SerializeField] ParticleSystem mainThrusterParticles, leftThrusterParticles, rightThrusterParticles;

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
            StartThrusting();
        }
        else
        {
            StopThrusting();

        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(moveLeftKey))
        {
            RotateLeft();
        }
        else if (Input.GetKey(moveRightKey))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }

    }

    void StartThrusting()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(engineThrust);
        }

        if (!mainThrusterParticles.isPlaying)
        {
            mainThrusterParticles.Play();
        }

        rb.AddRelativeForce(Vector3.up * thrustMagnitude * Time.deltaTime);
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainThrusterParticles.Stop();
    }

    void RotateLeft()
    {
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }

        ApplyRotation(rotationThrustMagnitude);
    }

    void RotateRight()
    {
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }

        ApplyRotation(-rotationThrustMagnitude);
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //freeze rotation so that when we rotate we aren't rotated by obstacles
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }

    void StopRotating()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }
}
