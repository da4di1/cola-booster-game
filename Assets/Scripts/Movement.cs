using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float pushPower = 850f;
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] AudioClip pushSound;
    [SerializeField] ParticleSystem mainPushParticles;
    [SerializeField] ParticleSystem rightPushParticles;
    [SerializeField] ParticleSystem leftPushParticles;
    [SerializeField] AudioSource rightThrusterAudioSource;
    [SerializeField] AudioSource leftThrusterAudioSource;

    Rigidbody myRigidbody;
    AudioSource mainAudioSource;


    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        mainAudioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Push();
        RotateByZ();
    }

    void Push()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartPushing();
        }
        else
        {
            StopPushing();
        }
    }

    void RotateByZ()
    {
        if (Input.GetKey(KeyCode.D))
        {
            StartRotatingRight();
        }
        else
        {
            StopRotatingRight();
        }

        if (Input.GetKey(KeyCode.A))
        {
            StartRotatingLeft();
        }
        else
        {
            StopRotatingLeft();
        }
    }

    void StartPushing()
    {

        if (!mainAudioSource.isPlaying)
        {
            mainAudioSource.PlayOneShot(pushSound);
        }
        if (!mainPushParticles.isPlaying)
        {
            mainPushParticles.Play();
        }
        myRigidbody.AddRelativeForce(Vector3.up * pushPower * Time.deltaTime);
    }

    void StopPushing()
    {
        mainAudioSource.Stop();
        mainPushParticles.Stop();
    }

    void StartRotatingRight()
    {

        if (!leftThrusterAudioSource.isPlaying)
        {
            leftThrusterAudioSource.PlayOneShot(pushSound);
        }
        if (!leftPushParticles.isPlaying)
        {
            leftPushParticles.Play();
        }
        Rotate(Vector3.back);
    }

    void StartRotatingLeft()
    {

        if (!rightThrusterAudioSource.isPlaying)
        {
            rightThrusterAudioSource.PlayOneShot(pushSound);
        }
        if (!rightPushParticles.isPlaying)
        {
            rightPushParticles.Play();
        }
        Rotate(Vector3.forward);
    }

    void StopRotatingRight()
    {
        leftThrusterAudioSource.Stop();
        leftPushParticles.Stop();
    }

    void StopRotatingLeft()
    {
        rightThrusterAudioSource.Stop();
        rightPushParticles.Stop();
    }

    void Rotate(Vector3 rotationVector)
    {
        myRigidbody.freezeRotation = true;
        transform.Rotate(rotationVector * rotationSpeed * Time.deltaTime);
        myRigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
    }
}
