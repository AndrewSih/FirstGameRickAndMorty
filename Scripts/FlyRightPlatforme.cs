using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyRightPlatforme : MonoBehaviour
{
    [SerializeField] private float distRight = 5f;
    [SerializeField] private float distLeft = 5f;
    float dirX;
    float speed = 3f;
    bool movingRight = true;
    void Start()
    {
        dirX = transform.position.x;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= dirX + distRight)
        {
            movingRight = false;
        }
        else if (transform.position.x <= dirX - distLeft)
        {
            movingRight = true;
        }
        if (movingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y );
        }
    }
}