using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController playerController;
    public int health;
    int maxHealth = 100;

    public ItemData itemData;
    public Action interactItem;

    void Awake()
    {
        GameManager.Instance.Player = this;
        playerController = GetComponent<PlayerController>();
    }

    private void Update()
    {
        Revive();
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Revive();
        }
    }

    void Revive()
    {
        if (isFallToEnd() || health <= 0)
        {
            transform.position = GameManager.Instance.spawnPoint;
            health = maxHealth;
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
