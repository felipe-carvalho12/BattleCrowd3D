using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
    bool spawnedNewTile;


    private void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "ReferenceAlly")
        {
            if (!spawnedNewTile && !GroundSpawner.lastTileWasSpawned) groundSpawner.SpawnTile(Random.Range(0, groundSpawner.groundTiles.Length));
            if (transform.position.z > other.transform.position.z)
            {
                groundSpawner.allyTileIndex--;
            }
            else
            {
                groundSpawner.allyTileIndex++;
            }
            spawnedNewTile = true;
        }
    }
}
