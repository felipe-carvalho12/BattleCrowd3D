using UnityEngine;

public class Ally : MonoBehaviour
{
    [SerializeField] float walkingMinSpeed;
    [SerializeField] float walkingMaxSpeed;
    [SerializeField] float joggingMaxSpeed;
    [SerializeField] float speedMultiplier;
    [SerializeField] float maxSpeed;
    Rigidbody rigidBody;
    Animator animator;
    GameObject allies;
    bool walking;
    bool jogging;
    bool running;
    bool falling;
    Vector2 XZvelocity;

    bool isGrounded() {
        return Physics.Raycast(transform.position, -Vector3.up, 5f);
    }

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

        XZvelocity.x = rigidBody.velocity.x;
        XZvelocity.y = rigidBody.velocity.z;

        // Idle
        if (XZvelocity.magnitude < walkingMinSpeed && (walking || running || jogging))
        {
            animator.SetBool("walking", false);
            animator.SetBool("jogging", false);
            animator.SetBool("running", false);
            walking = false;
            jogging = false;
            running = false;
        }
        // Walking
        else if ((XZvelocity.magnitude > 0 && XZvelocity.magnitude <= walkingMaxSpeed) && !walking)
        {
            animator.SetBool("walking", true);
            animator.SetBool("jogging", false);
            animator.SetBool("running", false);
            walking = true;
            jogging = false;
            running = false;
        }
        // Jogging
        else if ((XZvelocity.magnitude > walkingMaxSpeed && XZvelocity.magnitude <= joggingMaxSpeed) && !jogging)
        {
            animator.SetBool("walking", false);
            animator.SetBool("jogging", true);
            animator.SetBool("running", false);
            walking = false;
            jogging = true;
            running = false;
        }
        // Running
        else if ((XZvelocity.magnitude > joggingMaxSpeed) && !running)
        {
            animator.SetBool("walking", false);
            animator.SetBool("jogging", false);
            animator.SetBool("running", true);
            walking = false;
            jogging = false;
            running = true;
        }

        Debug.Log(isGrounded());
        if (isGrounded() || rigidBody.velocity.y > -3f)
        {
            if (falling)
            {
                animator.SetBool("falling", false);
                falling = false;
            }
        }
        else if (!falling)
        {
            if (rigidBody.velocity.y < -3f)
            {
                animator.SetBool("falling", true);
                falling = true;
            }
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
            newAlly.transform.SetParent(allies.transform);
        }
        else if (gameObject.activeSelf && other.relativeVelocity.y > 10)
        {
            DestroyAlly();
        }
    }
}
