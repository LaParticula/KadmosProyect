using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CumGun : MonoBehaviour
{
    [SerializeField] private GameObject BulletObject;
    [SerializeField] private Transform BulletSpawnPoint;
    [SerializeField] private float ShootingRate = 0.2f;
    [SerializeField] private int magazineSize = 40;
    [SerializeField] private int bulletsStorageSize = 160;
    private int currentMagazine;
    private int bulletsStored;
    private Animator GunAnimator;
    private bool canShoot = true;

    void Start() {
        GunAnimator = GetComponent<Animator>();
        currentMagazine = magazineSize;
        bulletsStored = bulletsStorageSize;
    }

    void Update() {
        if (Input.GetMouseButton(0) && canShoot && currentMagazine > 0) {
            canShoot = false;
            currentMagazine -= 1;

            GameObject bullet = Instantiate(
                BulletObject, 
                BulletSpawnPoint.position, 
                BulletSpawnPoint.rotation
            );
            bullet.transform.localScale = new Vector3(20, 20, 20);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.AddForce(BulletSpawnPoint.forward * 30f, ForceMode.Impulse);

            GunAnimator.SetTrigger("Shooting");

            StartCoroutine(Reactivate());

        } else if (Input.GetKeyDown(KeyCode.R) && canShoot && currentMagazine < magazineSize && bulletsStored > 0) {
            GunAnimator.SetTrigger("Reloading");
            canShoot = false;
        }
        //Debug.Log(currentMagazine);
        //Debug.Log(bulletsStored);
    }

    IEnumerator Reactivate() {
        yield return new WaitForSeconds(ShootingRate);
        canShoot = true;
    }

    void ReloadCompleted() {
        int bulletsToReload = magazineSize - currentMagazine;
        if (bulletsStored - bulletsToReload >= 0) {
            currentMagazine = magazineSize;
            bulletsStored -= bulletsToReload;
        } else {
            currentMagazine += bulletsStored;
            bulletsStored = 0;
        }
        canShoot = true;
    }
 }
