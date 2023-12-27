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
    public bool CritDamageMult= false;
    
    // this value should be asignd inside editor
    public float UpgradeAmount = 0f;

    TextMeshProUGUI text = null;
    void Start( ) {
        // text child
        text = gameObject.transform.GetChild( 1 ).gameObject.GetComponent<TextMeshProUGUI>();

        // easiest way to update every text to the current upgrade lmao
        float backup = UpgradeAmount;
        UpgradeAmount = 0f;
        Upgrade( );
        UpgradeAmount = backup;
    }

    public void Upgrade() {

        switch ( true ) {
            case var _ when Firerate:
                // limit
                Variables.flFireRate = Mathf.Max( 0.2f, Variables.flFireRate + UpgradeAmount );
                text.text = $"{Math.Round(1f / Variables.flFireRate, 2)}/s";
                break;
            case var _ when Damage:
                Variables.flTowerDamage += UpgradeAmount;
                text.text = $"{Variables.flTowerDamage}";
                break;
            case var _ when CritChance:
                Variables.flCritChance += UpgradeAmount;
                text.text = $"{Variables.flCritChance}";
                break;
            case var _ when CritDamageMult:
                Variables.flCritDamageMult += UpgradeAmount;
                text.text = $"{Variables.flCritDamageMult}";
                break;
        }
    }
}
