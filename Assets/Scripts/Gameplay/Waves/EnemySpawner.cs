using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int spawnCount = 3;
    [SerializeField] float positionRandomOffset = 3f;
    [SerializeField] Transform spawnParent;
    Vector2 startOffset;

    private void Start()
    {
        startOffset = transform.position - PlayerController.Instance.transform.position;
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Gizmos.DrawWireSphere(transform.position, positionRandomOffset);

        Color c = Color.red;
        c.a = 0.2f;
        Gizmos.color = c;
        Gizmos.DrawSphere(transform.position, positionRandomOffset);
#endif
    }

    public Vector2 RandomPosition()
    {
        Vector2 circle = Random.insideUnitCircle;
        Vector2 random = new Vector2(circle.x, circle.y) * positionRandomOffset;
        return random;
    }

    public void Spawn(GameObject enemyPrefab)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector2 newPos = (Vector2)transform.position + RandomPosition();
            Instantiate(enemyPrefab, newPos, Quaternion.identity, spawnParent);
        }
    }

    private void Update()
    {
        transform.position = PlayerController.Instance.transform.position - (Vector3)startOffset;
    }
}
