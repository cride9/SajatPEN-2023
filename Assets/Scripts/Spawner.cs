using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour {

    /* Added inside editor */
    public List<GameObject> gameObjects = new List<GameObject>();

    /* Dynamically added, determined by device size */
    private List<GameObject> panelBounds = new List<GameObject>();
    private GameObject enemyHolder;

    private float flLastSpawnTime = 0f;
    void Start( ) {

        enemyHolder = GameObject.Find( "EnemyHolder" );
        panelBounds.AddRange( GameObject.FindGameObjectsWithTag("Bounds") );
    }


    void Update( ) {

        List<GameObject> enemies = new( );
        enemies.AddRange( GameObject.FindGameObjectsWithTag( "EnemyPending" ) );
        enemies.AddRange( GameObject.FindGameObjectsWithTag( "Enemy" ) );
        foreach ( var item in enemies ) {

            item.transform.position = Vector2.MoveTowards( item.transform.position, Vector2.zero, 1f * Time.deltaTime );

            Vector2 direction = item.transform.position - transform.position;
            item.transform.up = direction.normalized * -1;
            item.transform.localPosition = new Vector3( item.transform.localPosition.x, item.transform.localPosition.y, 0f );
        }
    }

    private void FixedUpdate( ) {

        if ( Time.time - 0.1f > flLastSpawnTime ) {

            int SpawnLocation = Random.Range( 0, panelBounds.Count );
            Vector3 flModifier = Vector3.zero;
            switch ((BOUNDS)SpawnLocation ) {

                case BOUNDS.BOT or BOUNDS.TOP:
                    flModifier = new Vector2(Random.Range( panelBounds[(int)BOUNDS.RIGHT].transform.position.x, panelBounds[ ( int )BOUNDS.LEFT ].transform.position.x ), 0f);
                    break;

                case BOUNDS.RIGHT or BOUNDS.LEFT:
                    flModifier = new Vector2( 0f,  Random.Range( panelBounds[ ( int )BOUNDS.BOT ].transform.position.y, panelBounds[ ( int )BOUNDS.TOP ].transform.position.y ));
                    break;
            }

            GameObject newObject = Instantiate( gameObjects[ Random.Range( 0, gameObjects.Count ) ] );
            newObject.transform.SetParent( enemyHolder.transform );
            newObject.transform.position = panelBounds[ SpawnLocation ].transform.position + flModifier;
            newObject.tag = "EnemyPending";

            flLastSpawnTime = Time.time;
        }
    }

    enum BOUNDS : int {

        BOT = 0,
        TOP,
        RIGHT,
        LEFT
    }
}
