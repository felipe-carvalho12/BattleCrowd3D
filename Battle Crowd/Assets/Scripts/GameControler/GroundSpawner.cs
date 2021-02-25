using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] int renderingRadius;
    [SerializeField] GameObject lastTile;
    [SerializeField] int tilesInLevel;
    public GameObject[] groundTiles;
    public int allyTileIndex;
    List<GameObject> tiles = new List<GameObject>();
    Vector3 nextSpawnPoint = new Vector3(0, 50, -25);
    public static bool lastTileWasSpawned;

    public void SpawnTile(int index)
    {
        GameObject newTile = Instantiate(groundTiles[index], nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = newTile.transform.GetChild(0).GetChild(0).position;
        tiles.Add(newTile);

    }

    private void Start()
    {
        lastTileWasSpawned = false;
        for (int i = 0; i < 40; i++)
        {
            if (i < 5) SpawnTile(0);
            else SpawnTile(Random.Range(0, groundTiles.Length));
        }
    }
    private void Update()
    {
        if (tiles.Count == tilesInLevel && !lastTileWasSpawned)
        {
            Instantiate(lastTile, nextSpawnPoint, Quaternion.identity);
            lastTileWasSpawned = true;
        }
        if (!lastTileWasSpawned)
        {
            for (int i = 0; i < tiles.Count; i++)
            {
                if (Mathf.Abs(i - allyTileIndex) <= renderingRadius)
                {
                    tiles[i].SetActive(true);
                    continue;
                }
                tiles[i].SetActive(false);
            }
        }
    }
}
