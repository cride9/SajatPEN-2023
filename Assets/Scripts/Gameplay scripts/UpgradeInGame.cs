using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeInGame : MonoBehaviour {

    // these bools should be asigned inside editor
    public bool Firerate = false;
    public bool Damage = false;
    public bool CritChance = false;
    public bool CritDamageMult = false;
    public bool BulletSpeed = false;
    public bool Radius = false;

    // this value should be asignd inside editor
    public float UpgradeAmount = 0f;

    TextMeshProUGUI text = null;
    void Start( ) {
        // text child
        text = gameObject.transform.GetChild( 1 ).gameObject.GetComponent<TextMeshProUGUI>( );

        // easiest way to update every text to the current upgrade
        float backup = UpgradeAmount;
        UpgradeAmount = 0f;
        Upgrade( );
        UpgradeAmount = backup;
    }

    public void Upgrade( ) {

        switch ( true ) {

            case var _ when Firerate:
                // limit
                Variables.flFireRate = Mathf.Max( 0.15f, Variables.flFireRate + UpgradeAmount );
                text.text = $"{Math.Round( 1f / Variables.flFireRate, 2 )}/s";
                break;

            case var _ when Damage:
                Variables.flTowerDamage = Mathf.Min( 370f, Variables.flTowerDamage + UpgradeAmount );
                text.text = $"{Variables.flTowerDamage}";
                break;

            case var _ when CritChance:
                Variables.flCritChance = Mathf.Min( 0.35f, Variables.flCritChance + UpgradeAmount );
                text.text = $"{Math.Round(Variables.flCritChance * 100, 2)}%";
                break;

            case var _ when CritDamageMult:
                Variables.flCritDamageMult = Mathf.Min( 5f, Variables.flCritDamageMult + UpgradeAmount );
                text.text = $"{Math.Round(Variables.flCritDamageMult * 100, 2)}%";
                break;

            case var _ when BulletSpeed:
                Variables.flBulletSpeed = Mathf.Min( 14.5f, Variables.flBulletSpeed + UpgradeAmount );
                text.text = $"{Math.Round( Variables.flBulletSpeed, 2 )}";
                break;

            case var _ when Radius:
                Variables.flRadius = Mathf.Min( 200f, Variables.flRadius + UpgradeAmount );
                text.text = $"{Variables.flRadius}%";

                GameObject.Find( "Radius" ).transform.localScale = new Vector3( Variables.flRadius, Variables.flRadius, Variables.flRadius );
                break;
        }
    }
}
