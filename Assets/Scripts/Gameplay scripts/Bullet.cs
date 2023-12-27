using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STATS = EnemyStats.STATS;
using static Variables;
using TMPro;

public class Bullet : MonoBehaviour {

    public Tower tower = null;
    private TextMeshProUGUI tempCoins = null;
    private TextMeshProUGUI permaCoins = null;

    private void Start( ) {

        // get tower object
        tower = GameObject.Find( "Tower" ).GetComponent<Tower>();
        tempCoins = GameObject.Find("TempCoin").GetComponent<TextMeshProUGUI>();
        permaCoins = GameObject.Find("PermaCoin").GetComponent <TextMeshProUGUI>();
    }

    private void OnCollisionEnter2D( Collision2D collision ) {

        // destroy both objects on collision

        if ( collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyPending" ) {

            // get stats for the damage
            var stats = collision.gameObject.GetComponent<EnemyStats>( );

            UpdateHealth( stats, collision.transform.parent.GetChild( 1 ).gameObject );
            if ( stats.GetStat( STATS.HEALTH ) <= 0 ) {
                Destroy( collision.gameObject.transform.parent.gameObject );
                iTempCoin += Mathf.RoundToInt( Random.Range( 1, 3 ) ) * iWaveCount;

                if ( Random.value < 0.6f )
                    iPermaCoin += 1 * iWaveCount;

                tempCoins.text = FormatCurrency(iTempCoin);
                permaCoins.text = FormatCurrency(iPermaCoin);
            }

            // always destroy current bullet on contact
            Destroy( this.gameObject );
        }
    }


    // this function updates the health (line length)
    private void UpdateHealth( EnemyStats stats, GameObject child ) {

        // stats - enemy stats
        // child - the healthbar gameobject
        var LineRenderer = child.GetComponent<LineRenderer>( );

        // calculate the hp%
        float hpPercent = stats.GetStat( STATS.HEALTH ) / stats.GetOriginalStat( STATS.HEALTH );

        // change the line end position
        LineRenderer.SetPosition( 1, new Vector3( Variables.flLineLength * hpPercent, 0f, 0f ) );
    }

    private string FormatCurrency(int currency) {

        if ( currency > 1000000 )
            return $"{System.Math.Round( currency / 1000000f, 2 )}M";

        if ( currency > 1000 )
            return $"{System.Math.Round( currency / 1000f, 2 )}K";

        return currency.ToString();
    } 
}
