using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsideRadius : MonoBehaviour {

    private void OnCollisionEnter2D( Collision2D collision ) {

        // checks enemy getting inside the radius
        if (collision.gameObject.tag == "EnemyPending" ) {

            collision.gameObject.tag = "Enemy";

            // excluding layer 7 (layer 8 in editor)
            collision.gameObject.GetComponent<BoxCollider2D>( ).excludeLayers = 2 << 7;
            collision.gameObject.GetComponent<Rigidbody2D>( ).excludeLayers = 2 << 7;
        }
    }
}
