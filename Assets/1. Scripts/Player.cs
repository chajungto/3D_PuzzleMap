using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerController playerController;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
}
