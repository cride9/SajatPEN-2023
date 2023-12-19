using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug : MonoBehaviour {

    void Start( ) {

    }

    void Update( ) {

    }
}

public static class Variables {

    // spawner & enemy vars
    public static float flSpawnRate = 0.75f; // spawn interval (time between spawning an object)
    public static float flEnemyStatMultiplier = 1f; // base stat multiplier value

    // tower vars
    public static float flFireRate = 0.5f;
    public static float flTowerDamage = 30;

    public const float flLineLength = 0.3f; // used for the healthbar size

}