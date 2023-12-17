using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STATS = EnemyStats.STATS;

public class Bullet : MonoBehaviour {

    private void OnCollisionEnter2D( Collision2D collision ) {

        // destroy both objects on collision

        if ( collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyPending" ) {

            // get stats for the damage
            var stats = collision.gameObject.GetComponent<EnemyStats>( );

            // FIXME: passing damage from stats does not work
            // example: stats.DealDamage( stats.GetStat( STATS.DAMAGE ) ); deals 0 dmg
            stats.DealDamage( 30 );
            if ( stats.GetStat( STATS.HEALTH ) <= 0 ) 
                Destroy( collision.gameObject.transform.parent.gameObject );
            
            // always destroy current bullet on contact
            Destroy( this.gameObject );
        }
    }
}
