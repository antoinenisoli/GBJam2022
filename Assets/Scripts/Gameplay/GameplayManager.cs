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
    public PlayerExperience PlayerEXP;
    [SerializeField] GameObject weaponMenu;
    GameObject weaponSelection;
    [SerializeField] List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        weaponSelection = FindObjectOfType<WeaponSelectionManager>().gameObject;
        weaponMenu.SetActive(false);
    }

    private void Start()
    {
        EventManager.Instance.onPlayerNextLevel.AddListener(OpenWeaponSelection);
        EventManager.Instance.onNewWeapon.AddListener(CloseWeaponSelection);
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

    public void OpenWeaponSelection()
    {
        state = GameState.InWeaponSelection;
        weaponSelection.SetActive(true);
    }

    public void CloseWeaponSelection()
    {
        state = GameState.Active;
        weaponSelection.SetActive(false);
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
                    state = GameState.InWeaponMenu;
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
