using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : Entity

{
    private Animator anim;
    [SerializeField] private int lives = 1;
    private AudioSource audioSource;
    public AudioClip audioBoom;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponentInChildren<Animator>();
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
            Debug.Log("у червяка" + lives);
        }
        
    }
}
