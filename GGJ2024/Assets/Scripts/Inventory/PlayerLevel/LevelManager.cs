using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private double playerXP;
    [SerializeField] private double playerLevel;
    private double currentXPneeded;

    private void Start()
    {
        currentXPneeded = Math.Pow(2, playerLevel);
    }
    public void XPGained(int xp)
    {
        playerXP += xp;
        PlayerLevel();
    }

    private void PlayerLevel()
    {
        if (currentXPneeded <= playerXP) 
        {
            playerXP -= currentXPneeded;
            playerLevel += 1;
            currentXPneeded = Math.Pow(2, playerLevel);
        }
        print(playerXP / currentXPneeded);
    }
}
