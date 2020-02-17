using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Rigidbody projectile;
    public Transform spawnpoint;
    public MouseLook mouseSensitivity;
    public float firepowerRate = 10f;
    public float firepowerMax = 195f;
    float firepower;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Update is called once per frame
    void Update()
    { 
        if (Input.GetButton("Fire1"))
        {
            if (firepower < firepowerMax)
            {
                firepower += firepowerRate;
            }
        }
        else
        {
            if (firepower > 40)
            {
                float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity.mouseSensitivity * Time.deltaTime;
                //mouseY = Mathf.Clamp(mouseY, 0)

                Rigidbody clone;
                clone = (Rigidbody)Instantiate(projectile, spawnpoint.position, projectile.rotation);
                //clone.useGravity = false;
                clone.velocity = spawnpoint.TransformDirection(Vector3.forward * firepower);
                
                //clone.velocity = spawnpoint.TransformDirection(Vector3.up * mouseY * firepower);
            }
            firepower = 0;
        }
    }
}

