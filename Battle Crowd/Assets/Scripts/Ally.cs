using UnityEngine;

public class Ally : MonoBehaviour
{
    Rigidbody rigidBody;
    GameObject allies;
    float speed = 0.03f;

    void DestroyAlly()
    {
        string tag = transform.tag;
        Destroy(gameObject);
        GameController.armyCount--;
        if (tag == "ReferenceAlly" && GameController.armyCount > 0)
        {
            GameObject.FindGameObjectWithTag("Ally").tag = "ReferenceAlly";
        }
    }
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        allies = GameObject.FindGameObjectWithTag("Allies");
    }
    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector3(SwipeManager.swipeDelta.x * speed, rigidBody.velocity.y, SwipeManager.swipeDelta.y * speed);
        Debug.Log(Quaternion.Euler(new Vector3(-90, Vector2.SignedAngle(new Vector2(0, 1), SwipeManager.swipeDelta), Vector2.SignedAngle(new Vector2(0, 1), SwipeManager.swipeDelta))));
        transform.rotation = Quaternion.Euler(new Vector3(-90, Vector2.SignedAngle(new Vector2(0, 1), SwipeManager.swipeDelta), Vector2.SignedAngle(new Vector2(0, 1), SwipeManager.swipeDelta)));
        if (transform.position.y < 30)
        {
            DestroyAlly();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.tag == "Obstacle")
        {
            DestroyAlly();
        }
        if (other.transform.tag == "Enemy")
        {
            GameController.armyCount++;
            Destroy(other.gameObject);
            GameObject newAlly = Instantiate(gameObject, other.transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
            newAlly.transform.tag = "Ally";
            newAlly.transform.SetParent(allies.transform);
        }
    }
}
