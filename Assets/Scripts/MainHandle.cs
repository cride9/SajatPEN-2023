using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainHandle : MonoBehaviour {

    public GameObject upgradeMenu;
    public GameObject exitButton;

    void Start( ) {

        Application.targetFrameRate = 144;

        Variables.LoadVariables( );
        Variables.UpdatePermaCoins( GameObject.Find( "PermCoinMenu" ).GetComponent<TextMeshProUGUI>( ) );
        upgradeMenu = GameObject.Find( "UpgradeMenu" );
        exitButton = GameObject.Find( "Giveup" );
        upgradeMenu.SetActive( true );
        exitButton.SetActive( false );
    }

    public void StartGame( ) {

        Variables.UpdateTempValues( );
        Variables.bPause = false;
        SceneManager.UnloadSceneAsync( "GameMenu", UnloadSceneOptions.UnloadAllEmbeddedSceneObjects );
        exitButton.SetActive( true );
    }
}
