using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Transform player;
    Vector3 offset;

    private void Start() {
        player = GameObject.FindGameObjectWithTag("ReferenceAlly").transform;
        offset = transform.position - player.position;
    }

    private void FixedUpdate() {
        if (player == null && GameController.armyCount > 0)
        {
            player = GameObject.FindGameObjectWithTag("ReferenceAlly").transform;
        }
        Vector3 targetPos = player.position + offset;
        transform.position = targetPos;
    }
}
