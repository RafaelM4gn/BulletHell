using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ClickAttack : MonoBehaviour 
{
    [SerializeField] private NavMeshAgent _agent = null;
    RaycastHit hit;
    public GameObject enemy = null;
    //Important
    float recoveryLeft = 0f;
    float recovery;
    float windUpLeft = 0f;
    float windUp;
    bool inCoolDown = false;
    GameObject equipped;
    public bool attackMode = false;
    public bool isCanceling = false;
    public bool isMoving = false;
    public bool isAttacking = false;
    public bool findAttack = false;
    public bool targetFound = false;
    [SerializeField] private float windUpPercent = 0.5f;
    [SerializeField] private float attackCD = 4f;
    [SerializeField] private float rotateSpeed = 1;
    [SerializeField] private int attackFindRange = 50;
    [SerializeField] private float attackRange = 5;
    private void Start(){
        windUp = attackCD * windUpPercent;
        recovery = attackCD* (1 - windUpPercent);
    }
    private void Update()
    {   
        //Debug.Log("DEBUG");
        //Debug.Log("InCoolDown " + inCoolDown);
        //Debug.Log("findAttack " + findAttack);
        //Debug.Log("windUpLeft " + windUpLeft);
        //Debug.Log("recorveryLeft " + recoveryLeft);
        //Debug.Log("isCanceling " + isCanceling);
        //Debug.Log("isMoving " + isMoving);
        //Debug.Log("attackMode " + attackMode);
        //Debug.Log("windUp" + windUp);
        //Debug.Log("windUp" + windUp);
        //Debug.Log("recovery " + recovery);
        equipped = GetComponent<Inventory>().inHand;
        if (isAttacking){
            FaceTarget(enemy.transform.position);
        }
        if (inCoolDown){
            recoveryLeft += Time.deltaTime;
            if (recoveryLeft >= recovery){
                inCoolDown = false;
                Debug.Log("Attack Ready");
                targetFound = false;
            }
        }
        if (isCanceling){
            windUpLeft = 0;
        }
        if (Input.GetKeyDown(KeyCode.A) && equipped != null){
            if (equipped.GetComponent<Item>().type == "Weapon"){    
                attackMode = true;
            }
        }
        if(Input.GetMouseButtonDown(0) && attackMode)
        {
            attackMode = false;
            isCanceling = true;
            isAttacking = false;
            isMoving = true;
            findAttack = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out hit))
            {
                enemy = FindClosestEnemy(hit.point);
                if (Vector3.Distance(enemy.transform.position, transform.position) < attackFindRange){
                    if (Vector3.Distance(transform.position, hit.point) > 1)
                    {
                        _agent.SetDestination(enemy.transform.position);
                    }
                }
            }
        }
        if (findAttack){
            if (Vector3.Distance(enemy.transform.position, transform.position) < attackRange){
                isCanceling = false;
                isMoving = false;
                targetFound = true;
                _agent.SetDestination(transform.position);
            }
        }
        if (targetFound && findAttack){
            if (!inCoolDown){
                    if (windUpLeft == 0){
                        Debug.Log("Windup Started");
                    }
                    if (!isCanceling){
                        isAttacking = true;
                        windUpLeft += Time.deltaTime;
                    } else {
                        isAttacking = false;
                    }
                    if (windUpLeft >= windUp){
                        //! PROK ATTACK
                        equipped.GetComponent<StandardGunShoot>().Attack();
                        Debug.Log("S-H-O-O-T-E-D-!");
                        recoveryLeft = 0;
                        windUpLeft = 0;
                        inCoolDown = true;
                    }
                }
        } else if (!targetFound && findAttack){
            isAttacking = false;
            isMoving = true;
            if (Vector3.Distance(enemy.transform.position, transform.position) < attackFindRange){
                if (Vector3.Distance(transform.position, hit.point) > 1)
                {
                    _agent.SetDestination(enemy.transform.position);
                }
            }
        }
    }
    GameObject FindClosestEnemy(Vector3 position) {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach (GameObject go in gos) {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance) {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }
    private void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
    }
    public float GetAttackSpeed(){
        return 1/(windUp + recovery);
    }
    public void SetStatus(float attackRangeN, float attackCDN, float windUpN){
        attackRange = attackRangeN;
        windUp = attackCDN * windUpN;
        recovery = attackCDN* (1 - windUpN);
    }
}
