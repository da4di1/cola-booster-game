using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    [SerializeField] float pushPower = 100f;

    public bool isPushing = false;


    void OnTriggerStay(Collider other) 
    {
        if (isPushing)
        {
            Push(other);
        }   
    }

    void Push(Collider collider)
    {
        GameObject objectToPush = collider.gameObject;

        while (objectToPush.tag != "Coke")
        {
            objectToPush = objectToPush.transform.parent.gameObject;
        }

        objectToPush.GetComponent<Rigidbody>().AddForce(Vector3.up * pushPower * Time.deltaTime);
    }
}
