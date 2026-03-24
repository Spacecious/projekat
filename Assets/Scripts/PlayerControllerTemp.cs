using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    InputAction MoveAction;
    Vector3 moveInput;
    [SerializeField]float moveSpeed = 10f;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    [SerializeField] float paddingTop;
    [SerializeField] float paddingBottom;
    Rigidbody2D rb;
    private bool isStunned = false;
   
    void Start()
    {
        MoveAction = InputSystem.actions.FindAction("Move");
        rb = GetComponent<Rigidbody2D>();
        initBounds();
    }

    
    void Update()
    {
        moveInput = MoveAction.ReadValue<Vector2>();
        if (isStunned) 
        {
            rb.linearVelocity = Vector2.zero;
            return; 
        }
    }


    void FixedUpdate()
    {
        if (isStunned)
            return;
        movePlayer();
        constrainPosition();
    }

    public void ApplyStun(float duration)
{
    StartCoroutine(StunRoutine(duration));
}

    IEnumerator StunRoutine(float duration)
    {
        isStunned = true;

        yield return new WaitForSeconds(duration);

        isStunned = false;
    }

    public void movePlayer()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    void constrainPosition()
    {
        Vector3 currentPos = transform.position;
        
        float clampedX = Mathf.Clamp(currentPos.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(currentPos.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        
        transform.position = new Vector3(clampedX, clampedY, currentPos.z);
    }

    void initBounds()
    {
        Camera cam = Camera.main;
        minBounds = cam.ViewportToWorldPoint(new Vector2(0,0));
        maxBounds = cam.ViewportToWorldPoint(new Vector2(1,1));
    }
}
