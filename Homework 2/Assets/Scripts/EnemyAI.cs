using UnityEngine;

public class EnemyAI : MonoBehaviour {

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float aggroRange = 6f;
    [SerializeField] private Rigidbody2D myRigidbody;

    private Transform playerTransform;

    private void Start() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
    }

    private void FixedUpdate() {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer <= aggroRange) {
            ChasePlayer();
        } else {
            myRigidbody.linearVelocity = new Vector2(0, myRigidbody.linearVelocity.y);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(1);
        }
    }

    private void ChasePlayer() {
        float direction = Mathf.Sign(playerTransform.position.x - transform.position.x);
        myRigidbody.linearVelocity = new Vector2(direction * moveSpeed, myRigidbody.linearVelocity.y);
        transform.localScale = new Vector2(direction * Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }

}