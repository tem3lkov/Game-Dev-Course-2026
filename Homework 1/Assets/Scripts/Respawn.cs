using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private GameObject respawnPoint;
    [SerializeField] private Rigidbody2D playerRigidbody;
    private void OnTriggerEnter2D(Collider2D collision) {
        RespawnPlayer(collision);
    }

    void RespawnPlayer(Collider2D collision) {
        int layer = LayerMask.NameToLayer("Out Of Boundary");

        if (collision.gameObject.layer == layer) {
            transform.position = respawnPoint.transform.position;
            playerRigidbody.linearVelocity = Vector2.zero;
        }
    }
}
