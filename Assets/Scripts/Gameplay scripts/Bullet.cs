using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STATS = EnemyStats.STATS;

public class Bullet : MonoBehaviour {

    public Tower tower = null;
    private void Start( ) {

        // get tower object
        tower = GameObject.Find( "Tower" ).GetComponent<Tower>();
    }

    private void OnCollisionEnter2D( Collision2D collision ) {

        // destroy both objects on collision

        if ( collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyPending" ) {

            // get stats for the damage
            var stats = collision.gameObject.GetComponent<EnemyStats>( );

            UpdateHealth( stats, collision.transform.parent.GetChild( 1 ).gameObject );
            if ( stats.GetStat( STATS.HEALTH ) <= 0 ) 
                Destroy( collision.gameObject.transform.parent.gameObject );
            
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
}
