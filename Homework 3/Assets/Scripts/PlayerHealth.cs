using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] private int maxHealth = 3;

    [Header("Components")]
    [SerializeField] private Animator myAnimator;

    [Header("Low Health Effect")]
    [SerializeField] private GameObject redVignetteShader;

    private int currentHealth;


    private void Start() {
        currentHealth = maxHealth;

        HealthUI.instance.UpdateHealth(currentHealth);
        redVignetteShader.SetActive(false);
    }

    public void TakeDamage(int damageAmount) {
        currentHealth -= damageAmount;
        currentHealth = Mathf.Max(currentHealth, 0);

        HealthUI.instance.UpdateHealth(currentHealth);

        myAnimator.SetTrigger("Hurt");
        CameraShake.instance.TriggerShake(0.2f, 0.3f);

        if (currentHealth == 1 && redVignetteShader != null) {
            redVignetteShader.SetActive(true);
        }

        if (currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        Debug.Log("Player Died! Showing Lose Screen...");
        SceneManager.LoadScene("LoseScene");
    }
}
