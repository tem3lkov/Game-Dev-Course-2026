using UnityEngine;
using UnityEngine.SceneManagement;
public class WinTrigger : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            // Проверяваме дали са събрани ключовете
            if (KeysUI.instance.HasAllKeys()) {
                Debug.Log("You Win! Loading Win Scene...");
                SceneManager.LoadScene("WinScene");
            } else {
                Debug.Log("You need to collect all keys first!");
            }
        }
    }
}