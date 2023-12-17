using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideRadius : MonoBehaviour {

    private void OnCollisionEnter2D( Collision2D collision ) {

        if (collision.gameObject.tag == "EnemyPending" ) {

            collision.gameObject.tag = "Enemy";
            collision.gameObject.GetComponent<BoxCollider2D>( ).excludeLayers = 2 << 7;
            collision.gameObject.GetComponent<Rigidbody2D>( ).excludeLayers = 2 << 7;
        }
    }
}
