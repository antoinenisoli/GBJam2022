using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerLevel
{
    public int count;
    public int stepAmount;

    public PlayerLevel(int count, int amount)
    {
        this.count = count;
        this.stepAmount = amount;
    }
}

[System.Serializable]
public class PlayerExperience
{
    public int currentLevel = 1;
    public int experience;
    public AnimationCurve levelCurve;
    public int maxXpAmount = 50000;
    public PlayerLevel[] levels = new PlayerLevel[50];

    public int Experience
    {
        get => experience;
        set
        {
            if (value < 0)
                value = 0;
            else if (value > maxXpAmount)
                value = maxXpAmount;

            CheckXP(value);
            experience = value;
        }
    }

    public PlayerLevel GetNextLevel()
    {
        if (currentLevel < levels.Length)
            return levels[currentLevel + 1];
        else
            return levels[levels.Length - 1];
    }

    void CheckXP(int xpAmount)
    {
        if (currentLevel < levels.Length)
        {
            if (xpAmount >= levels[currentLevel + 1].stepAmount)
            {
                currentLevel++;
                EventManager.Instance.onPlayerNextLevel.Invoke();
            }
        }
    }

    public void AddXP(int amount)
    {
        Experience += amount;
    }

    public void CreateLevels()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            int count = i + 1;
            float diff = (float)count / (float)levels.Length;
            Debug.Log(diff);
            int money = Mathf.RoundToInt(maxXpAmount * levelCurve.Evaluate(diff));
            levels[i] = new PlayerLevel(count, money);
        }
    }
}
