using System.Collections;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public static FallingPlatform Instance = null;
    [SerializeField]
    GameObject platformPrefab;

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Instantiate(platformPrefab,new Vector2 (-3.5f,-2.5f),platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(0f, -2.5f), platformPrefab.transform.rotation);
        Instantiate(platformPrefab, new Vector2(3.5f, -2.5f), platformPrefab.transform.rotation);

    }

    IEnumerator SpawnPlatform(Vector2 spawnPosition)
    {
        yield return new WaitForSeconds(2f);
        Instantiate(platformPrefab,spawnPosition,platformPrefab.transform.rotation);
    }

}
