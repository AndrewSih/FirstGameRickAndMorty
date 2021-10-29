using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMonster : Entity
{
   
    [SerializeField]
    private int lives = 1;
    [SerializeField] private float rate = 2.0f;
    private BulletEnemy bulletEnemy;
    private Animator anim;
    private AudioSource audioSource;
    public AudioClip audioBoom;

    void Awake()
    {
        bulletEnemy = Resources.Load<BulletEnemy>("BulletEnemy");
        
    }
     void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
        InvokeRepeating("Shoot", rate, rate);
    }
    private void Shoot()
    {
        Vector3 position = transform.position;
        position.y += 0.5f;
        BulletEnemy newBullet = Instantiate(bulletEnemy, position, bulletEnemy.transform.rotation);
        newBullet.Parent = gameObject;
        newBullet.Direction = -newBullet.transform.right;
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            anim.SetBool("Boom", true);
            audioSource.PlayOneShot(audioBoom);
            Destroy(gameObject, 0.3f);
            Debug.Log("у червяка" + lives);
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
