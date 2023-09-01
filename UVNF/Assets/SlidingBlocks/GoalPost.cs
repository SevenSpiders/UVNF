using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalPost : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.LogWarning(collision.gameObject.name);
    }
}
