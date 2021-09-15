using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CumGunBullet : MonoBehaviour
{
    [SerializeField] private Material Material1;
    [SerializeField] private Material Material2;
    private Renderer BulletRenderer;
    private Rigidbody BulletRigidbody;
    private int maxScale = 150;
    private bool collided = false;
    private bool canBeDestroyed = true;
    private bool canMaterialLerp = false;
    private float t = 0f;

    void Start() {
        BulletRigidbody = GetComponent<Rigidbody>();
        BulletRenderer = GetComponent<Renderer>();
        StartCoroutine(WillBeDestroyed()); 
    }

    void FixedUpdate() {
        if (transform.localScale.x < maxScale && collided) {
            transform.localScale += new Vector3(15, 15, 15);
        }
        if (canMaterialLerp && t < 0.7f) {
            t += 0.02f;
            BulletRenderer.material.Lerp(Material1, Material2, t);
        } else if (canMaterialLerp && t < 1) {
            t += 0.01f;
            BulletRenderer.material.Lerp(Material1, Material2, t);
        }
    }

    void OnCollisionEnter(Collision col) {
        if (col.gameObject.tag != gameObject.tag && !collided) {
            transform.rotation = Random.rotation;
            collided = true;
            canBeDestroyed = false;
            canMaterialLerp = true;
            Destroy(BulletRigidbody);
            Destroy(gameObject, 120);
        }
    }

    IEnumerator WillBeDestroyed() {
        yield return new WaitForSeconds(2);
        if (canBeDestroyed) {
            Destroy(gameObject);
        }
    }
}
