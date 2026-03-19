using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody;

    [SerializeField] private float maxDistance = 3f;
    [SerializeField] private float moveSpeed = 1.0f;

    private float startX;
    private int direction = 1;
    void Start()
    {
        startX = transform.position.x;
    }

    void FixedUpdate() {
        Move();
       
    }

    void Move() {
        float offset = transform.position.x - startX;

        if (offset >= maxDistance) {
            direction = -1;
        } else if (offset <= -maxDistance) {
            direction = 1;
        }

        myRigidbody.linearVelocity = new Vector2(direction * moveSpeed, myRigidbody.linearVelocity.y);
    }
}
