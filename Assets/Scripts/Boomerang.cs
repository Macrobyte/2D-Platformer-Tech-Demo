using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    Rigidbody2D rigidBody2D;
    Transform returnPoint;
    BoomerangManager boomerangManager;

    [SerializeField] float boomerangSpeed;
    [SerializeField] float boomerangRange;

    bool isReturning = false;
    Vector2 mousePosition;
    
    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        if (rigidBody2D == null)
            throw new System.Exception("Couldn't Find Rigidbody2D!");

        returnPoint = PlayerController.instance.transform;

        boomerangManager = BoomerangManager.instance;



    }
    private void Start()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - rigidBody2D.position;
        direction.Normalize();

        rigidBody2D.velocity = direction * boomerangSpeed;

    }

    private void Update()
    { 
        if (Vector2.Distance(transform.position, returnPoint.position) >= boomerangRange && !isReturning)
        {
            isReturning = true;
        }

        if (isReturning && Vector2.Distance(transform.position, returnPoint.position) <= 0.1f)
        {
            boomerangManager.currentAmmo++;
            Destroy(gameObject);
        }

        
    }

    void FixedUpdate()
    {
        if (isReturning)
        {
            rigidBody2D.velocity = (returnPoint.position - transform.position).normalized * boomerangSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            isReturning = true;
        }
        
    }
}
