using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetGame : MonoBehaviour
{

    private float resetDelay = 4f;
    private bool startLevelResetDelay;

    private void Start()
    {
        SubscribeGameEvents();
    }
    private void SubscribeGameEvents()
    {
        GameEvents.current.onDie += StartLevelResetDelay;
    }
    private void StartLevelResetDelay()
    {
        startLevelResetDelay = true;
    }
    private void Update()
    {
        if (startLevelResetDelay)
        {
            StartCoroutine(LevelResetDelay());  
        }
    }
    IEnumerator LevelResetDelay()
    {
        yield return new WaitForSeconds(resetDelay);
        ReLoadScene();
    }

    private void ReLoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDestroy()
    {
        GameEvents.current.onDie -= StartLevelResetDelay;
    }
}
