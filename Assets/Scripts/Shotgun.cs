using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private float spread = 10;
    [SerializeField] private int pelletCount = 10;
    [SerializeField] private GameObject BulletObject;
    [SerializeField] private Transform BulletSpawnPoint;
    [SerializeField] private float bulletForce = 100;
    [SerializeField] private int maxBulletsStored = 40;
    [Space]
    [Header("Materials")]
    [SerializeField] private Renderer MagazineIndicator1;
    [SerializeField] private Renderer MagazineIndicator2;
    [SerializeField] private Material LoadedBulletMaterial;
    [SerializeField] private Material DeloadedBulletMaterial;

    private Animator ShotgunAnimator;
    private const int MagazineSize = 2;
    private int currentMagazine;
    private int currentBulletsStored;
    private bool canShoot = true;

    void Start() {
        ShotgunAnimator = GetComponent<Animator>();
        currentBulletsStored = maxBulletsStored;
        currentMagazine = MagazineSize;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0) && canShoot && currentMagazine > 0) {
            currentMagazine -= 1;
            for (int i = 0; i < pelletCount; i++) {
                GameObject bullet = Instantiate(BulletObject, BulletSpawnPoint.position, transform.rotation);
                bullet.transform.rotation = Quaternion.RotateTowards(bullet.transform.rotation, Random.rotation, spread);
                Rigidbody bulletRB = bullet.GetComponent<Rigidbody>();
                bulletRB.AddForce(bullet.transform.forward * bulletForce, ForceMode.Impulse);
            }
            ShotgunAnimator.SetTrigger("Shoot");
            updateMagazineIndicators();

        } else if (Input.GetKeyDown(KeyCode.R) && canShoot && currentMagazine < MagazineSize && currentBulletsStored > 0) {
            canShoot = false;
            ShotgunAnimator.SetTrigger("Reloading");
        }
    }

    void reload() {
        if (currentBulletsStored > 1) {
            currentMagazine = MagazineSize;
            currentBulletsStored -= MagazineSize;
        } else {
            currentMagazine += 1;
            currentBulletsStored -= 1;
        }
        updateMagazineIndicators();
    }

    void updateMagazineIndicators() {
        switch (currentMagazine) {
            case 1:
                MagazineIndicator1.material = DeloadedBulletMaterial;
                MagazineIndicator2.material = LoadedBulletMaterial;
                break;
            case 2:
                MagazineIndicator1.material = LoadedBulletMaterial;
                MagazineIndicator2.material = LoadedBulletMaterial;
                break;
            default:
                MagazineIndicator1.material = DeloadedBulletMaterial;
                MagazineIndicator2.material = DeloadedBulletMaterial;
                break;
        }
    }

    // Animation event
    void ReloadEnded() {
        reload();
        canShoot = true;
    }
}
