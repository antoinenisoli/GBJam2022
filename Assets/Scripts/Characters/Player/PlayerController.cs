using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    public static PlayerController Instance;
    [SerializeField] float speed = 10f;

    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector2 inputs = new Vector2(horizontal, vertical);
        Vector2 movement = inputs.normalized * Time.deltaTime * speed;
        transform.position += (Vector3)movement;
    }
}
