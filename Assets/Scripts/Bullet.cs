using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private void OnCollisionEnter2D( Collision2D collision ) {

        // destroy both objects on collision

        // TODO: make health & dmg system and only destroy if enemy health is 0 or below
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyPending" ) {

            Destroy( collision.gameObject );
            Destroy( this.gameObject );
        }
    }
}
