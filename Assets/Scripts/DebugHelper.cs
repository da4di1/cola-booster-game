using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugHelper : MonoBehaviour
{
    CollisionHandler collisionHandlerScript;
    BoxCollider[] myCollider;
    

    void Start() 
    {
        myCollider = GetComponentsInChildren<BoxCollider>();
        collisionHandlerScript = GetComponent<CollisionHandler>();
    }

    void Update()
    {
        SwitchCollision();
        ReloadLevel();
        LoadNextLevel();
    }

    void SwitchCollision()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach(BoxCollider collider in myCollider)
                {
                    collider.enabled = !collider.enabled;
                }
        }
    }

    void ReloadLevel()
    {   
        if (Input.GetKeyDown(KeyCode.R))
        {
            collisionHandlerScript.RestartLevel();
        }
    }

    void LoadNextLevel()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            collisionHandlerScript.LoadNextLevel();
        }
    }
}
