using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FollowPlayer : MonoBehaviour 
{
    [SerializeField] private NavMeshAgent _agent = null;
    private GameObject player, bullet;
    [SerializeField] private GameObject prefab;
    Vector3 targetToF;
    public float attackRange = 5;


    public float fireRate = 1;
    float fireRateLeft = 0;
    public float bulletSpeed = 20;
    public bool follow = false;
    RaycastHit hit;
    float dist;
    private void Start(){
      dist = _agent.remainingDistance; 
      player = GameObject.Find("Player");
   }
   private void Update()
   {
        targetToF = new Vector3(player.transform.position.x, 0, player.transform.position.z);
        _agent.SetDestination(player.transform.position);
        if (Vector3.Distance(transform.position, targetToF) < attackRange){
            _agent.SetDestination(transform.position);
        }
        fireRateLeft += Time.deltaTime;
        if (fireRateLeft>= fireRate){
            Attack();
            fireRateLeft = 0;
        }
   }

   public void Attack()
    {
        bullet =  Instantiate(prefab, transform.position, new Quaternion(0,0,0,0));
        bullet.GetComponent<BulletTracker>().Init(player.transform, follow, bulletSpeed);
    }
}
