using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppClosing : MonoBehaviour
{
    void Update()
    {
        QuitApp();
    }

    void QuitApp()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("App is closed");
            Application.Quit();
        }
    }
}
