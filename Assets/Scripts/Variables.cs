using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public static class Variables {

    public static bool bPause = true;

    public static int iWaveCount = 1;
    public static float flSpawnMultiplier = 1.07f;
    public static float flSpawnrateMultiplier = 0.01f;

    public static int iPermaCoin = 0;
    public static int iTempCoin = 0;

    // spawner & enemy vars
    public static float flSpawnRate = 1.2f; // spawn interval (time between spawning an object)
    public static float flEnemyStatMultiplier = 1f; // base stat multiplier value
    public static float flSpeedMultiplier = 0.075f;
    public static float flHealthMultiplier = 0.08f;
    public static float flDamageMultiplier = 0.02f;

    // tower vars
    public static float flFireRate = 0.5f;
    public static float flTowerDamage = 30;
    public static float flCritChance = 0f;  // percentage
    public static float flCritDamageMult = 2f; // percentage
    public static float flBulletSpeed = 5f;
    public static float flRadius = 130f;

    public static float flBaseFireRate = 0.5f;
    public static float flBaseTowerDamage = 30;
    public static float flBaseCritChance = 0f;  // percentage
    public static float flBaseCritDamageMult = 2f; // percentage
    public static float flBaseBulletSpeed = 5f;
    public static float flBaseRadius = 130f;

    public static readonly float flLineLength = 0.3f; // used for the healthbar size
    public static readonly int iEnemyCount = 10;


    public static void SaveVariables( ) {

        PlayerPrefs.SetFloat( "flBaseFireRate", flBaseFireRate );
        PlayerPrefs.SetFloat( "flBaseTowerDamage", flBaseTowerDamage );
        PlayerPrefs.SetFloat( "flBaseCritChance", flBaseCritChance );
        PlayerPrefs.SetFloat( "flBaseCritDamageMult", flBaseCritDamageMult );
        PlayerPrefs.SetFloat( "flBaseBulletSpeed", flBaseBulletSpeed );
        PlayerPrefs.SetFloat( "flBaseRadius", flBaseRadius );
        PlayerPrefs.SetInt( "iPermaCoin", iPermaCoin );
    }

    public static void LoadVariables( ) {

        PlayerPrefs.GetInt( "iPermaCoin", iPermaCoin );
        PlayerPrefs.GetFloat( "flBaseFireRate", flBaseFireRate );
        PlayerPrefs.GetFloat( "flBaseTowerDamage", flBaseTowerDamage );
        PlayerPrefs.GetFloat( "flBaseCritChance", flBaseCritChance );
        PlayerPrefs.GetFloat( "flBaseCritDamageMult", flBaseCritDamageMult );
        PlayerPrefs.GetFloat( "flBaseBulletSpeed", flBaseBulletSpeed );
        PlayerPrefs.GetFloat( "flBaseRadius", flBaseRadius );
    }

    public static void UpdateCoins( TextMeshProUGUI tempCoins, TextMeshProUGUI permaCoins ) {

        tempCoins.text = FormatCurrency( iTempCoin );
        permaCoins.text = FormatCurrency( iPermaCoin );
    }

    public static void UpdateTempCoins( TextMeshProUGUI tempCoins ) =>
        tempCoins.text = FormatCurrency( iTempCoin );

    public static void UpdatePermaCoins( TextMeshProUGUI permaCoins ) =>
        permaCoins.text = FormatCurrency( iPermaCoin );

    private static string FormatCurrency( int currency ) {

        if ( currency > 1000000 )
            return $"{System.Math.Round( currency / 1000000f, 2 )}M";

        if ( currency > 1000 )
            return $"{System.Math.Round( currency / 1000f, 2 )}K";

        return currency.ToString( );
    }

    public static void UpdateTempValues() {

        flFireRate = flBaseFireRate;
        flTowerDamage = flBaseTowerDamage;
        flCritChance = flBaseCritChance;
        flCritDamageMult = flBaseCritDamageMult;
        flBulletSpeed = flBaseBulletSpeed;
        flRadius = flBaseRadius;
    }
}