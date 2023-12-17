using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private void OnCollisionEnter2D( Collision2D collision ) {

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyPending" ) {

            Destroy( collision.gameObject );
            Destroy( this.gameObject );
        }
    }
}
