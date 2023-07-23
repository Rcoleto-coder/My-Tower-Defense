using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float initialSpeed = 10.0f;
    public float initialHealth = 100.0f;

    

    [HideInInspector]
    public float currentSpeed;

    private float currentHealth;
    public int worth = 50;
    public GameObject deathEffect;

    [Header("Unity Parameters")]
    public Image healthBar; 

    private bool isDead = false;


    void Start()
    {
        currentSpeed = initialSpeed;
        currentHealth = initialHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.fillAmount = currentHealth / initialHealth;

        if(currentHealth <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Slow (float factor)
    {
        currentSpeed = initialSpeed * (1.0f - factor);
    }

    void Die()
    {
        isDead = true;
        PlayerStats.money += worth;
        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5.0f);
        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }

    

}
