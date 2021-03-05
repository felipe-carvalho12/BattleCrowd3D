using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlliesAnimator : MonoBehaviour
{
    static float walkingMinSpeed = .5f;
    static float walkingMaxSpeed = 3f;
    static float joggingMaxSpeed = 5f;
    public static void AnimateAlly(Vector3 vel, Animator animator)
    {
        bool walking = animator.GetBool("walking");
        bool jogging = animator.GetBool("jogging");
        bool running = animator.GetBool("running");
        bool falling = animator.GetBool("falling");

        // Idle
        if (vel.magnitude < walkingMinSpeed && (walking || running || jogging))
        {
            animator.SetBool("walking", false);
            animator.SetBool("jogging", false);
            animator.SetBool("running", false);
        }
        // Walking
        else if ((vel.magnitude > 0 && vel.magnitude <= walkingMaxSpeed) && !walking)
        {
            animator.SetBool("walking", true);
            animator.SetBool("jogging", false);
            animator.SetBool("running", false);
        }
        // Jogging
        else if ((vel.magnitude > walkingMaxSpeed && vel.magnitude <= joggingMaxSpeed) && !jogging)
        {
            animator.SetBool("walking", false);
            animator.SetBool("jogging", true);
            animator.SetBool("running", false);
        }
        // Running
        else if ((vel.magnitude > joggingMaxSpeed) && !running)
        {
            animator.SetBool("walking", false);
            animator.SetBool("jogging", false);
            animator.SetBool("running", true);
        }

        if (Mathf.Abs(vel.y) < 6f)
        {
            if (falling)
            {
                animator.SetBool("falling", false);
            }
        }
        else if (!falling)
        {
            animator.SetBool("falling", true);
        }
    }
}
