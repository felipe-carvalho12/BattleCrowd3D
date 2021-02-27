using UnityEngine;

public class Ally : MonoBehaviour
{
    Rigidbody rigidBody;
    Animator animator;
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
        animator = GetComponent<Animator>();
        allies = GameObject.FindGameObjectWithTag("Allies");
    }
    private void FixedUpdate()
    {
        rigidBody.velocity = new Vector3(SwipeManager.swipeDelta.x * speed, rigidBody.velocity.y, SwipeManager.swipeDelta.y * speed);
        if (SwipeManager.swipeDelta.magnitude != 0)
        {

            transform.rotation = Quaternion.Euler(0, -Vector2.SignedAngle(new Vector2(0, 1), SwipeManager.swipeDelta), 0);
        }

        if (transform.position.y < 30)
        {
            DestroyAlly();
        }

        if (rigidBody.velocity.magnitude > 0 && !animator.GetBool("running"))
        {
            animator.SetBool("running", true);
        }
        else
        {
            animator.SetBool("running", false);
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
            GameObject newAlly = Instantiate(gameObject, other.transform.position, Quaternion.identity);
            newAlly.transform.tag = "Ally";
            newAlly.transform.SetParent(allies.transform);
        }
    }
}
