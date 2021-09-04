using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BastonWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletObject;
    [SerializeField] private Transform PlayerCamera;
    private bool canShot = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate() {
        bulletObject.transform.Rotate(2f, 5f, 3f);
    }
    private void Update() {
            if (Input.GetMouseButtonDown(0) && canShot == true) {
                canShot = false;
                bulletObject.SetActive(false);
                //Create A bullet Copy
                GameObject bullet = Instantiate<GameObject>(bulletObject);
                bullet.SetActive(true);    
                bullet.transform.localScale = new Vector3(7, 7, 7);
                bullet.transform.position = bulletObject.transform.position;
                bullet.AddComponent<SphereCollider>();
                Rigidbody bulletRrigidbody = bullet.AddComponent<Rigidbody>();
            bulletRrigidbody.AddForce(PlayerCamera.forward * 1500f);
            bulletRrigidbody.AddForce(new Vector3(0,1,0) * 500f);
            Destroy(bullet, 10);
                StartCoroutine(Reactivate());
            }

    }

    IEnumerator Reactivate() {
        yield return new WaitForSeconds(0.5f);
        bulletObject.SetActive(true);
        canShot = true;
    }
    // Update is called once per frame

}
