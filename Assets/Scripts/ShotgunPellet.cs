using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunPellet : MonoBehaviour {
    private Rigidbody pelletRB;
    private BoxCollider pelletCollider;
    private bool hasCollisoned = false;

    void Start() {
        pelletRB = GetComponent<Rigidbody>();
        pelletCollider = GetComponent<BoxCollider>();
        StartCoroutine(WillBeDestroyed());
    }

    private void OnCollisionEnter() {
        Destroy(pelletRB);
        Destroy(pelletCollider);
        hasCollisoned = true;
        Destroy(gameObject, 10);
    }

    IEnumerator WillBeDestroyed() {
        yield return new WaitForSeconds(100f);
        if (!hasCollisoned) {
            Destroy(gameObject);
        }
    }
}
