using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float rateOfFire = 0.5f;
    public float nextShot = 0.0f;
    public float bulletForce = 15f;
    public float bulletLifespan = 2f;

    public Sprite redSprite;
    public Sprite greenSprite;
    public Sprite blueSprite;
    private Sprite[] bulletSprites;

    // Start is called before the first frame update
    void Start()
    {
        bulletSprites = new Sprite[] {redSprite, greenSprite, blueSprite};
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextShot) 
        {
            nextShot = Time.time + rateOfFire;
            Shoot(); 
        }
    }

    void Shoot()
    {
        // create new bullet
        GameObject newBullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        // set bullet color to same color as ship
        Bullet newBulletScript = newBullet.GetComponent<Bullet>();
        PrismPlayerControl shipControl = gameObject.GetComponent<PrismPlayerControl>();
        newBulletScript.colorCode = shipControl.colorCode;
        SpriteRenderer bulletSprite = newBullet.GetComponent<SpriteRenderer>();
        bulletSprite.sprite = bulletSprites[newBulletScript.colorCode];
        // send bullet on its way
        Rigidbody2D bulletBody = newBullet.GetComponent<Rigidbody2D>();
        bulletBody.velocity = firePoint.up * bulletForce;
        Destroy(newBullet, bulletLifespan);
    }
}
