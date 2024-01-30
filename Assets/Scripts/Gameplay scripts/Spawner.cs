using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;
using static Variables;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class Spawner : MonoBehaviour {

    // Added inside editor
    public List<GameObject> gameObjects = new List<GameObject>();
    public GameObject WaveTextObject;

    // Dynamically added, determined by device size
    private List<GameObject> panelBounds = new List<GameObject>();
    private GameObject enemyHolder;
    private GameObject upgradeHolder;
    private TextMeshProUGUI waveText;

    private float flLastSpawnTime = 0f; // used in delaying spawning
    private int DesiredEnemyCount = iEnemyCount;
    private float flTimeoutTime = -1f;

    private GameObject tempCoins;
    private GameObject permaCoins;

    void Start( ) {

        // get bounds & holder parent
        enemyHolder = GameObject.Find("EnemyHolder");
        //panelBounds.AddRange( GameObject.FindGameObjectsWithTag("Bounds") );
        panelBounds.AddRange( new List<GameObject>( ) { GameObject.Find( "BotBound" ), GameObject.Find( "TopBound" ), GameObject.Find( "RightBound" ), GameObject.Find( "LeftBound" ) } );
        waveText = WaveTextObject.GetComponent<TextMeshProUGUI>();
        tempCoins = GameObject.Find( "TempCoin" );
        permaCoins = GameObject.Find( "PermaCoin" );
        UpdateCoins(
            tempCoins.GetComponent<TextMeshProUGUI>( ),
            permaCoins.GetComponent<TextMeshProUGUI>( )
            );
        upgradeHolder = GameObject.Find( "Upgrades" );

        SceneManager.LoadScene("GameMenu", LoadSceneMode.Additive);
        SceneManager.LoadScene("Options", LoadSceneMode.Additive);

    }


    void Update( ) {

        if ( bPause ) {
            ResetGameplay( );
            return;
        }
        upgradeHolder.SetActive( true );
        tempCoins.SetActive( true );
        permaCoins.SetActive( true );

        // make a list out of every pending and current enemies
        List<GameObject> enemies = new( );
        enemies.AddRange( GameObject.FindGameObjectsWithTag( "EnemyPending" ) );
        enemies.AddRange( GameObject.FindGameObjectsWithTag( "Enemy" ) );

        // this foreach will align them to look in the middle and also updates health
        // SIDENOTE: aligning them to look at the middle point needs to be done every update to make it smooth
        foreach ( var item in enemies ) {

            // rotation to look to the middle
            Vector2 direction = item.transform.position - transform.position;
            item.transform.up = direction.normalized * -1;

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

        if ( bPause )
            return;

        // if wave is completed prepare the next one
        if ( DesiredEnemyCount < 1 ) {

            // wait for every enemy to be deleted
            if ( enemyHolder.transform.childCount != 0 )
                return;

            // set timeout time
            flTimeoutTime = Time.time;
            iWaveCount++;
            DesiredEnemyCount = Mathf.RoundToInt( iEnemyCount * ( flSpawnMultiplier * iWaveCount ) );
        }

        // timeout for the wave text
        if ( Time.time - 2f < flTimeoutTime ) {

            waveText.gameObject.SetActive( true );
            waveText.text = $"Wave {iWaveCount}";
            return;
        }
        waveText.text = $""; // clear text
        waveText.gameObject.SetActive( false );

        if ( Time.time - flSpawnRate - (flSpawnrateMultiplier * iWaveCount) > flLastSpawnTime ) {

            int groupSpawn = Random.Range( 1, 3 );

            for ( int i = 0; i < groupSpawn; i++ ) {

                // generate a random side to start from
                int SpawnLocation = Random.Range( 0, panelBounds.Count );

                // generate a random position on that side
                Vector3 flModifier = Vector3.zero;
                switch ( ( BOUNDS )SpawnLocation ) {

                    case BOUNDS.BOT or BOUNDS.TOP:
                        flModifier = new Vector3( Random.Range( panelBounds[ ( int )BOUNDS.RIGHT ].transform.position.x, panelBounds[ ( int )BOUNDS.LEFT ].transform.position.x ), 0f );
                        break;

                    case BOUNDS.RIGHT or BOUNDS.LEFT:
                        flModifier = new Vector3( 0f, Random.Range( panelBounds[ ( int )BOUNDS.BOT ].transform.position.y, panelBounds[ ( int )BOUNDS.TOP ].transform.position.y ) );
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
                stats.SetMultiplier( EnemyStats.STATS.DAMAGE, flDamageMultiplier * iWaveCount  );
                stats.SetMultiplier( EnemyStats.STATS.HEALTH, flHealthMultiplier * iWaveCount );
                stats.SetMultiplier( EnemyStats.STATS.SPEED, flSpeedMultiplier * iWaveCount );
                DesiredEnemyCount--;
            }
            
            flLastSpawnTime = Time.time;
        }
    }

    void ResetGameplay() {

        upgradeHolder.SetActive( false );
        tempCoins.SetActive( false );
        permaCoins.SetActive( false );
        GameObject.FindGameObjectsWithTag( "EnemyPending" ).ToList( ).ForEach( x => Destroy( x.transform.parent ) );
        GameObject.FindGameObjectsWithTag( "Enemy" ).ToList( ).ForEach( x => Destroy( x.transform.parent ) );

        iWaveCount = 1;
        DesiredEnemyCount = iEnemyCount;
        flTimeoutTime = -1f;
        flLastSpawnTime = 0f;
        iTempCoin = 0;
    }

    // easier handling
    enum BOUNDS : int {

        BOT = 0,
        TOP,
        RIGHT,
        LEFT
    }
}
