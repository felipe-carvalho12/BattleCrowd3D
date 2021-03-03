using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliesCommander : MonoBehaviour
{
    [SerializeField] public static GameObject soldiersObj;
    [SerializeField] GameObject allyPrefab;
    [SerializeField] float speedMultiplier;
    [SerializeField] float maxSpeed;
    static bool velocityWasResetted;

    public static List<GameObject> allies = new List<GameObject>();
    static List<Vector2> alliesOffset = new List<Vector2>();

    void MoveArmy() {
        for (int i = 0; i < allies.Count; i++)
        {
            GameObject ally = allies[i];
            Vector2 offset = alliesOffset[i];
            ally.transform.position = Vector3.Lerp(ally.transform.position, ally.transform.position + new Vector3(offset.x, 0, offset.y), .4f);
        }
    }
    public static void OrganizeArmy()
    {
        float rowsNum = Mathf.Round(Mathf.Sqrt(allies.Count));
        for (int i = 0; i < rowsNum; i++)
        {
            float place = Mathf.Ceil(Mathf.Sqrt(allies.Count));
            for (int j = 1; (place*i + j <= allies.Count && j <= Mathf.Ceil(allies.Count/rowsNum)); j++)
            {
                float xPosOffset = (i + 1 == rowsNum ? allies.Count - place*i : place) - 1;
                Vector3 offset = new Vector2(-(xPosOffset) / 2 + (j - 1), -i);
                alliesOffset[(int)place*i + j - 1] = offset;
            }
        }
        velocityWasResetted = false;
    }

    void ResetArmyVelocity()
    {
        for (int i = 0; i < allies.Count; i++)
        {
            GameObject ally = allies[i];
            ally.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        velocityWasResetted = true;
    }

    private void Start()
    {
        soldiersObj = GameObject.FindGameObjectWithTag("Soldiers");

        GameObject ally = Instantiate(allyPrefab, transform.position, Quaternion.identity);
        ally.transform.SetParent(soldiersObj.transform);
        allies.Add(ally);
    }

    private void FixedUpdate()
    {
        float velX = SwipeManager.swipeDelta.x * speedMultiplier;
        if (velX > maxSpeed) velX = maxSpeed;
        else if (Mathf.Abs(velX) > maxSpeed) velX = -maxSpeed;

        float velZ = SwipeManager.swipeDelta.y * speedMultiplier;
        if (velZ > maxSpeed) velZ = maxSpeed;
        else if (Mathf.Abs(velZ) > maxSpeed) velZ = -maxSpeed;

        Vector3 prevPos = transform.position;
        transform.position += new Vector3(velX, 0, velZ);
        if (SwipeManager.swipeDelta != Vector3.zero)
        {
            MoveArmy();
        }
        else if (!velocityWasResetted)
        {
            ResetArmyVelocity();
        }
    }
}
