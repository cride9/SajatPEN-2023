using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using STATS = EnemyStats.STATS;
using static Variables;
using TMPro;
using UnityEngine.Audio;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

public class Bullet : MonoBehaviour {

    public Tower tower = null;
    public AudioClip[] audioClip;
    public AudioSource source;
    private TextMeshProUGUI tempCoins = null;
    private TextMeshProUGUI permaCoins = null;


    public GameObject crit;
    public Color32 critHidden = new Color32(1, 1, 1, 0);
    public Color32 critShown = new Color32(255, 1, 1, 255);
    public int critFramesShowTime = 100;


    public void Update()
    {
        critFramesShowTime++;
        if (critFramesShowTime > 15 && GetCritColor().Equals(critShown))
        {
            HideCrit();
        }   
    }

    private void Start( ) {

        crit = GameObject.FindGameObjectWithTag("crit");

        // get tower object
        tower = GameObject.Find( "Tower" ).GetComponent<Tower>();
        tempCoins = GameObject.Find("TempCoin").GetComponent<TextMeshProUGUI>();
        permaCoins = GameObject.Find("PermaCoin").GetComponent <TextMeshProUGUI>();
        source = GetComponent<AudioSource>();
        AudioClip audio = audioClip[Random.Range(0, audioClip.Length)];
        source.PlayOneShot(audio);
    }

    private void OnCollisionEnter2D( Collision2D collision ) {

        // destroy both objects on collision

        if ( collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "EnemyPending" ) {
            
            if (gameObject.name.Equals("Crit"))
            {
                print("crit");
                Vector3 targetPos = gameObject.transform.position;
                crit.transform.position = new Vector3(targetPos.x, targetPos.y, targetPos.z);
                ShowCrit();
                critFramesShowTime = 0;

            }
            // get stats for the damage
            EnemyStats stats = collision.gameObject.GetComponent<EnemyStats>( );

            UpdateHealth( stats, collision.transform.parent.GetChild( 1 ).gameObject );
            if ( stats.GetStat( STATS.HEALTH ) <= 0 ) {
                Destroy( collision.gameObject.transform.parent.gameObject );
                iTempCoin += Mathf.RoundToInt( Random.Range( 1, 3 ) ) * iWaveCount;

                if ( Random.value < 0.6f )
                    iPermaCoin += 1 * iWaveCount;

                UpdateCoins( tempCoins, permaCoins );
            }



            // always destroy current bullet on contact
           
            Destroy(gameObject);
        }
    }
    public Color32 GetCritColor()
    {
        return crit.GetComponent<Image>().color;
    }

    public void SetCritColor(Color32 critColor)
    {
        crit.GetComponent<Image>().color = critColor;
    }

    public void ShowCrit()
    {
        SetCritColor(critShown);
    }
    public void HideCrit()
    {
        SetCritColor(critHidden);
    }

    // this function updates the health (line length)
    private void UpdateHealth( EnemyStats stats, GameObject child ) {

        // stats - enemy stats
        // child - the healthbar gameobject
        var LineRenderer = child.GetComponent<LineRenderer>( );

        // calculate the hp%
        float hpPercent = stats.GetStat( STATS.HEALTH ) / stats.GetOriginalStat( STATS.HEALTH );

        // change the line end position
        LineRenderer.SetPosition( 1, new Vector3( Variables.flLineLength * hpPercent, 0f, 0f ) );
    }
}
