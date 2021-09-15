using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform x in transform) {
            x.gameObject.SetActive(false);
        }
        transform.GetChild(3).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
