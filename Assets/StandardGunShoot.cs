using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardGunShoot : MonoBehaviour
{
    //!WEAPON STATUS
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCD;
    [SerializeField] private float windUp;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float damage;
    private GameObject player;
    private GameObject sphere;
    private GameObject target;
    public GameObject prefab;
    bool follow;
    // Start is called before the first frame update
    public void Attack()
    {
        target = player.GetComponent<ClickAttack>().enemy;
        sphere =  Instantiate(prefab, player.transform.position, new Quaternion(0,0,0,0));
        //TODO SPAWN BULLET
        sphere.GetComponent<BulletTracker>().Init(target.transform, follow, 50f);
    }
    void Start()
    {
        player = GameObject.Find("Player");
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            Attack();
        }
        if(Input.GetKeyDown(KeyCode.E)){
            follow = !follow;
        }
    }
    public float GetAttackRange(){
        return attackRange;
    }
    public float GetAttackCD(){
        return attackCD;
    }
    public float GetWindUp(){
        return windUp;
    }
}
