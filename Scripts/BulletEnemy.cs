using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    private GameObject parent;
    public GameObject Parent { set { parent = value; } }
    private float speed = 10.0f;
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }
    private void Start()
    {

        Destroy(gameObject, 0.7F);

    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
    /* private void OnTriggerEnter2D(Collider2D collider)
     {
         Entity entity = collider.GetComponent<Entity>();
         if (entity && entity.gameObject != parent)
         {
            entity.GetDamage();
            Destroy(gameObject);
         }
     }*/
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Entity entity = collider.GetComponent<Entity>();
        if (entity && entity.gameObject != parent)
        {
            Destroy(gameObject);
        }
    }
}