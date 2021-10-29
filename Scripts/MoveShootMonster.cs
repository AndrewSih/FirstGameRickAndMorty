using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class MoveShootMonster : Entity
{
    [SerializeField] private float distRight = 2f;
    [SerializeField] private float distLeft = 2f;
    [SerializeField] private float rate = 2.0f;
    private BulletEnemy bulletEnemy;
    [SerializeField] private float speed = 2F;
    float dirX;
    bool movingRight = true;
    private Animator anim;
    private SpriteRenderer sprite;
    private AudioSource audioSource;
    public AudioClip audioBoom;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        bulletEnemy = Resources.Load<BulletEnemy>("BulletEnemy");
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

     void Start()
    {
        anim = GetComponentInChildren<Animator>();
        InvokeRepeating("Shoot", rate, rate);
        // dir = transform.right;
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
            sprite.flipX = false;
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            sprite.flipX = true;
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }

    void Update()
    {
        Move();
    }
    private void Shoot()
    {
        Vector3 position = transform.position;
        position.y += 0.3f;
        BulletEnemy newBullet = Instantiate(bulletEnemy, position, bulletEnemy.transform.rotation);
        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
        // newBullet.Direction = -newBullet.transform.right;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
        }
       
    }
}
