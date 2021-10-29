using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WalkingMonster : Entity
{
    [SerializeField] private float distRight = 2f;
    [SerializeField] private float distLeft = 2f;
    [SerializeField] private float speed = 2F;
    float dirX;
    bool movingRight = true;
    private Vector3 dir;
    private SpriteRenderer sprite;
    private Animator anim;
    private AudioSource audioSource;
    public AudioClip audioBoom;



    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        dirX = transform.position.x;
    }
    private void Move()
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
            sprite.flipX = true;
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            sprite.flipX = false;
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }

     void Update()
    {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            audioSource.PlayOneShot(audioBoom);
            anim.SetBool("Boom", true);
            Destroy(gameObject, 0.3f);
            Debug.Log("у червяка");
        }

    }
}
