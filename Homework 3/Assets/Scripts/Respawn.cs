using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {
    [SerializeField] private GameObject respawnPoint;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private PlayerHealth playerHealth;

    private bool isRespawning = false;

    private void OnCollisionEnter2D(Collision2D collision) {
        if ((collision.gameObject.layer == LayerMask.NameToLayer("OutOfBoundary") || collision.gameObject.CompareTag("Enemy")) && !isRespawning) {
            StartCoroutine(HandleRespawn());
        }
    }

    private IEnumerator HandleRespawn() {
        isRespawning = true;

        playerHealth.TakeDamage(1);

        transform.position = respawnPoint.transform.position;
        playerRigidbody.linearVelocity = Vector2.zero;

        yield return new WaitForSeconds(0.1f);

        isRespawning = false;
    }
}