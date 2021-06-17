using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip crashSound, succesSound;
    [SerializeField] ParticleSystem crashParticles, succesParticles;

    AudioSource audioSource;

    bool isTransitioning = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isTransitioning) {return;}

        switch (collision.gameObject.tag)
        {
            case "Finish":
                StartSuccesSequence();
                break;
            case "Friendly":

                break;
            case "Fuel":

                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartCrashSequence()
    {
        DisableMovement();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSound);

        crashParticles.Play();

        Invoke("ReloadCurrentScene", levelLoadDelay);
    }

    void StartSuccesSequence()
    {
        DisableMovement();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(succesSound);

        succesParticles.Play();

        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void DisableMovement()
    {
        Movement movementScript = GetComponent<Movement>();   //gets movement script
        movementScript.enabled = false;     //disables movement
    }

    private void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

    private void ReloadCurrentScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
