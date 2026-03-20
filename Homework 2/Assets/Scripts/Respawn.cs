using UnityEngine;
using System.Collections;

public class Respawn : MonoBehaviour {
    [SerializeField] private GameObject respawnPoint;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private PlayerHealth playerHealth;

    private bool isRespawning = false;

    private void OnTriggerEnter2D(Collider2D collision) {
        int layer = LayerMask.NameToLayer("Out Of Boundary");

        if (collision.gameObject.layer == layer && !isRespawning) {
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