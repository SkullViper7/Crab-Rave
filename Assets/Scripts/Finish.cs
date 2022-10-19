using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Finish : MonoBehaviour
{
    public string playerTag = "Player";
    public UnityEvent endEvent;

    public Sprite lockSprite, unlockSprite;

    public float delayNextLevel = 0.2f;

    [HideInInspector]
    public bool isUnlock;

    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(playerTag) && isUnlock)
        {
            Debug.Log("End");
            endEvent?.Invoke();
            StartCoroutine(NextLevel());
        }
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(delayNextLevel);
        ChangeSceneManager.Instance.ChangeScene(1);
    }

    private void Update()
    {
        if (unlockSprite != null && lockSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = isUnlock ? unlockSprite : lockSprite;
        }
    }
}
