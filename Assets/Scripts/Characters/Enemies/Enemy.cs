using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{    
    public void Move(Vector2 targetPos, float speed)
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPos, Time.deltaTime * speed);
    }
}
