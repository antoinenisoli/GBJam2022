using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance;
    List<Enemy> enemies = new List<Enemy>();

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
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
}
