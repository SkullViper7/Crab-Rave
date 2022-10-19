using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrateArea : MonoBehaviour
{
    private bool isOccupied;
    public string[] cratesTag = new string[] { "Crate" };

    public UnityEvent validateEvent,removeEvent;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isOccupied)
            return;
        foreach (var crate in cratesTag)
        {
            if (collision.CompareTag(crate) && collision.TryGetComponent(out Crate crateComponent))
            {
                if (crateComponent.notMoving && !GameManager.Instance.currentCrate.Contains(crateComponent))
                {
                    isOccupied = true;
                    GameManager.Instance.currentCrate.Add(crateComponent);
                    validateEvent?.Invoke();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!isOccupied)
            return;
        foreach (var crate in cratesTag)
        {
            if (collision.CompareTag(crate) && collision.TryGetComponent(out Crate crateComponent))
            {
                if (GameManager.Instance.currentCrate.Contains(crateComponent))
                {
                    isOccupied = false;
                    GameManager.Instance.currentCrate.Remove(crateComponent);
                    removeEvent?.Invoke();
                }
            }
        }
    }
}
