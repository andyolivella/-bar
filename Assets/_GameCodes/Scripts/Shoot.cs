using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletOrigin;
    public float bulletForce = 5;
    // Start is called before the first frame update
    public void ShootTo( GameObject objetive)
    {
        GameObject gobj = Instantiate<GameObject>(bulletPrefab);
        gobj.transform.position = bulletOrigin.position;
        Rigidbody rb = gobj.GetComponent<Rigidbody>();
        rb.AddForce((objetive.transform.position - gobj.transform.position).normalized * bulletForce, ForceMode.Impulse);
    }


}
