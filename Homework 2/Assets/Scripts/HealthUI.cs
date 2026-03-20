using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {
    public static HealthUI instance;

    [Header("Heart Sprites")]
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private Image[] hearts;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
            return;
        }

        int childCount = transform.childCount;
        hearts = new Image[childCount];

        for (int i = 0; i < childCount; i++) {
            hearts[i] = transform.GetChild(i).GetComponent<Image>();
        }
    }

    public void UpdateHealth(int currentHealth) {
        for (int i = 0; i < hearts.Length; i++) {
            if (i < currentHealth) {
                hearts[i].sprite = fullHeart;
            } else {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}