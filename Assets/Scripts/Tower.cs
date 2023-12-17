using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class Tower : MonoBehaviour {

    // this determines the fire rate
    public float flFireRate = 0.1f; 

    public GameObject bulletObject;
    private float flShotTime = 0f;

    // this list handles every bullet shot
    private List<Targeting> targets = new( );

    void Update( ) {

        // get all enemies inside radius
        var enemies = GameObject.FindGameObjectsWithTag( "Enemy" );
        foreach ( var item in enemies ) {

            // check fire rate
            if ( flShotTime < Time.time - flFireRate ) {

                // add new projectile to the game -> place in the middle -> add projectile tag
                GameObject newObject = Instantiate( bulletObject );
                newObject.transform.position = Vector2.zero;
                newObject.tag = "Projectile";

                // add to targets queue
                targets.Add( new( item, newObject ) );

                // save current time for fire rate
                flShotTime = Time.time;
            }
        }

        // remove null objects (destoryed objects)
        targets.RemoveAll( item => item.bullet == null );

        // call fire physics
        targets.ForEach( target => target.OnUpdate( ) );
    }
}

public class Targeting {

    public GameObject target;
    public GameObject bullet;
    public Vector2 direction;

    public Targeting(GameObject tar, GameObject bul) {

        target = tar;
        bullet = bul;

        // save direction
        direction = Vector2.MoveTowards( bullet.transform.position, target.transform.position, 5f * Time.deltaTime );
    }
    public void OnUpdate() {

        // destroy both objects if out of bound
        if ( Mathf.Abs( bullet.transform.position.x ) > 20 || Mathf.Abs( bullet.transform.position.y ) > 20 ) {

            MonoBehaviour.Destroy( target );
            MonoBehaviour.Destroy( bullet );
        }

        // move to the shot direction
        bullet.transform.position = bullet.transform.position + (Vector3)direction;
    }
}
