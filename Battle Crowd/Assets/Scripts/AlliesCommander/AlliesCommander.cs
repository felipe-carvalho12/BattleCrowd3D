using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliesCommander : MonoBehaviour
{
    [SerializeField] public static GameObject soldiersObj;
    [SerializeField] GameObject allyPrefab;
    [SerializeField] float speedMultiplier;
    [SerializeField] float maxSpeed;
    public static List<GameObject> allies = new List<GameObject>();
    public static List<Vector2> alliesOffset = new List<Vector2>();
    public static List<Animator> alliesAnimators = new List<Animator>();
    static bool velocityWasResetted;
    static float rowsNum;

    void MoveArmy()
    {
        for (int i = 0; i < allies.Count; i++)
        {
            GameObject ally = allies[i];
            Vector2 offset = alliesOffset[i];

            ally.transform.position = transform.position + new Vector3(offset.x, 0, offset.y);
        }
    }
    public static void OrganizeArmy()
    {
        rowsNum = Mathf.Round(Mathf.Sqrt(allies.Count));
        for (int i = 0; i < rowsNum; i++)
        {
            float place = Mathf.Ceil(Mathf.Sqrt(allies.Count));
            for (int j = 1; (place * i + j <= allies.Count && j <= Mathf.Ceil(allies.Count / rowsNum)); j++)
            {
                float xPosOffset = (i + 1 == rowsNum ? allies.Count - place * i : place) - 1;
                int index = (int)place * i + j - 1;
                Vector2 offset = new Vector2(-(xPosOffset) / 2 + (j - 1), -i);
                if (alliesOffset.Count > index)
                {
                    alliesOffset[index] = offset;
                }
                else
                {
                    alliesOffset.Add(offset);
                }
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

        OrganizeArmy();
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
