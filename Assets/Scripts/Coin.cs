using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public string playerTag = "Player";

    public UnityEvent pickupEvent;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            GameManager.Instance.coinNumber++;
            pickupEvent?.Invoke();
            Destroy(gameObject);
        }
    }
}
