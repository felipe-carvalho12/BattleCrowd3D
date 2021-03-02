using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField] int renderingRadius;
    [SerializeField] GameObject lastTile;
    [SerializeField] int tilesInLevel;
    [SerializeField] int[] tilesWeights;
    [SerializeField] GameObject[] tiles;
    public int allyTileIndex;
    List<GameObject> spawnedTiles = new List<GameObject>();
    Vector3 nextSpawnPoint = new Vector3(0, 50, -25);
    public static bool lastTileWasSpawned;

    public void SpawnTile(int index = -1)
    {
        if (index == -1) index = GetRandomWeightedIndex(tilesWeights);
        GameObject newTile = Instantiate(tiles[index], nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = newTile.transform.GetChild(0).GetChild(0).position;
        spawnedTiles.Add(newTile);

    }

    private void Start()
    {
        lastTileWasSpawned = false;
        for (int i = 0; i < 40; i++)
        {
            if (i < 3) SpawnTile(0);
            else SpawnTile();
        }
    }
    private void Update()
    {
        if (spawnedTiles.Count == tilesInLevel && !lastTileWasSpawned)
        {
            Instantiate(lastTile, nextSpawnPoint, Quaternion.identity);
            lastTileWasSpawned = true;
        }
        if (!lastTileWasSpawned)
        {
            for (int i = 0; i < spawnedTiles.Count; i++)
            {
                if (Mathf.Abs(i - allyTileIndex) <= renderingRadius)
                {
                    spawnedTiles[i].SetActive(true);
                    continue;
                }
                spawnedTiles[i].SetActive(false);
            }
        }
    }

    int GetRandomWeightedIndex(int[] weights)
    {
        // Get the total sum of all the weights.
        int weightSum = 0;
        for (int i = 0; i < weights.Length; ++i)
        {
            weightSum += weights[i];
        }

        // Step through all the possibilities, one by one, checking to see if each one is selected.
        int index = 0;
        int lastIndex = weights.Length - 1;
        while (index < lastIndex)
        {
            // Do a probability check with a likelihood of weights[index] / weightSum.
            if (Random.Range(0, weightSum) < weights[index])
            {
                return index;
            }

            // Remove the last item from the sum of total untested weights and try again.
            weightSum -= weights[index++];
        }

        // No other item was selected, so return very last index.
        return index;
    }
}
