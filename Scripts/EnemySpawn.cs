using UnityEngine;
using System.Collections;

public class EnemySpawn : MonoBehaviour
{
    public GameObject BatPrefab;
    public GameObject SkullPrefab;
    public GameObject dinoPrefab;
    public Transform BatSpawn;
    public Transform SkullSpawn;
    public Transform dinoSpawn;
    public static EnemySpawn Instance{get; private set;}
    void Awake()
    {
        if (Instance == null) // If no instance exists, assign this as the instance
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep GameManager across scenes
        }
        else
        {
            Destroy(gameObject); // If an instance already exists, destroy this duplicate
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Spawn()
    {
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    IEnumerator SpawnEnemies()
    {
        while (GameManager.Instance.SpawnEnemies)
        {
            Instantiate(BatPrefab, BatSpawn.position, Quaternion.identity);
            Instantiate(SkullPrefab, SkullSpawn.position, Quaternion.identity);
            Instantiate(dinoPrefab, dinoSpawn.position, Quaternion.identity);
            yield return new WaitForSeconds(6f);
        }
    }
}
