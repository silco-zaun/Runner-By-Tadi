using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private GameObject[] tilePrefabs;
    [SerializeField] private float tileSpawnPosition = 0;
    [SerializeField] private float tileLength = 100f;
    [SerializeField] private int numberOfActiveTiles = 4;
    //[SerializeField] private Transform player;

    private List<GameObject> activeTiles = new List<GameObject>();

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < numberOfActiveTiles; i++)
        {
            if (i == 0)
            {
                SpawnTile(0);
            }
            else
            {
                SpawnTile(Random.Range(0, tilePrefabs.Length));
            }
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (PlayerController.Ins.transform.position.z > tileSpawnPosition - ((numberOfActiveTiles - 1) * tileLength))
        {
            SpawnTile(Random.Range(0, tilePrefabs.Length));
            DeleteTile();
        }
    }

    public void SpawnTile(int tileIndex)
    {
        GameObject tilePrefab = Instantiate(tilePrefabs[tileIndex], transform.forward * tileSpawnPosition, transform.rotation);
        tilePrefab.SetActive(true);
        activeTiles.Add(tilePrefab);
        tileSpawnPosition += tileLength;
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }
}
