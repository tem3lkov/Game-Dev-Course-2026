using UnityEngine;

public class Pickup : MonoBehaviour
{
    private bool isPickedUp = false;
    private void OnTriggerEnter2D(Collider2D collision) {
        PickItem(collision);
    }

    void PickItem(Collider2D collision) {

        if (isPickedUp) return;

        int layer = LayerMask.NameToLayer("Player");

        if (collision.gameObject.layer == layer) {
            isPickedUp = true;
            Destroy(gameObject);
            print("Picked up a collectable");
        }
    }
}
