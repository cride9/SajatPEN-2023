using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tower : MonoBehaviour {

    public GameObject bulletObject;
    private float flShotTime = 0f;

    // this list handles every bullet shot
    private List<Targeting> targets = new( );

    void Update( ) {

        // get all enemies inside radius
        GameObject[] enemies = GameObject.FindGameObjectsWithTag( "Enemy" );
        foreach ( GameObject item in enemies ) {

            // check fire rate
            if ( flShotTime <= Time.time - Variables.flFireRate ) {

                Targeting ExistingEnemy = targets.Find( find => find.target == item );
                if ( ExistingEnemy is not null && ExistingEnemy.Ignore )
                    continue;

                // add new projectile to the game -> place in the middle -> add projectile tag
                GameObject newObject = Instantiate( bulletObject );
                newObject.transform.position = Vector2.zero;
                newObject.tag = "Projectile";

                // add to targets queue
                targets.Add( new( item, newObject, Variables.flTowerDamage ) );

                // save current time for fire rate
                flShotTime = Time.time;
            }
        }

        // remove null objects (destoryed objects)
        targets.RemoveAll( item => item.bullet == null );

        // call fire physics
        targets.ForEach( target => target.OnUpdate( ) );
    }

    private void OnCollisionEnter2D( Collision2D collision ) {

        // destroy enemy if they're in contact with the player
        // TODO: player health & enemy dmg
        if ( collision.gameObject.tag == "Enemy" ) {

            Destroy( collision.gameObject.transform.parent.gameObject );
            //SceneManager.LoadScene( "GameMenu", LoadSceneMode.Additive );
            SceneManager.LoadScene( "GameOver" );
            Variables.bPause = true;
        }
    }
}

public class Targeting {

    public GameObject target;
    public GameObject bullet;

    public float damage;
    public Vector2 direction;

    public bool Ignore = false;

    public Targeting(GameObject tar, GameObject bul, float dam) {

        target = tar;
        bullet = bul;
        damage = dam;

        EnemyStats targetInfo = target.GetComponent<EnemyStats>( );

        if ( Random.value <= Variables.flCritChance) {
            target.name = "Crit";
            targetInfo.DealDamage( damage * Variables.flCritDamageMult );

        }
        else
        {
            targetInfo.DealDamage(damage);
        }

        // save direction
        direction = Vector2.MoveTowards( bullet.transform.position, target.transform.position, Variables.flBulletSpeed * Time.deltaTime );

        if ( damage >= targetInfo.GetStat( EnemyStats.STATS.HEALTH ) )
            Ignore = true;
    }
    public void OnUpdate() {

        // destroy bullet object if out of bound
        if ( Mathf.Abs( bullet.transform.position.x ) > 10 || Mathf.Abs( bullet.transform.position.y ) > 10 )
        {      
            MonoBehaviour.Destroy(bullet);
        }
            
        // move towards the shot direction
        bullet.transform.position = bullet.transform.position + ( Vector3 )direction;
    }

}
