using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController playerController;
    public Health health;

    public ItemData itemData;
    public Action interactItem;

    void Awake()
    {
        GameManager.Instance.Player = this;
        playerController = GetComponent<PlayerController>();
        health = GetComponent<Health>();
    }

    private void Update()
    {
        Revive();
    }

    public void Heal(int amount)
    {
        health.Add(amount);
    }

    public void TakeDamage(int amount)
    {
        health.Subtract(amount);  
        if(health.curHealth <= 0)
        {
            Revive();
        }
    }

    void Revive()
    {
        if (isFallToEnd() || health.curHealth <= 0)
        {
            transform.position = GameManager.Instance.spawnPoint;
            Heal(100);
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
