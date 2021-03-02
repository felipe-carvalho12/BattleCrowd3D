using UnityEngine;

public class GroundTile : MonoBehaviour
{
    GroundSpawner groundSpawner;
    bool spawnedNewTile;
    bool referenceAllyCrossedTile;
    float timeToComputeNextCross;


    private void Start()
    {
        groundSpawner = GameObject.FindObjectOfType<GroundSpawner>();
    }
    private void Update() {
        if (timeToComputeNextCross <= 0 && referenceAllyCrossedTile)
        {
            referenceAllyCrossedTile = false;
            timeToComputeNextCross = 0;
        }
        else if (referenceAllyCrossedTile)
        {
            timeToComputeNextCross -= Time.deltaTime;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!referenceAllyCrossedTile)
        {
            Transform referenceAllyTransform = GameObject.FindGameObjectWithTag("ReferenceAlly").transform;
            if (other.transform.IsChildOf(referenceAllyTransform))
            {
                if (!spawnedNewTile && !GroundSpawner.lastTileWasSpawned) groundSpawner.SpawnTile();
                if (transform.position.z > referenceAllyTransform.position.z)
                {
                    groundSpawner.allyTileIndex--;
                }
                else
                {
                    groundSpawner.allyTileIndex++;
                }
                spawnedNewTile = true;
                referenceAllyCrossedTile = true;
                timeToComputeNextCross = 3f;
            }
        }
    }
}
