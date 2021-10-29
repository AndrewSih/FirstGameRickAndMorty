
using UnityEngine;

using Photon.Pun;


public class Hero : Entity, IPunObservable
{
    [SerializeField] private float speed;
    [SerializeField] private int lives = 5;
    private PhotonView photonview;
    private LivesBar livesBar;
    [SerializeField] private float jumpForce = 15f;
    private bool isGrounded = false;
    public Bullet bullet;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    public float normalSpeed = 4f;
    public AudioClip audioClip;
    public AudioClip audioJump;
    public AudioClip audioDamage;
    private AudioSource audioSource;
    public GameObject canvas;

    public static Hero Instance { get; set; }
    void Start()
    {
        Time.timeScale = 1;
        audioSource = GetComponent<AudioSource>();
        speed = 0f;
        photonview = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
        AddObservable();
        if (!photonview.IsMine)
        {
            canvas.SetActive(false);
        }
    }
    public int Lives
    {

        get
        {
            return lives;
        }
        set
        {
            if (value < 5) lives = value;
            livesBar.Refresh();
        }
    }
    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }
    private void Awake()
    {

        livesBar = FindObjectOfType<LivesBar>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        Instance = this;
        bullet = Resources.Load<Bullet>("Bullet");
    }

    private void FixedUpdate()
    {
        if (photonview.IsMine)
        {

            if (!isGrounded) State = States.jump;
            if (isGrounded) State = States.idle;
            if (speed != 0f) State = States.run;
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }

        CheckGround();
    }
    private void Update()
    {

        if (Lives == 0)
        {
            Camera.main.GetComponent<DeadCanvas>().PauseOn();
        }
        if (transform.position.y <= -2.1f)
        {
            Camera.main.GetComponent<DeadCanvas>().PauseOn();
        }
    }
    public void PressPause()
    {
        Camera.main.GetComponent<DeadCanvas>().Pause();
    }
    public void OnLeftButtonDown()
    {
        if (speed >= 0f)
        {
            speed = -normalSpeed;
            sprite.flipX = true;
        }
    }
    public void OnRightButtonDown()
    {
        if (speed <= 0f)
        {
            speed = normalSpeed;
            sprite.flipX = false;
        }
    }
    public void OnButtonUp()
    {
        speed = 0f;
    }

    public void Jump()
    {

        audioSource.PlayOneShot(audioJump);
        if (isGrounded) rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

        //if (isGrounded) rb.velocity = Vector2.up * jumpForce;

    }

    public void Shoot()
    {
        audioSource.PlayOneShot(audioClip);
        Vector3 position = transform.position;
        position.y += 0.8f;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
        newBullet.Parent = gameObject;
        if (sprite.flipX == false)
        {
            newBullet.Direction = newBullet.transform.right * (1.0F);
        }
        else
        {
            newBullet.Direction = newBullet.transform.right * (-1.0F);
        }
    }
    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
    }

    public override void GetDamage()
    {
        if (photonview.IsMine) { 
        Lives--;
        audioSource.PlayOneShot(audioDamage);
        rb.velocity = Vector3.zero;
        rb.AddForce(transform.up * 5.0F, ForceMode2D.Impulse);
        Debug.Log(lives);}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Flyplatform"))
        {
            this.transform.parent = collision.transform;
        }
        if (collision.gameObject.name.Equals("FlyRightLeftPl"))
        {
            this.transform.parent = collision.transform;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Flyplatform"))
        {
            this.transform.parent = null;
        }
        if (collision.gameObject.name.Equals("FlyRightLeftPl"))
        {
            this.transform.parent = null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "BulletEnemy")
        {
            // anim.SetBool("Boom", true);
            GetDamage();
            Debug.Log("у червяка");
        }

    }
    private void AddObservable() //метод последовательность событий во времени, использовал для синхронизации flipX
    {
        if (!photonview.ObservedComponents.Contains(this))
        {
            photonview.ObservedComponents.Add(this);
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(sprite.flipX);
        }
        else
        {
            sprite.flipX = (bool)stream.ReceiveNext();
        }
    }
}
public enum States
{
    idle,
    run,
    jump
}

