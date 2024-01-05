using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainHandle : MonoBehaviour {

    public GameObject upgradeMenu;
    void Start( ) {

        Application.targetFrameRate = 144;

        Variables.LoadVariables( );
        Variables.UpdatePermaCoins( GameObject.Find( "PermCoinMenu" ).GetComponent<TextMeshProUGUI>( ) );
        upgradeMenu = GameObject.Find( "UpgradeMenu" );
        upgradeMenu.SetActive( false );
    }

    public void StartGame( ) {

        Variables.UpdateTempValues( );
        Variables.bPause = false;
        upgradeMenu.SetActive( true );
        SceneManager.UnloadSceneAsync( "GameMenu", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects );
    }
}
