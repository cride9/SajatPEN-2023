using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PermanentUpgrades : MonoBehaviour {

    // these bools should be asigned inside editor
    public bool Firerate = false;
    public bool Damage = false;
    public bool CritChance = false;
    public bool CritDamageMult = false;
    public bool BulletSpeed = false;
    public bool Radius = false;

    // this value should be asignd inside editor
    public float UpgradeAmount = 0f;
    public int UpgradePrice = 0;

    float UpgradePriceBalance = 0f;
    TextMeshProUGUI currentValueText = null;
    TextMeshProUGUI upgradeText = null;
    TextMeshProUGUI permCoins = null;

    void Start( ) {
        // text child
        currentValueText = gameObject.transform.GetChild( 1 ).gameObject.GetComponent<TextMeshProUGUI>( );
        upgradeText = gameObject.transform.GetChild( 2 ).gameObject.GetComponent<TextMeshProUGUI>( );
        permCoins = GameObject.Find( "PermCoinMenu" ).GetComponent<TextMeshProUGUI>( );

        // easiest way to update every text to the current upgrade
        Upgrade( true );
    }

    public void Upgrade( bool setDefaults = false ) {

        if ( UpgradePrice > Variables.iPermaCoin && !setDefaults )
            return;

        if ( !setDefaults ) {

            Variables.iPermaCoin -= UpgradePrice;
            UpgradePriceBalance = Mathf.Min( 0.9f, UpgradePriceBalance + 0.15f );
            Variables.UpdatePermaCoins( permCoins );
        }
        Variables.SaveVariables( );
        float priceChange = 2f - UpgradePriceBalance;
        switch ( true ) {

            case var _ when Firerate:

                if ( !setDefaults ) {
                    Variables.flBaseFireRate = Mathf.Max( 0.15f, Variables.flBaseFireRate + UpgradeAmount );
                    UpgradePrice = Mathf.RoundToInt( ( float )UpgradePrice * priceChange );
                }
                currentValueText.text = $"{Math.Round( 1f / Variables.flBaseFireRate, 2 )}/s";
                upgradeText.text = $"{UpgradePrice}";
                break;

            case var _ when Damage:
                if ( !setDefaults ) {
                    Variables.flBaseTowerDamage = Mathf.Min( 370f, Variables.flBaseTowerDamage + UpgradeAmount );
                    UpgradePrice = Mathf.RoundToInt( ( float )UpgradePrice * priceChange );
                }
                currentValueText.text = $"{Variables.flBaseTowerDamage}";
                upgradeText.text = $"{UpgradePrice}";

                break;

            case var _ when CritChance:
                if ( !setDefaults ) {
                    Variables.flBaseCritChance = Mathf.Min( 0.35f, Variables.flBaseCritChance + UpgradeAmount );
                    UpgradePrice = Mathf.RoundToInt( ( float )UpgradePrice * priceChange );
                }
                currentValueText.text = $"{Math.Round( Variables.flBaseCritChance * 100, 2 )}%";
                upgradeText.text = $"{UpgradePrice}";

                break;

            case var _ when CritDamageMult:
                if ( !setDefaults ) {
                    Variables.flBaseCritDamageMult = Mathf.Min( 5f, Variables.flBaseCritDamageMult + UpgradeAmount );
                    UpgradePrice = Mathf.RoundToInt( ( float )UpgradePrice * priceChange );
                }
                currentValueText.text = $"{Math.Round( Variables.flBaseCritDamageMult * 100, 2 )}%";
                upgradeText.text = $"{UpgradePrice}";

                break;

            case var _ when BulletSpeed:
                if ( !setDefaults ) {
                    Variables.flBaseBulletSpeed = Mathf.Min( 14.5f, Variables.flBaseBulletSpeed + UpgradeAmount );
                    UpgradePrice = Mathf.RoundToInt( ( float )UpgradePrice * priceChange );
                }
                currentValueText.text = $"{Math.Round( Variables.flBaseBulletSpeed, 2 )}";
                upgradeText.text = $"{UpgradePrice}";

                break;

            case var _ when Radius:
                if ( !setDefaults ) {
                    Variables.flBaseRadius = Mathf.Min( 200f, Variables.flBaseRadius + UpgradeAmount );
                    UpgradePrice = Mathf.RoundToInt( ( float )UpgradePrice * priceChange );
                }
                currentValueText.text = $"{Variables.flBaseRadius}%";
                upgradeText.text = $"{UpgradePrice}";

                GameObject.Find( "Radius" ).transform.localScale = new Vector3( Variables.flBaseRadius, Variables.flBaseRadius, Variables.flBaseRadius );
                break;
        }
    }
}
