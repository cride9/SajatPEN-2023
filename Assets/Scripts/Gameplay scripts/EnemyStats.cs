using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Variables;

public class EnemyStats : MonoBehaviour {
    
    /* Base stats */
    readonly static float[ ] BaseStats = { 25f, 0.8f, 0.75f }; // health, damage, speed

    /* Multiplied actual stats */
    private float[ ] Stats = new float[ BaseStats.Length ];
    private float[ ] Multiplier = { flHealthMultiplier, flDamageMultiplier, flSpeedMultiplier };

    /* Used to set stats */
    public void SetMultiplier( STATS stat, float multiplier ) {

        Stats[ ( int )stat ] = BaseStats[ ( int )stat ] + (BaseStats[ ( int )stat ] * multiplier);
        Multiplier[ ( int )stat ] = multiplier;
    }

    public void SetMultiplier( float multiplier ) {
        for ( int i = 0; i < BaseStats.Length; i++ ) {
            Stats[ i ] = BaseStats[ i ] + multiplier;
            Multiplier[ i ] = multiplier;
        }
    }

    /* Used to get stats */
    public float GetStat( STATS stat ) =>
        Stats[ ( int )stat ];

    /* Returns the max stats aka Multiplied original health */
    public float GetOriginalStat( STATS stat ) =>
        BaseStats[ ( int )stat ] + ( BaseStats[ ( int )stat ] * Multiplier[ ( int )stat ]);

    public void DealDamage( float damage ) =>
        Stats[ ( int )STATS.HEALTH ] -= damage;

    /* Can be expanded */
    public enum STATS : int {

        HEALTH,
        DAMAGE,
        SPEED
    }
}
