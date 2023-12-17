using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyStats : MonoBehaviour {
    
    /* Base stats */
    readonly static float[ ] BaseStats = { 50f, 1f, 1f };

    /* Multiplied actual stats */
    private float[ ] Stats = new float[ BaseStats.Length ];
    private float[ ] MaxStats = new float[ BaseStats.Length ];
    private float Multiplier = 1f;

    /* Used to set stats */
    public void SetMultiplier( STATS stat, float multiplier ) {
        Stats[ ( int )stat ] = BaseStats[ ( int )stat ] * multiplier;
        MaxStats[ ( int )stat ] = BaseStats[ ( int )stat ] * multiplier;
        Multiplier = multiplier;
    }
    public void SetMultiplier( float multiplier ) {
        for ( int i = 0; i < BaseStats.Length; i++ ) {
            Stats[ i ] = BaseStats[ i ] * multiplier;
            MaxStats[ i ] = BaseStats[ i ] * multiplier;
        }
        Multiplier = multiplier;
    }

    /* Used to get stats */
    public float GetStat( STATS stat ) =>
        Stats[ ( int )stat ];

    /* Returns the max stats aka Multiplied original health */
    public float GetOriginalStat( STATS stat ) =>
        MaxStats[ ( int )stat ];

    public void DealDamage( float damage ) {
        Stats[ ( int )STATS.HEALTH ] -= damage;
    }

    /* Can be expanded */
    public enum STATS : int {

        HEALTH,
        DAMAGE,
        SPEED
    }
}
