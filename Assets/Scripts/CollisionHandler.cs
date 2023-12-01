using System.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEngine;
using Unity.VisualScripting;
using System;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] AudioClip levelComplete;
    [SerializeField] AudioClip crash;
    [SerializeField] float soundEffectVolume = .5f;

    AudioSource audioSource;
    bool isTransitioning = false;

    int currentSceneIndex;
    void Start(){
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
    }
    //get index of current scene   
    void OnCollisionEnter(Collision other){
        if(isTransitioning){
            return;
        }
        
            switch (other.gameObject.tag){
                case "Friendly":
                    UnityEngine.Debug.Log("You landed on a launch Pad");
                    break;
                case "Finish":
                    UnityEngine.Debug.Log("You completed the level");
                    SuccessSequence();
                    break;
                default:
                    UnityEngine.Debug.Log("you Crashed");
                    CrashSequence();
                    break;
            }
        
    }

    void DisableMovement(){
        GetComponent<PlayerController>().enabled = false;
    }
    void SuccessSequence(){
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(levelComplete, soundEffectVolume);
        DisableMovement();
        Invoke("LoadNextScene", loadDelay);
    }
    void CrashSequence(){
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash, soundEffectVolume);
        DisableMovement();
        Invoke("ReloadScene", loadDelay);
    }
    void ReloadScene(){
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextScene(){
        int nextSceneIndex = currentSceneIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
