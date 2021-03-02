using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliesCommander : MonoBehaviour
{

    [SerializeField] public static GameObject soldiers;
    [SerializeField] GameObject allyPrefab;
    [SerializeField] float speedMultiplier;
    [SerializeField] float maxSpeed;
    
    List<GameObject> allies = new List<GameObject>();

    void OrganizeArmy() {
        for (int i = 0; i < allies.Count; i++)
        {
            GameObject ally = allies[i];
            ally.transform.position -= transform.position;
        }
    }

    private void Start() {
        soldiers = GameObject.FindGameObjectWithTag("Soldiers");

        GameObject ally = Instantiate(allyPrefab, transform.position, Quaternion.identity);
        ally.transform.SetParent(soldiers.transform);
        allies.Add(ally);
    }

    private void FixedUpdate() {
        float velX = SwipeManager.swipeDelta.x * speedMultiplier;
        if (velX > maxSpeed) velX = maxSpeed;
        else if (Mathf.Abs(velX) > maxSpeed) velX = -maxSpeed;

        float velZ = SwipeManager.swipeDelta.y * speedMultiplier;
        if (velZ > maxSpeed) velZ = maxSpeed;
        else if (Mathf.Abs(velZ) > maxSpeed) velZ = -maxSpeed;

        Vector3 prevPos = transform.position;
        transform.position += new Vector3(velX, 0, velZ);
        if (SwipeManager.swipeDelta != Vector3.zero) OrganizeArmy();
    }
}
