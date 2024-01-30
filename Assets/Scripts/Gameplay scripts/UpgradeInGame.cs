using System;
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
    public int UpgradePrice = 0;

    float UpgradePriceBalance = 0f;
    TextMeshProUGUI currentValueText = null;
    TextMeshProUGUI upgradeText = null;
    TextMeshProUGUI tempCoins = null;
    TextMeshProUGUI balanceChangeText = null;

    Vector3 defaultBalanceChangePosition;
    bool isBalanceFloating = false;
    Color32 showColor = new Color32(255, 0, 0, 255);
    Color32 hiddenColor = new Color32(255, 0, 0, 0);

    void Start( ) {
        // text child
        balanceChangeText = GameObject.Find("BalanceChangeText").GetComponent<TextMeshProUGUI>();
        defaultBalanceChangePosition = balanceChangeText.transform.localPosition;
        balanceChangeText.faceColor = hiddenColor;


        currentValueText = gameObject.transform.GetChild( 1 ).gameObject.GetComponent<TextMeshProUGUI>( );
        upgradeText = gameObject.transform.GetChild( 2 ).gameObject.GetComponent<TextMeshProUGUI>( );
        tempCoins = GameObject.Find( "TempCoin" ).GetComponent<TextMeshProUGUI>( );


        // easiest way to update every text to the current upgrade
        Upgrade( true );
    }

    public void BalanceChangeAnimation(float amount)
    {
        balanceChangeText.faceColor = showColor; // Show the text by adding max opacity back
        balanceChangeText.text = $"-{amount}";
        isBalanceFloating = true;
    }



    void Update()
    {
        if (balanceChangeText != null )
        {


            if (isBalanceFloating)
            { 
                Vector3 pos = balanceChangeText.transform.localPosition;
                balanceChangeText.transform.localPosition = new Vector3(pos.x, pos.y + 2);
                float diff = balanceChangeText.transform.localPosition.y - defaultBalanceChangePosition.y;

                if (diff > 200.0)
                {
                    balanceChangeText.faceColor = hiddenColor;
                    balanceChangeText.transform.localPosition = defaultBalanceChangePosition;
                    isBalanceFloating = false;
                }
            }

        }
    }

    public void Upgrade( bool setDefaults = false ) {

        if ( UpgradePrice > Variables.iTempCoin && !setDefaults )
            return;

        if ( !setDefaults ) {

            Variables.iTempCoin -= UpgradePrice;
            UpgradePriceBalance = Mathf.Min(0.9f, UpgradePriceBalance + 0.15f );
            Variables.UpdateTempCoins( tempCoins );
            BalanceChangeAnimation(UpgradePrice);
        }
        float priceChange = 2f - UpgradePriceBalance;
        switch ( true ) {

            case var _ when Firerate:

                if ( !setDefaults ) {
                    Variables.flFireRate = Mathf.Max( 0.15f, Variables.flFireRate + UpgradeAmount );
                    UpgradePrice = Mathf.RoundToInt( ( float )UpgradePrice * priceChange );
                }
                currentValueText.text = $"{Math.Round( 1f / Variables.flFireRate, 2 )}/s";
                upgradeText.text = $"{UpgradePrice}";
                break;

            case var _ when Damage:
                if ( !setDefaults ) {
                    Variables.flTowerDamage = Mathf.Min( 370f, Variables.flTowerDamage + UpgradeAmount );
                    UpgradePrice = Mathf.RoundToInt( ( float )UpgradePrice * priceChange );
                }
                currentValueText.text = $"{Variables.flTowerDamage}";
                upgradeText.text = $"{UpgradePrice}";

                break;

            case var _ when CritChance:
                if ( !setDefaults ) {
                    Variables.flCritChance = Mathf.Min( 0.35f, Variables.flCritChance + UpgradeAmount );
                    UpgradePrice = Mathf.RoundToInt( ( float )UpgradePrice * priceChange );
                }
                currentValueText.text = $"{Math.Round(Variables.flCritChance * 100, 2)}%";
                upgradeText.text = $"{UpgradePrice}";

                break;

            case var _ when CritDamageMult:
                if ( !setDefaults ) {
                    Variables.flCritDamageMult = Mathf.Min( 5f, Variables.flCritDamageMult + UpgradeAmount );
                    UpgradePrice = Mathf.RoundToInt( ( float )UpgradePrice * priceChange );
                }
                currentValueText.text = $"{Math.Round(Variables.flCritDamageMult * 100, 2)}%";
                upgradeText.text = $"{UpgradePrice}";

                break;

            case var _ when BulletSpeed:
                if ( !setDefaults ) {
                    Variables.flBulletSpeed = Mathf.Min( 14.5f, Variables.flBulletSpeed + UpgradeAmount );
                    UpgradePrice = Mathf.RoundToInt( ( float )UpgradePrice * priceChange );
                }
                currentValueText.text = $"{Math.Round( Variables.flBulletSpeed, 2 )}";
                upgradeText.text = $"{UpgradePrice}";

                break;

            case var _ when Radius:
                if ( !setDefaults ) {
                    Variables.flRadius = Mathf.Min( 200f, Variables.flRadius + UpgradeAmount );
                    UpgradePrice = Mathf.RoundToInt( ( float )UpgradePrice * priceChange );
                }
                currentValueText.text = $"{Variables.flRadius}%";
                upgradeText.text = $"{UpgradePrice}";

                GameObject.Find( "Radius" ).transform.localScale = new Vector3( Variables.flRadius, Variables.flRadius, Variables.flRadius );
                break;
        }
    }
}
