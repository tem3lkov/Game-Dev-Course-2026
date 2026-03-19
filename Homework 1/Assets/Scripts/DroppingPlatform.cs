using UnityEngine;

public class DroppingPlatform : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody;

    private void OnCollisionEnter2D(Collision2D collision) {
        DropPlatform(collision);
    }

    void DropPlatform(Collision2D collision) {
        int layer = LayerMask.NameToLayer("Player");

        if (collision.gameObject.layer == layer) {
            myRigidbody.bodyType = RigidbodyType2D.Dynamic;

        }
    }
}
