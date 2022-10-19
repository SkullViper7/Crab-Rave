using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Teleport : MonoBehaviour
{
    public string playerTag = "Player";
    public Teleport otherTeleport;
    [HideInInspector]
    public bool canTeleport, isOccupied;

    public string[] cratesTag = new string[] { "Crate" };

    private SpriteRenderer spriteRenderer;

    public Sprite unlockSprite, lockSprite;

    public UnityEvent teleportEvent;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        canTeleport = true;
    }

    private void Update()
    {
        spriteRenderer.sprite = !otherTeleport.isOccupied ? unlockSprite : lockSprite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var crate in cratesTag)
        {
            if (collision.CompareTag(crate))
            {
                isOccupied = true;
            }
        }
        if (collision.CompareTag(playerTag) && canTeleport && !otherTeleport.isOccupied)
        {
            canTeleport = false;
            otherTeleport.canTeleport = false;
            collision.transform.position = otherTeleport.transform.position;
            teleportEvent?.Invoke();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (var crate in cratesTag)
        {
            if (collision.CompareTag(crate))
            {
                isOccupied = false;
            }
        }
        if (collision.CompareTag(playerTag))
        {
            canTeleport = true;
            //otherTeleport.canTeleport = true;
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if(otherTeleport == this)
        {
            otherTeleport = null;
            Debug.LogError("Other Teleport needs to be another than this one");
        }
    }
#endif
}
