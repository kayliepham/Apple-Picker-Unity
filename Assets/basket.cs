using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour {
    public ScoreCounter scoreCounter;
    void Start () {
    // Find a GameObject named ScoreCounter in the Scene Hierarchy
        GameObject scoreGO = GameObject.Find( "ScoreCounter");
        // Get the ScoreCounter (Script) component of scoreGO
        scoreCounter = scoreGO.GetComponent<ScoreCounter>();
    }

    void Update () {
        // Get the current screen position of the mouse from Input
        Vector3 mousePos2D = Input.mousePosition;
        // The Camera’s z position sets how far to push the mouse into 3D
        // If this line causes a NullReferenceException, select the Main Camera
        // in the Hierarchy and set its tag to MainCamera in the Inspector.
        mousePos2D.z = -Camera.main.transform.position.z;
        // Convert the point from 2D screen space into 3D game world space
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D ); 
        // Move the x position of this Basket to the x position of the Mouse
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }


    void OnCollisionEnter(Collision coll) {
        // Find out what hit this basket
        GameObject collidedWith = coll.gameObject;

        // Only handle apples (red or gold) by tag
        if (!collidedWith.CompareTag("Apple")) return;

         // Read the points from the Apple script on that apple
        Apple appleScript = collidedWith.GetComponent<Apple>();

        // If it's poison: destroy it and count as a "miss" (lose a basket)
        if (appleScript != null && appleScript.isPoison) {
            Destroy(collidedWith);

            ApplePicker apScript = Camera.main.GetComponent<ApplePicker>();
            apScript.AppleMissed();

            return; // IMPORTANT: don't give points
        }

        // Normal apples (red/gold): award points
        int points = 100; // fallback
        if (appleScript != null) points = appleScript.points;

        Destroy(collidedWith);
        // Increase the score
        scoreCounter.score += points;
        HighScore.TRY_SET_HIGH_SCORE(scoreCounter.score);
    }
}