using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Crate : MonoBehaviour
{
    public float moveSpeed = 1;
    public LayerMask stopMovement;
    public bool dontStop;
    [Range(0,1)]
    public float delay = 0.1f;

    public UnityEvent moveEvent, stopEvent;

    [HideInInspector]
    public bool notMoving;

    public bool Move(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) < 0.5f)
        {
            direction.x = 0;
        }
        else
        {
            direction.y = 0;
        }
        direction.Normalize();
        if (Blocked(transform.position, direction))
        {
            notMoving = true;
            stopEvent?.Invoke();
            return false;
        }
        else
        {
            notMoving = false;
            StartCoroutine(TranslateCrate(direction));
            return true;
        }
    }

    IEnumerator TranslateCrate(Vector2 direction)
    {
        moveEvent?.Invoke();
        transform.Translate(direction * moveSpeed);
        if(dontStop)
        {
            yield return new WaitForSeconds(delay);
            Move(direction);
        }
        else
        {
            notMoving = true;
        }
    }

    bool Blocked(Vector3 position, Vector2 direction)
    {
        return Physics2D.OverlapCircle(position + (Vector3)direction * moveSpeed, .2f, stopMovement);
    }
}