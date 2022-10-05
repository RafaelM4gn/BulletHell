using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTracker : MonoBehaviour
{
    // Start is called before the first frame update
    Transform target;
    bool follow;
    float bulletSpeed;
    private Vector3 normalizeDirection;
    public void Init(Transform target, bool follow, float bulletSpeed){
        this.target = target;
        this.follow = follow;
        this.bulletSpeed = bulletSpeed;
        normalizeDirection = (target.position - transform.position).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null ){
            if(!follow){
                //target
                transform.position += normalizeDirection * bulletSpeed * Time.deltaTime;
            } else {
                //skillshoot
                transform.position = Vector3.MoveTowards(transform.position, target.transform.position, bulletSpeed * Time.deltaTime);
                if (transform.position == target.transform.position){
                    Destroy(gameObject);
                }
            }
        }
        
    }
}
