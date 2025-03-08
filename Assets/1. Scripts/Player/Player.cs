using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerController playerController;
    public int health;

    public Action InteractItem;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        ReviveAfterFall();
    }

    void ReviveAfterFall()
    {
        if (isFallToEnd())
        {
            transform.position = GameManager.Instance.spawnPoint;
        }
    }

    bool isFallToEnd()
    {
        if (transform.position.y < -25f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
