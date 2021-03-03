using UnityEngine;

public class Ally : MonoBehaviour
{
    [SerializeField] GameObject blood;
    [SerializeField] float walkingMinSpeed;
    [SerializeField] float walkingMaxSpeed;
    [SerializeField] float joggingMaxSpeed;
    Rigidbody rigidBody;
    Animator animator;
    bool walking;
    bool jogging;
    bool running;
    bool falling;
    Vector2 XZvelocity;

    void DestroyAlly()
    {
        gameObject.SetActive(false);
        GameController.armyCount--;

        AlliesCommander.alliesOffset.RemoveAt(AlliesCommander.allies.IndexOf(gameObject));
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
        /*// Walking
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

        if (Mathf.Abs(rigidBody.velocity.y) < 6f)
        {
            if (falling)
            {
                animator.SetBool("falling", false);
                falling = false;
            }
        }
        else if (!falling)
        {
            animator.SetBool("falling", true);
            falling = true;
        }*/

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
            AlliesCommander.OrganizeArmy();
        }
        else if (gameObject.activeSelf && other.relativeVelocity.y > 10)
        {
            DestroyAlly();
        }
    }
}
