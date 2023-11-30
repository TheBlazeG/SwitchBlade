using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingSpike : MonoBehaviour
{
    private Rigidbody2D fallingSpikeRigidBody;
    void Start()
    {
        fallingSpikeRigidBody = GetComponent<Rigidbody2D>();
        Physics2D.IgnoreLayerCollision(3, 6, true);
    }
}
