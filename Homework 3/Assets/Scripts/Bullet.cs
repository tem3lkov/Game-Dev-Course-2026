using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField] private GameObject explosionParticles;
    [SerializeField] private float bulletSpeed = 20f;

    private Rigidbody2D myRigidbody;
    private PlayerController player;
    private float xSpeed;

    void Start() {
        myRigidbody = GetComponent<Rigidbody2D>();
        player = FindFirstObjectByType<PlayerController>();
        xSpeed = Mathf.Sign(player.transform.localScale.x) * bulletSpeed;
        Destroy(gameObject, 1f);
    }

    void Update() {
        myRigidbody.linearVelocity = new Vector2(xSpeed, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("Enemy")) {
            Destroy(collision.gameObject);
            Instantiate(explosionParticles, transform.position, Quaternion.identity);
        }
    }
}
