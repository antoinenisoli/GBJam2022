using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PlayerLevel
{
    public int count;
    public int stepAmount;

    public PlayerLevel(int count, int stepAmount)
    {
        this.count = count;
        this.stepAmount = stepAmount;
    }
}

[System.Serializable]
public class PlayerExperience
{
    public int CurrentLevel = 0;
    [SerializeField] int experience;
    [SerializeField] AnimationCurve levelCurve;
    public int maxXpAmount = 50000;
    public PlayerLevel[] levels = new PlayerLevel[50];

    public int Experience
    {
        get => experience;
        set
        {
            if (value < 0)
                value = 0;
            if (value > maxXpAmount)
                value = maxXpAmount;

            experience = value;
        }
    }

    public PlayerLevel GetNextLevel()
    {
        if (CurrentLevel < levels.Length)
            return levels[CurrentLevel];
        else
            return levels[levels.Length - 1];
    }

    void CheckXP()
    {
        if (CurrentLevel < levels.Length)
        {
            if (Experience >= levels[CurrentLevel].stepAmount)
            {
                LevelUp();
            }
        }
    }

    void LevelUp()
    {
        Experience = 0;
        CurrentLevel++;
        SoundManager.Instance.PlayAudio("levelUp");
        EventManager.Instance.onPlayerNextLevel.Invoke();
    }

    public bool LevelMax()
    {
        return CurrentLevel >= levels.Length - 1;
    }

    public void AddXP(int amount)
    {
        if (LevelMax())
            return;

        Experience += amount;
        CheckXP();
    }

    public void CreateLevels()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            int count = i + 1;
            float diff = (float)count / (float)levels.Length;
            int money = Mathf.RoundToInt(maxXpAmount * levelCurve.Evaluate(diff));
            levels[i] = new PlayerLevel(count, money);
        }
    }
}
