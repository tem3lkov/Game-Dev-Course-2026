using UnityEngine;

public class JumpingPad : MonoBehaviour
{
    [SerializeField] private float jumpStrength = 20f;
    private void OnTriggerEnter2D(Collider2D collision) {
        UseJumpPad(collision);
    }

    private void UseJumpPad(Collider2D collision) {
        int layer = LayerMask.NameToLayer("Player");

        if (collision.gameObject.layer == layer) {
            Rigidbody2D player = collision.gameObject.GetComponent<Rigidbody2D>();
            player.linearVelocity = new Vector2(player.linearVelocity.x, jumpStrength);
        }
    }
}
