using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Variables;

public class Gmaeover : MonoBehaviour
{
    public void LoadBack() =>
        UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Gameplay");

    public void Exit()
    {
        GameObject.Find("Panel").GetComponent<Spawner>().GameOverMode();
        flSpawnRate = 0.01f;
    }
}
