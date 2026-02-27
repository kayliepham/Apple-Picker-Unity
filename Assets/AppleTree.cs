using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour {
    [Header("Inscribed")]

    // Prefab for instantiating apples
    public GameObject applePrefab;

    // Prefab for instantiating gold apples
    public GameObject goldApplePrefab;

    public GameObject badApplePrefab;

    [Range(0f,1f)] public float badAppleChance = 0.15f;

    // Chance an apple is gold (0.1 = 10%)
    [Range(0f, 1f)] public float goldChance = 0.10f;    

    // Speed at which the AppleTree moves
    public float speed = 1f;

    // Distance where AppleTree turns around
    public float leftAndRightEdge = 10f;

    // Chance that the AppleTree will change directions
    public float changeDirChance = 0.1f;

    // Seconds between Apples instantiations
    public float appleDropDelay = 1f;

    void Start() {
       // Start dropping apples
        Invoke( "DropApple", 2f );

    }

    void DropApple() {
        GameObject prefabToDrop;

        // 1) Poison/bad apple check first
        if (badApplePrefab != null && Random.value < badAppleChance) {
            prefabToDrop = badApplePrefab;
        }
        else {
            // 2) Otherwise, maybe gold
            if (goldApplePrefab != null && Random.value < goldChance) {
                prefabToDrop = goldApplePrefab;
            } else {
                prefabToDrop = applePrefab;
            }
        }

        GameObject apple = Instantiate(prefabToDrop);
        apple.transform.position = transform.position;

        Invoke("DropApple", appleDropDelay);
    }

    void Update () {
        // Basic Movement
        Vector3 pos = transform.position;

        pos.x += speed * Time.deltaTime;

        transform.position = pos;

        // Changing Direction
        if ( pos.x < -leftAndRightEdge ) {

        speed = Mathf.Abs( speed ); // Move right

        } else if ( pos.x > leftAndRightEdge ) {
            speed = -Mathf.Abs( speed ); // Move left
        // } else if ( Random.value < changeDirChance ) {
        //   speed *= -1; // Change direction
        }
    }

    void FixedUpdate() {
        // Random direction changes are now time-based due to FixedUpdate()
        if ( Random.value < changeDirChance ) {
            speed *= -1; // Change direction
        }
    }
}
