using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI healthText;
    [HideInInspector] public float health;
    private bool justTookDamage;
    [SerializeField]
    private ParticleSystem blood;
    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
        justTookDamage = false;
    }

    public void TakeDamage(float damage)
    {
        if (!justTookDamage) {
            blood.Play();
            health -= damage;
            healthText.text = Convert.ToString(health);
            StartCoroutine(timeBetweenDamage());
        }
    }
    
    IEnumerator timeBetweenDamage()
    {
        justTookDamage = true;
        yield return new WaitForSeconds(0.7f);
        justTookDamage = false;
    }
}
