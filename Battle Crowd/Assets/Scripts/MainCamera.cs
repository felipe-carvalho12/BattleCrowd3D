using UnityEngine;

public class MainCamera : MonoBehaviour
{
    Transform allies;
    Vector3 offset;

    private void Start() {
        allies = GameObject.FindGameObjectWithTag("AlliesCommander").transform;
        offset = transform.position - allies.position;
    }

    private void FixedUpdate() {
        transform.position = allies.position + offset;
    }
}
