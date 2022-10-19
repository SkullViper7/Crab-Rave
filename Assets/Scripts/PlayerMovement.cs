using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 1f;

    public LayerMask stopMovement;

    public float timeMovement = 1f;

    public UnityEvent moveEvent, blockEvent;

    bool canMove;
    Vector2 moveInput;
    float t;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        moveInput.Normalize();
        if(moveInput.sqrMagnitude>0.5f)
        {
            if(canMove)
            {
                t = 0;
                canMove = false;
                Move(moveInput);
            }
        }
        else
        {
            canMove = true;
        }

        t += Time.deltaTime;
        if(t>=timeMovement)
        {
            canMove = true;
        }
    }

    public void Move(Vector2 direction)
    {
        if(Mathf.Abs(direction.x) < 0.5f)
        {
            direction.x = 0;
        }
        else
        {
            direction.y = 0;
        }
        direction.Normalize();
        if(!Blocked(transform.position, direction))
        {
            transform.Translate(direction * moveSpeed);
            moveEvent?.Invoke();
        }
        else
        {
            blockEvent?.Invoke();
        }
    }

    bool Blocked(Vector3 position, Vector2 direction)
    {
        bool cantMove = Physics2D.OverlapCircle(position + (Vector3)direction * moveSpeed, .2f, stopMovement);
        var obstacles = Physics2D.OverlapCircleAll(position + (Vector3)direction * moveSpeed, .2f);
        foreach (var obstacle in obstacles)
        {
            if(obstacle.TryGetComponent(out Crate crate))
            {
                cantMove = !crate.Move(direction);
            }
        }

        return cantMove;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)moveInput * moveSpeed, .2f);
    }
#endif
}
