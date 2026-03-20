using System.Collections;
using UnityEngine;

public class DroppingPlatform : MonoBehaviour
{
    [SerializeField] private Rigidbody2D myRigidbody;

    private Vector3 startPosition;

    private void OnCollisionEnter2D(Collision2D collision) {
        DropPlatform(collision);
    }

    void Start() {
        startPosition = transform.position;
    }

    private void DropPlatform(Collision2D collision) {
        int layer = LayerMask.NameToLayer("Player");

        if (collision.gameObject.layer == layer) {
            startPosition = gameObject.transform.position;
            myRigidbody.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(DestroyPlatform());
        }
    }

   private IEnumerator DestroyPlatform() {
    yield return new WaitForSeconds(1f);

    GetComponent<SpriteRenderer>().enabled = false;
    GetComponent<Collider2D>().enabled = false;
    
    myRigidbody.linearVelocity = Vector2.zero;
    myRigidbody.angularVelocity = 0f;
    myRigidbody.bodyType = RigidbodyType2D.Kinematic;

    yield return new WaitForSeconds(3f);

    transform.position = startPosition;
    transform.rotation = Quaternion.identity;
    
    GetComponent<SpriteRenderer>().enabled = true;
    GetComponent<Collider2D>().enabled = true;
}
}
