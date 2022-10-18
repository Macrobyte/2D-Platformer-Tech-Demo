using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance { get; private set; }
    public Rigidbody2D rigidBody2D { get; private set; }
    public BoxCollider2D boxCollider2D { get; private set; }
    public Animator animator { get; private set; }
    
    public float HorizontalInput { get; private set; }
    public float VerticalInput { get; private set; }
    public bool JumpInput { get; private set; }
    public bool FlyInput { get; private set; }
    public bool attackInput { get; private set; }
    

    public float currentGravityScale { get; private set; }

    [SerializeField][Range(1f,5f)] float defaultGravityScale;

    protected float lastHorizontalInput;

    void Awake()
    {
        instance = this;
        rigidBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        
        if (rigidBody2D == null)
            Debug.LogError("Rigidbody2D is null");
        if (boxCollider2D == null)
            Debug.LogError("BoxCollider2D is null");
        if (animator == null)
            Debug.LogError("Animator is null");
    }
    
    private void Start()
    {
        rigidBody2D.gravityScale = currentGravityScale = defaultGravityScale;
    }
    
    void Update()
    {
        HandleInput();
        FlipPlayer();
    }

    void HandleInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");

        VerticalInput = Input.GetAxisRaw("Vertical");

        JumpInput = Input.GetButton("Jump");

        attackInput = Input.GetButtonDown("Fire1");

        FlyInput = Input.GetButton("Fire3");
    }

    void FlipPlayer()
    {
        if (HorizontalInput != 0) lastHorizontalInput = HorizontalInput;
        transform.localScale = new Vector3(lastHorizontalInput > 0 ? 1 : -1, 1, 1);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
            this.transform.parent = collision.gameObject.transform;

    }
    
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
            this.transform.parent = null;
    }

}
