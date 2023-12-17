using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour {

    // Added inside editor
    public List<GameObject> gameObjects = new List<GameObject>();

    // Dynamically added, determined by device size
    private List<GameObject> panelBounds = new List<GameObject>();
    private GameObject enemyHolder;

    private float flLastSpawnTime = 0f; // used in delaying spawning
    const float flLineLength = 0.3f; // used for the healthbar size

    public float flSpawnRate = 0.75f; // spawn interval (time between spawning an object)
    void Start( ) {

        // get bounds & holder parent
        enemyHolder = GameObject.Find( "EnemyHolder" );
        //panelBounds.AddRange( GameObject.FindGameObjectsWithTag("Bounds") );
        panelBounds.AddRange( new List<GameObject>( ) { GameObject.Find( "BotBound" ), GameObject.Find( "TopBound" ), GameObject.Find( "RightBound" ), GameObject.Find( "LeftBound" ) } );
    }


    void Update( ) {

        // make a list out of every pending and current enemies
        List<GameObject> enemies = new( );
        enemies.AddRange( GameObject.FindGameObjectsWithTag( "EnemyPending" ) );
        enemies.AddRange( GameObject.FindGameObjectsWithTag( "Enemy" ) );

        // this foreach will align them to look in the middle and also updates health
        // SIDENOTE: aligning them to look at the middle point needs to be done every update to make it smooth
        foreach ( var item in enemies ) {

            var statistics = item.GetComponent<EnemyStats>( );

            // rotation to look to the middle
            Vector2 direction = item.transform.position - transform.position;
            item.transform.up = direction.normalized * -1;

            UpdateHealth( statistics, item.transform.parent.gameObject.transform.GetChild( 1 ).gameObject );

            item.transform.localPosition = new Vector3( item.transform.localPosition.x, item.transform.localPosition.y, 0f );
        }

        // get the enemy holders ( the parent of the enemy sprite & healthbar )
        var holders = GameObject.FindGameObjectsWithTag( "EnemyHolder" );
        foreach ( var item in holders ) {

            // get speed stat
            var stats = item.transform.GetChild(0).GetComponent<EnemyStats>();

            // always make the enemy sprite in the middle to align with the healthbar
            item.transform.GetChild( 0 ).localPosition = Vector3.zero;

            // move the whole holder closer to the middle
            item.transform.position = Vector2.MoveTowards( item.transform.position, Vector2.zero, stats.GetStat( EnemyStats.STATS.SPEED ) * Time.deltaTime );
        }
    }

    private void FixedUpdate( ) {

        if ( Time.time - flSpawnRate > flLastSpawnTime ) {

            // generate a random side to start from
            int SpawnLocation = Random.Range( 0, panelBounds.Count );

            // generate a random position on that side
            Vector3 flModifier = Vector3.zero;
            switch ((BOUNDS)SpawnLocation ) {

                case BOUNDS.BOT or BOUNDS.TOP:
                    flModifier = new Vector3(Random.Range( panelBounds[(int)BOUNDS.RIGHT].transform.position.x, panelBounds[ ( int )BOUNDS.LEFT ].transform.position.x ), 0f);
                    break;

                case BOUNDS.RIGHT or BOUNDS.LEFT:
                    flModifier = new Vector3( 0f, Random.Range( panelBounds[ ( int )BOUNDS.BOT ].transform.position.y, panelBounds[ ( int )BOUNDS.TOP ].transform.position.y ));
                    break;
            }

            // generate a new enemy
            GameObject newObject = Instantiate( gameObjects[ Random.Range( 0, gameObjects.Count ) ] );

            // move to the direction
            newObject.transform.position = panelBounds[ SpawnLocation ].transform.position + flModifier;

            // place it inside the enemy holder panel
            newObject.transform.SetParent( enemyHolder.transform );

            // change tag to enemy holder for later usage
            newObject.tag = "EnemyHolder";

            // first child is the sprite aka the square or triangle
            var spriteChild = newObject.transform.GetChild( 0 );

            // pending enemy = not inside the radius
            spriteChild.tag = "EnemyPending";

            // give it some stats
            var stats = spriteChild.AddComponent<EnemyStats>( );
            stats.SetMultiplier( 1f );

            flLastSpawnTime = Time.time;
        }
    }

    // this function updates the health (line length)
    private void UpdateHealth( EnemyStats stats, GameObject child ) {

        // stats - enemy stats
        // child - the healthbar gameobject
        var LineRenderer = child.GetComponent<LineRenderer>( );

        // calculate the hp%
        float hpPercent = stats.GetStat( EnemyStats.STATS.HEALTH ) / stats.GetOriginalStat( EnemyStats.STATS.HEALTH );

        // change the line end position
        LineRenderer.SetPosition( 1, new Vector3( flLineLength * hpPercent, 0f, 0f ) );
    }

    // easier handling
    enum BOUNDS : int {

        BOT = 0,
        TOP,
        RIGHT,
        LEFT
    }
}
