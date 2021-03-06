﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] List<Transform> guns;

    float timeSinceLastShoot = Mathf.Infinity;
    void Start()
    {
        SetGuns();
    }

    void Update()
    {
        timeSinceLastShoot += Time.deltaTime;
    }

    private void SetGuns()
    {
        foreach (Transform child in transform)
        {
            if (child.name == "Gun")
            {
                guns.Add(child);
            }
        }
    }

    public void Shoot(){
        if(timeSinceLastShoot >= bullet.GetComponent<BulletController>().coolDown){
            foreach (Transform gun in guns)
            {
                GameObject bulletInst = Instantiate(bullet, gun.position, gun.rotation) as GameObject;
                bulletInst.GetComponent<BulletController>().SetFriend(gameObject);
                bulletInst.GetComponent<Rigidbody2D>().velocity = bulletInst.transform.up * bullet.GetComponent<BulletController>().velocity;
                Destroy(bulletInst, bulletInst.GetComponent<BulletController>().lifeTime);
            }
            timeSinceLastShoot = 0;
        }
    }

    public void SetBullet(GameObject bullet){
        this.bullet = bullet;
    }


}
