using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null;
    public static GameManager Instance => instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
    }

    public CrateArea[] cratesArea;
    public Coin[] coins;
    public Finish finish;

    [HideInInspector]
    public List<Crate> currentCrate = new List<Crate>();

    [HideInInspector]
    public int coinNumber;

    // Start is called before the first frame update
    void Start()
    {
        cratesArea = FindObjectsOfType<CrateArea>();
        coins = FindObjectsOfType<Coin>();
    }

    // Update is called once per frame
    void Update()
    {
        finish.isUnlock = cratesArea.Length == currentCrate.Count && coinNumber == coins.Length;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        cratesArea = FindObjectsOfType<CrateArea>();
        coins = FindObjectsOfType<Coin>();
    }
#endif
}
