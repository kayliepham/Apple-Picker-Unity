using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour {
    public static float bottomY = -20f;

    [Header("Scoring")]
    public int points = 100;   // Red apples stay 100, Gold will be 200
    public bool isPoison = false;   // true only for bad apples


    void Update () {
        if ( transform.position.y < bottomY ) {
            Destroy(this.gameObject);

            // Only normal apples should trigger AppleMissed when falling
            if (!isPoison) {
                // Get a reference to the ApplePicker component of Main Camera
                ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
                // Call the public AppleMissed() method of apScript
                apScript.AppleMissed();
            }
        }
    }
}