using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class Variables {

    public static int iWaveCount = 1;
    public static float flSpawnMultiplier = 1.07f;
    public static float flSpawnrateMultiplier = 0.01f;

    public static int iPermaCoin = 0;
    public static int iTempCoin = 0;

    // spawner & enemy vars
    public static float flSpawnRate = 1.2f; // spawn interval (time between spawning an object)
    public static float flEnemyStatMultiplier = 1f; // base stat multiplier value
    public static float flSpeedMultiplier = 0.04f;
    public static float flHealthMultiplier = 0.04f;
    public static float flDamageMultiplier = 0.02f;

    // tower vars
    public static float flFireRate = 0.5f;
    public static float flTowerDamage = 30;
    public static float flCritChance = 0f;  // percentage
    public static float flCritDamageMult = 2f; // percentage
    public static float flBulletSpeed = 5f;
    public static float flRadius = 130f;

    public static readonly float flLineLength = 0.3f; // used for the healthbar size
    public static readonly int iEnemyCount = 10;
}