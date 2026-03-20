using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameMenu : MonoBehaviour {
    public void RetryGame() {
        SceneManager.LoadScene("Level");
    }

    public void ReturnToMainMenu() {
        SceneManager.LoadScene(0);
    }
}