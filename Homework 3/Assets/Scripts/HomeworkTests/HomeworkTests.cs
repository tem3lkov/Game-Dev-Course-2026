using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class HomeworkTests {
    [UnityTest]
    public IEnumerator PlayerTakesDamage_LosesLife() {
        // Test 1: Deal 1 damage
        GameObject uiObj = new GameObject("FakeUI");
        uiObj.AddComponent<HealthUI>();

        GameObject camObj = new GameObject("FakeCamera");
        camObj.AddComponent<CameraShake>();

        GameObject playerObj = new GameObject("TestPlayer");
        playerObj.AddComponent<Animator>();
        PlayerHealth playerHealth = playerObj.AddComponent<PlayerHealth>();

        // We use Reflection to assign private [SerializeField] references
        var vignetteField = typeof(PlayerHealth).GetField("redVignetteShader", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        vignetteField.SetValue(playerHealth, new GameObject("FakeShader"));

        var animatorField = typeof(PlayerHealth).GetField("myAnimator", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        animatorField.SetValue(playerHealth, playerObj.GetComponent<Animator>());

        yield return null;

        playerHealth.TakeDamage(1);

        var currentHealthField = typeof(PlayerHealth).GetField("currentHealth", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        int finalHealth = (int)currentHealthField.GetValue(playerHealth);

        // Assert: Verify health dropped from 3 to 2
        Assert.AreEqual(2, finalHealth, "Health should drop from 3 to 2 after taking 1 damage");
    }

    // Test 2: When we jump on a spring to change the player's Y position
    [UnityTest]
    public IEnumerator SpringPad_ChangesPlayerYVelocity() {
        GameObject padObj = new GameObject("JumpingPad");
        padObj.transform.position = Vector3.zero;
        BoxCollider2D padCol = padObj.AddComponent<BoxCollider2D>();
        padCol.isTrigger = true;
        padObj.AddComponent<JumpingPad>();

        // Arange: Create the player directly above the pad
        GameObject playerObj = new GameObject("Player");
        playerObj.layer = LayerMask.NameToLayer("Player");
        playerObj.transform.position = new Vector3(0, 2f, 0);
        Rigidbody2D playerRb = playerObj.AddComponent<Rigidbody2D>();
        BoxCollider2D playerCol = playerObj.AddComponent<BoxCollider2D>();

        yield return new WaitForSeconds(1f);

        // Assert: The player should have hit the trigger and been launched upward
        Assert.Greater(playerRb.linearVelocity.y, 0f, "Player's Y velocity should be positive after touching the pad");
    }
}