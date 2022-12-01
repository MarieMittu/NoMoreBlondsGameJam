using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject playerCam;
    public float range = 100f;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
            GameObject bulletObject = Instantiate(bulletPrefab);
            bulletObject.transform.position = playerCam.transform.position + playerCam.transform.forward;
            bulletObject.transform.forward = playerCam.transform.forward;
        }   
    }

    void Shoot()
    {
        RaycastHit hit;
        if(Physics.Raycast(playerCam.transform.position, transform.forward, out hit, range))
        {
            SmartManager smartManager = hit.transform.GetComponent<SmartManager>();
            if(smartManager != null)
            {
                Destroy(smartManager.gameObject);
            }
        }
    }
}
