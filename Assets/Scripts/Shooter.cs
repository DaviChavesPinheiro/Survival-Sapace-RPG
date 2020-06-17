using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    List<Transform> guns;

    void Start()
    {
        foreach (Transform child in transform)
        {
            if(child.name == "Gun"){
                guns.Add(child);
            }
        }
    }

    public void Shoot(){
        foreach (Transform gun in guns)
        {
            GameObject bulletInst = Instantiate(bullet, gun.position, gun.rotation) as GameObject;
            bulletInst.GetComponent<Rigidbody2D>().velocity = bulletInst.transform.up * bullet.GetComponent<Bullet>().velocity;
            Destroy(bulletInst, bulletInst.GetComponent<Bullet>().life_time);
        }
    }

    public void SetBullet(GameObject bullet){
        this.bullet = bullet;
    }


}
