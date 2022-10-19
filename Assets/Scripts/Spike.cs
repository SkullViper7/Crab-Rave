using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spike : MonoBehaviour
{
    public string[] cratesTag = new string[] { "Crate" };
    public UnityEvent destroyEvent;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var crate in cratesTag)
        {
            if (collision.CompareTag(crate))
            {
                destroyEvent?.Invoke();
                Destroy(collision.gameObject);
            }
        }
    }
}
