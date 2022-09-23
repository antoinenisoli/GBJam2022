using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum GameState
{
    Active,
    InWeaponMenu,
    InWeaponSelection,
    Paused,
}

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
    [SerializeField] GameState state;
    [SerializeField] List<Enemy> enemies = new List<Enemy>();
    public PlayerExperience PlayerEXP;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        EventManager.Instance.onPlayerNextLevel.AddListener(()=> { SetState(GameState.InWeaponSelection); });
        EventManager.Instance.onNewWeapon.AddListener(() => { SetState(GameState.Active); });
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

    void SetState(GameState newState)
    {
        state = newState;
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
            case GameState.InWeaponMenu:
                if (Input.GetButtonDown("SelectButton")) //quit weapon menu
                {
                    SetState(GameState.Active);
                    EventManager.Instance.onQuitSelect.Invoke();
                }

                break;
            case GameState.Paused:
                if (Input.GetButtonDown("StartButton")) //unpause game
                    SetState(GameState.Active);

                break;
            case GameState.Active:
                if (Input.GetButtonDown("SelectButton")) // pause the game and open the weapon menu
                {
                    SetState(GameState.InWeaponMenu);
                    EventManager.Instance.onSelectButton.Invoke();
                }

                if (Input.GetButtonDown("StartButton")) //pause game
                    SetState(GameState.Paused);

                break;
        }

        Time.timeScale = state == GameState.Active ? 1 : 0;
    }

    private void Update()
    {
        ManageState();
    }
}
