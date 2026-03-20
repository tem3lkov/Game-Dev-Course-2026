using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [Header("Components")]
    [SerializeField] private Rigidbody2D myRigidbody;
    [SerializeField] private Animator myAnimator;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float jumpSpeed = 12f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck; 
    [SerializeField] private float groundCheckRadius = 0.2f; 
    [SerializeField] private LayerMask floorLayer; 

    private Vector2 moveInput;

    private void Update() {
        FlipSprite();
        UpdateJumpAnimation();
    }

    private void FixedUpdate() {
        Move();
    }

    private void OnMove(InputValue value) {
        moveInput = value.Get<Vector2>();
    }

    private void OnJump(InputValue value) {
        if (value.isPressed && IsGrounded()) {
            myRigidbody.linearVelocity = new Vector2(myRigidbody.linearVelocity.x, jumpSpeed);
        }
    }

    private void Move() {
        myRigidbody.linearVelocity = new Vector2(moveInput.x * moveSpeed, myRigidbody.linearVelocity.y);
    }

    private void FlipSprite() {
        bool hasHorizontalSpeed = Mathf.Abs(myRigidbody.linearVelocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isWalking", hasHorizontalSpeed);

        if (hasHorizontalSpeed) {
            float currentXScale = Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.linearVelocity.x) * currentXScale, transform.localScale.y);
        }
    }

    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, floorLayer);
    }

    private void UpdateJumpAnimation() {
        myAnimator.SetBool("isJumping", !IsGrounded());
    }
}