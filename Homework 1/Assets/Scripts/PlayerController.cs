using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 moveInput;


    [SerializeField] private Rigidbody2D myRigidbody;

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpSpeed = 12f;

    private void FixedUpdate() {
        Move();
        FlipSprite();
    }

    void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value) {
        int layer = LayerMask.GetMask("Floor");

        if (!myRigidbody.IsTouchingLayers(layer)) {
            return;
        }

        if (value.isPressed) {
            myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocity.x, jumpSpeed);
        }
    }
     

    void Move() {
        myRigidbody.linearVelocity = new Vector2(moveInput.x * moveSpeed, myRigidbody.linearVelocity.y);
    }

    void FlipSprite() {
        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody.linearVelocity.x) > Mathf.Epsilon;

        if (hasHorizontalSpeed) {
            float currentXScale = Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.linearVelocity.x) * currentXScale, transform.localScale.y);
        }
    }
}
