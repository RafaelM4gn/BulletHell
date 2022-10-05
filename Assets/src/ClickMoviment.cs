using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class ClickMoviment : MonoBehaviour 
{
    [SerializeField] private NavMeshAgent _agent = null;
    RaycastHit hit;
    float dist;
    private void Start(){
      dist = _agent.remainingDistance; 
   }
   private void Update()
   {
        if (Input.GetMouseButton(1))
        {
            GetComponent<ClickAttack>().findAttack = false;
            GetComponent<ClickAttack>().isCanceling = true;
            GetComponent<ClickAttack>().isAttacking = false;
            GetComponent<ClickAttack>().isMoving = true;
            GetComponent<ClickAttack>().attackMode = false;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
 
            if(Physics.Raycast(ray, out hit))
            {
                if(Vector3.Distance(transform.position, hit.point) > 1)
                {
                    _agent.SetDestination(hit.point);
                }
            } 
        }
      
        if (dist != Mathf.Infinity && _agent.pathStatus == NavMeshPathStatus.PathComplete && _agent.remainingDistance==0){
            GetComponent<ClickAttack>().isCanceling = false;
            GetComponent<ClickAttack>().isMoving = false;
        }
   }
}