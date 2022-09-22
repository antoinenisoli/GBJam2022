using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameState
{
    Active,
    InMenu,
    Paused,
}

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
    [SerializeField] GameState state;
    public PlayerExperience PlayerEXP;
    [SerializeField] GameObject weaponMenu;
    [SerializeField] List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        weaponMenu.SetActive(false);
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
    }

    public Enemy ClosestEnemy()
    {
        float maxDist = Mathf.Infinity;
        Enemy closestEnemy = null;

        foreach (var item in enemies)
        {
            float distance = Vector2.Distance(item.transform.position, PlayerController.Instance.transform.position);
            if (distance < maxDist)
            {
                maxDist = distance;
                closestEnemy = item;
            }
        }

        return closestEnemy;
    }

    [ContextMenu(nameof(CreateLevels))]
    public void CreateLevels()
    {
        PlayerEXP.CreateLevels();
    }

    void ManageState()
    {
        switch (state)
        {
            case GameState.InMenu:
                if (Input.GetButtonDown("SelectButton"))
                {
                    state = GameState.Active;
                    weaponMenu.SetActive(false);
                }

                break;
            case GameState.Paused:
                if (Input.GetButtonDown("StartButton"))
                    state = GameState.Active;

                break;
            case GameState.Active:
                if (Input.GetButtonDown("SelectButton"))
                {
                    state = GameState.InMenu;
                    weaponMenu.SetActive(true);
                }

                if (Input.GetButtonDown("StartButton"))
                    state = GameState.Paused;

                break;
        }

        Time.timeScale = state == GameState.Active ? 1 : 0;
    }

    private void Update()
    {
        ManageState();
    }
}
