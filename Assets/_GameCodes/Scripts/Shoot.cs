using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform bulletOrigin;
    public float bulletForce = 5;
    public ObjectPool bulletsPool;
    // Start is called before the first frame update
    public void ShootTo( GameObject objetive)
    {
        GameObject gobj = bulletsPool.Get();
        gobj.transform.position = bulletOrigin.position;
        Rigidbody rb = gobj.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce((objetive.transform.position - gobj.transform.position).normalized * bulletForce, ForceMode.Impulse);
    }


}
