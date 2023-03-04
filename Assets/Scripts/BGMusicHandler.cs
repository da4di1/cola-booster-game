using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class BGMusicHandler : MonoBehaviour
{
    public static BGMusicHandler instance;
 
 
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}