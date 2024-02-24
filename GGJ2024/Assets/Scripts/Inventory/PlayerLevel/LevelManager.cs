using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Slider slider;
    [SerializeField] private double playerXP;
    [SerializeField] private double playerLevel;
    private double currentXPneeded;

    private void Start()
    {
        currentXPneeded = Math.Pow(2, playerLevel);
    }
    public IEnumerator XPGained(float xp)
    {
        for (int i = 1; i < 101; i++)
        {
            playerXP += xp/100;

            if (currentXPneeded <= playerXP)
            {
                playerXP -= currentXPneeded;
                playerLevel += 1;
                currentXPneeded = Math.Pow(2, playerLevel);
            }
            slider.value = (float)playerXP / (float)currentXPneeded;
            yield return new WaitForSeconds(0.01f);
        }
    }

    private void PlayerLevel()
    {
        
    }
}
