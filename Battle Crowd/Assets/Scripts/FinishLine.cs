using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnCollisionExit(Collision other) {
        if (other.transform.tag == "ReferenceAlly")
        {
            GameController.levelComplete = true;
        }
    }
}
