using UnityEngine;

public class Ally : MonoBehaviour
{
    Rigidbody rigidBody;
    Animator animator;
    GameObject allies;
    float speedMultiplier = .05f;
    float maxSpeed = 10;
    bool running;

    void DestroyAlly()
    {
        gameObject.SetActive(false);
        GameController.armyCount--;
        if (transform.tag == "ReferenceAlly" && GameController.armyCount > 0)
        {
            GameObject.FindGameObjectWithTag("Ally").tag = "ReferenceAlly";
            transform.tag = "Ally";
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
        float velX = SwipeManager.swipeDelta.x * speedMultiplier;
        if (velX > maxSpeed) velX = maxSpeed;
        else if (Mathf.Abs(velX) > maxSpeed) velX = -maxSpeed;

        float velZ = SwipeManager.swipeDelta.y * speedMultiplier;
        if (velZ > maxSpeed) velZ = maxSpeed;
        else if (Mathf.Abs(velZ) > maxSpeed) velZ = -maxSpeed;
        
        rigidBody.velocity = new Vector3(velX, rigidBody.velocity.y, velZ);

        if (SwipeManager.swipeDelta.magnitude != 0)
        {

            transform.rotation = Quaternion.Euler(0, -Vector2.SignedAngle(new Vector2(0, 1), SwipeManager.swipeDelta), 0);
        }

        if (transform.position.y < 30)
        {
            DestroyAlly();
        }

        if ((rigidBody.velocity.x != 0 || rigidBody.velocity.z != 0) && !running)
        {
            animator.SetBool("running", true);
            running = true;
        }
        else if ((rigidBody.velocity.x == 0 || rigidBody.velocity.z == 0) && running)
        {
            animator.SetBool("running", false);
            running = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (gameObject.activeSelf && other.transform.tag == "Obstacle")
        {
            DestroyAlly();
        }
        else if (gameObject.activeSelf && other.transform.tag == "Enemy")
        {
            GameController.armyCount++;
            other.gameObject.SetActive(false);
            GameObject newAlly = Instantiate(gameObject, other.transform.position, Quaternion.identity);
            newAlly.transform.tag = "Ally";
            newAlly.transform.SetParent(allies.transform);
        }
    }
}
