using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float loadingNewLevelDelay = 3f;
    [SerializeField] float reloadingLevelDelay = 1.5f;
    [SerializeField] AudioClip smashSound;
    [SerializeField] AudioClip waterPlopSound;
    [SerializeField] AudioClip successSound;
    [SerializeField] AudioClip failSound;
    [SerializeField] ParticleSystem crushParticles;
    [SerializeField] ParticleSystem waterPlopParticles;
    [SerializeField] ParticleSystem toiletExplosionParticles; 
    [SerializeField] GameObject toiletPusher;

    Movement myMovement;
    AudioSource myAudioSource;

    bool isTransitioning = false;


    public void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex); 
    }
    
    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;

        SceneManager.LoadScene(nextSceneIndex); 
    }

    void Start()
    {
        myMovement = GetComponent<Movement>();
        myAudioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(Collision other) 
    {
        if (isTransitioning) { return; }

        switch(other.gameObject.tag)
        {
            case "Respawn":
                Debug.Log("It's start");
                break;
            case "Finish":
                HandleFinishLevel(other);
                break;
            default:
                HandleCrush();
                break;
        }    
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Water")
            HandleWater(other);    
    }

    void HandleWater(Collider waterObject)
    {
        waterPlopParticles.Play();
        myMovement.enabled = false;
        waterObject.gameObject.GetComponent<MeshRenderer>().material.color = Color.black;
    }

    void HandleFinishLevel(Collision finishObject)
    {
        isTransitioning = true;

        HandleFinishAudio(finishObject);
        toiletExplosionParticles.Play();
        toiletPusher.GetComponent<Pusher>().isPushing = true;

        myMovement.enabled = false;
        Invoke("LoadNextLevel", loadingNewLevelDelay);
    }

    void HandleCrush()
    {
        isTransitioning = true;

        HandleCrushAudio();
        crushParticles.Play();

        myMovement.enabled = false;
        Invoke("RestartLevel", reloadingLevelDelay);
    }

    void HandleFinishAudio(Collision finishObject)
    {
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(waterPlopSound);
        finishObject.gameObject.GetComponent<AudioSource>().PlayDelayed(0.2f);
        myAudioSource.clip = successSound;
        myAudioSource.PlayDelayed(0.5f);
    }

    void HandleCrushAudio()
    {
        myAudioSource.Stop();
        myAudioSource.PlayOneShot(smashSound);
        myAudioSource.clip = failSound;
        myAudioSource.PlayDelayed(0.5f);
    }
}
