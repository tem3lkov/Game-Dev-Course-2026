using UnityEngine;
using UnityEngine.UI;

public class KeysUI : MonoBehaviour {

    public static KeysUI instance;

    [SerializeField] private Sprite pickedKeySprite;
    [SerializeField] private Sprite emptyKeySprite;


    private Image[] keys;
    private int keysCollected = 0;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
            return;
        }

        int childCount = transform.childCount;
        keys = new Image[childCount];

        for (int i = 0; i < childCount; i++) {
            keys[i] = transform.GetChild(i).GetComponent<Image>();

            keys[i].overrideSprite = emptyKeySprite;
        }
    }

    public void AddKey() {
        if (keysCollected < keys.Length) {
            keys[keysCollected].overrideSprite = pickedKeySprite;
            keysCollected++;
        }
    }

    public bool HasAllKeys() {
        return keysCollected >= keys.Length;
    }
}