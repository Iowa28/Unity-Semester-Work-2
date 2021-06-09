using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField]
    private float maxHelath;
    private float currentHealth;
    private bool isDead = false;

    [SerializeField]
    private Text healthText;

    // [SerializeField]
    // private UnityEvent onDamage;
    // [SerializeField]
    // private UnityEvent onDeath;
    [SerializeField] 
    private AudioManager audioManager;

    void Start() 
    {
        currentHealth = maxHelath;
    }


    public void TakeDamage(float amount)
    {
        if (isDead)
            return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;

            Die();
        } else
        {
            audioManager.PlaySound("PlayerDamage");
        }

        healthText.text = "Health " + currentHealth;
    }

    void Die()
    {
        audioManager.PlaySound("PlayerDeath");
        Debug.Log("You're dead now");
        FindObjectOfType<GameManager>().EndGame();
    }
}
