using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float curHealth;
    public float startHealth;
    public float maxHealth;
    public Image uiBar;


    private void Awake()
    {
        curHealth = startHealth;
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();

    }

    float GetPercentage()
    {
        return curHealth / maxHealth;
    }

    //¥ı«œ±‚
    public void Add(float value)
    {
        curHealth = Mathf.Min(curHealth + value, maxHealth);
    }

    //ª©±‚
    public void Subtract(float value)
    {
        curHealth = Mathf.Max(curHealth - value, 0);
    }
}
