using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    private static Player instance;

    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<Player>();
            }
           
            return instance;
        }

    }

    private Animator myAnimator;

    [SerializeField]
    private float movementSpeed;

    private bool facingRight;

    [SerializeField]
    private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

    [SerializeField]
    private LayerMask whatIsGround;


    [SerializeField]
    private bool airControl;

    [SerializeField]
    private float jumpForce;

    public Rigidbody2D MyRigidbody { get; set; }

    public bool Attack { get; set; }

    public bool Slide { get; set; }

    public bool Jump { get; set; }

    public bool OnGround { get; set; }

    // For Shooting
    public Transform gunTip;
    public GameObject bullet;
    float fireRate = 0.5f; // Shoot bullet every half a second
    float nextFire = 0f;

    // Use this for initialization
    void Start ()
    {
        facingRight = true;
        MyRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
	}

    private void Update()
    {
        HandleInput();

        // Player shooting
        if (Input.GetAxisRaw("Fire1") > 0) shootBullet();
    }

    void shootBullet()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            if (facingRight)
            {
                Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 0)));
            }
            else if (!facingRight)
            {
                Instantiate(bullet, gunTip.position, Quaternion.Euler(new Vector3(0, 0, 180f)));
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        float horizontal = Input.GetAxis("Horizontal");

        OnGround = IsGrounded();

        HandleMovement(horizontal);

        Flip(horizontal);

        HandleLayers();

	}

    private void HandleMovement(float horizontal)
    {
        if (MyRigidbody.velocity.y < 0)
        {
            myAnimator.SetBool("land", true);
        }
        if (!Attack && !Slide && (OnGround || airControl))
            {
            MyRigidbody.velocity = new Vector2(horizontal * movementSpeed, MyRigidbody.velocity.y);
        }
        if (Jump && MyRigidbody.velocity.y == 0)
        {
            MyRigidbody.AddForce(new Vector2(0, jumpForce));
        }

        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }


    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAnimator.SetTrigger("jump");
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            myAnimator.SetTrigger("attack");
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            myAnimator.SetTrigger("slide");
        }
    }

    private void Flip(float horizontal)
    {
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

    private bool IsGrounded()
    {
        if (MyRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject)

                    {
                        
                        return true;
                    }
                }
            }
        }
        return false;
    }

    private void HandleLayers()
    {
        if (!OnGround)
        {
            myAnimator.SetLayerWeight(1, 1);
        }
        else
        {
            myAnimator.SetLayerWeight(1, 0);
        }
    }
}
