using UnityEngine;

public class Ally : MonoBehaviour
{
    [SerializeField] GameObject blood;
    Rigidbody rigidBody;
    Animator animator;
    bool walking;
    bool jogging;
    bool running;
    bool falling;

    void DestroyAlly()
    {
        gameObject.SetActive(false);
        GameController.armyCount--;

        int index = AlliesCommander.allies.IndexOf(gameObject);
        AlliesCommander.alliesOffset.RemoveAt(index);
        AlliesCommander.alliesAnimators.RemoveAt(index);
        AlliesCommander.allies.Remove(gameObject);
        
        Instantiate(blood, gameObject.transform.position + Vector3.up, Quaternion.identity);
        AlliesCommander.OrganizeArmy();
    }
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (SwipeManager.swipeDelta != Vector3.zero)
        {
            transform.rotation = Quaternion.Euler(0, -Vector2.SignedAngle(Vector2.up, SwipeManager.swipeDelta), 0);
        }
        if (transform.position.y < 0)
        {
            DestroyAlly();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (gameObject.activeSelf && other.transform.tag == "Obstacle")
        {
            DestroyAlly();
        }
        else if (gameObject.activeSelf && other.transform.tag == "Enemy" && other.gameObject.activeSelf)
        {
            GameController.armyCount++;
            other.gameObject.SetActive(false);
            GameObject newAlly = Instantiate(gameObject, other.transform.position, Quaternion.identity);
            newAlly.transform.tag = "Ally";
            newAlly.transform.SetParent(AlliesCommander.soldiersObj.transform);

            AlliesCommander.allies.Add(newAlly);
            AlliesCommander.alliesAnimators.Add(newAlly.gameObject.GetComponent<Animator>());
            AlliesCommander.OrganizeArmy();
        }
        else if (gameObject.activeSelf && other.relativeVelocity.y > 10)
        {
            DestroyAlly();
        }
    }
}
