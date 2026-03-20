using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public void PlayGame() {
        SceneManager.LoadScene("Level");
    }
    public void ReturnToMainMenu() {
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}