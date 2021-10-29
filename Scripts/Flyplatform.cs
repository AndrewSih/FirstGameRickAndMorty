using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyplatform : MonoBehaviour
{
    [SerializeField] private float distUp = 4f;
    [SerializeField] private float distDown = 4f;
    float dirX;
    float speed = 3f;
    bool movingUp = true;
    void Start()
    {
        dirX = transform.position.y;
    }
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y >= dirX + distUp)
        {
            movingUp = false;
        }
        else if (transform.position.y <= dirX -distDown)
        {
            movingUp = true;
        }
        if  (movingUp)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }
    }
}
