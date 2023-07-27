using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{

    internal int playerLevel = 1;
    private int xp = 0;
    private int collectedXP = 1;
    private int xpThreshold = 5;
    private bool canLevelUp = true;

    private void Start()
    {
        SubscribeLevelUpEvents();
    }
    private void SubscribeLevelUpEvents()
    {
        GameEvents.current.onLevelUp += LevelUp;
        GameEvents.current.onGainXP += GainXP;
        Debug.Log("LevelUp events subscribed");
    }
  
    private void LevelUp()
    {
        if (canLevelUp)
        {
            playerLevel++;
            xpThreshold = xpThreshold + (playerLevel * 3);
            Debug.Log("player level-" + playerLevel);
            Debug.Log("xp Threshold-" + xpThreshold);
        }
    }
    private void GainXP()
    {
        xp = xp + collectedXP;
        Debug.Log("xp-" + xp);
        CheckXP();
    }
    private void CheckXP()
    {
        if(xp >= xpThreshold)
        {
            LevelUp();
        }
    }
    private void OnDestroy()
    {
        GameEvents.current.onLevelUp -= LevelUp;
        GameEvents.current.onGainXP -= GainXP;
    }

}
