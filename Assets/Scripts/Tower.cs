using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class Tower : MonoBehaviour {

    public GameObject bulletObject;
    public GameObject radiusObject;
    private float flShotTime = 0f;

    private List<Targeting> targets = new( );

    void Update( ) {

        var enemies = GameObject.FindGameObjectsWithTag( "Enemy" );
        foreach ( var item in enemies ) {

            float distance = Vector2.Distance( Vector2.zero, item.transform.position );
            if ( radiusObject.transform.localScale.x * 3 > distance && flShotTime < Time.time - 0.05f) {

                GameObject newObject = Instantiate( bulletObject );
                newObject.transform.position = Vector2.zero;
                newObject.tag = "Projectile";
                targets.Add( new( item, newObject ) );
                flShotTime = Time.time;
            }
        }

        targets.RemoveAll( item => item.bullet == null );
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
        direction = Vector2.MoveTowards( bullet.transform.position, target.transform.position, 5f * Time.deltaTime );
    }
    public void OnUpdate() {

        if ( Mathf.Abs( bullet.transform.position.x ) > 20 || Mathf.Abs( bullet.transform.position.y ) > 20 ) {

            MonoBehaviour.Destroy( target );
            MonoBehaviour.Destroy( bullet );
        }

        bullet.transform.position = bullet.transform.position + (Vector3)direction;
    }
}
