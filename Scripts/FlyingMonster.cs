using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlyingMonster : Monster
{
    private SpriteRenderer sprite;
    [SerializeField] private AIPath aiPath;


    protected override void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
        //lives = 2;
    }
    protected override void Update()
    {
        sprite.flipX = aiPath.desiredVelocity.x <= 0.01f;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        Bullet bullet = collider.GetComponent<Bullet>();
        if (bullet)
        {
            GetDamage();
        }
    }
}
