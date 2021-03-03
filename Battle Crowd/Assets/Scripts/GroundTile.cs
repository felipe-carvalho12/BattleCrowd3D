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
        
    }
}
