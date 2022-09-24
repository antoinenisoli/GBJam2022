using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float followSpeed = 10f;
    PlayerController player => PlayerController.Instance;

    public void Follow(Vector2 targetPosition)
    {
        Vector2 newPosition = targetPosition;
        transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime * followSpeed);
    }

    private void LateUpdate()
    {
        if (player)
            Follow(player.transform.position);
        else if (target)
            Follow(target.position);
    }
}
