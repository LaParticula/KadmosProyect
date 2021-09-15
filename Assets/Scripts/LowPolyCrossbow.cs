using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowPolyCrossbow : MonoBehaviour
{
    [SerializeField] private GameObject BulletObject;
    [SerializeField] private GameObject BulletObjectInGun;
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private float ShootingRate = 0.8f;
    [Space]
    [SerializeField] private LineRenderer RightString;
    [SerializeField] private LineRenderer LeftString;
    [SerializeField] private Transform RightLimbPos1;
    [SerializeField] private Transform LeftLimbPos1;
    [SerializeField] private Transform Limbs;
    private Animator CrossbowAnimator;

    private bool canShoot = true;

    // Start is called before the first frame update
    void Start() {
        CrossbowAnimator = GetComponent<Animator>();
        BulletObject.transform.localScale = transform.localScale;
        RightString.positionCount = 2;
        LeftString.positionCount = 2;
        DrawStrings();
    }

    // Update is called once per frame
    void Update() {
        DrawStrings();

        if (Input.GetMouseButtonDown(0) && canShoot) {
            canShoot = false;
            GameObject bullet = Instantiate(
                BulletObject, 
                BulletObjectInGun.transform.position, 
                BulletObjectInGun.transform.rotation
            );
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.AddForce(-transform.right * 60f, ForceMode.Impulse);
            Destroy(bullet, 10);
            CrossbowAnimator.SetTrigger("Shoot");
            StartCoroutine(Reactivate());
        }
    }

    IEnumerator Reactivate() {
        yield return new WaitForSeconds(ShootingRate);
        canShoot = true;
    }

    void DrawStrings() {
        RightString.SetPosition(1, RightString.transform.InverseTransformPoint(RightLimbPos1.position));
        LeftString.SetPosition(1, LeftString.transform.InverseTransformPoint(LeftLimbPos1.position));
    }
}
