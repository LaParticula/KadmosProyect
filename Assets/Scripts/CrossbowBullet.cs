// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class CrossbowBullet : MonoBehaviour
{
    private Rigidbody bulletRigidbody;
    private BoxCollider bulletCollider;

    void Start() {
        bulletRigidbody = GetComponent<Rigidbody>();
        bulletCollider = GetComponent<BoxCollider>();
    }

    void OnCollisionEnter(Collision collision) {
        Destroy(bulletRigidbody);
        Destroy(bulletCollider);
    }
}
