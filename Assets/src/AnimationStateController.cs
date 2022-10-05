using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationStateController : MonoBehaviour
{
    private Animator animator;
    private GameObject player;
    [SerializeField] private NavMeshAgent _agent = null;
    private float playerSpeed;
    public float playerAttackSpeed;
    
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        playerSpeed = _agent.speed;
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (player.GetComponent<ClickAttack>().isCanceling || player.GetComponent<ClickAttack>().isMoving){
            animator.SetBool("isWalking", true);
            animator.SetFloat("animMoveSpeed", playerSpeed/7);
        } else {
            animator.SetBool("isWalking", false);
        }
        if (player.GetComponent<ClickAttack>().isAttacking){
            animator.SetBool("isAttacking", true);
            animator.SetFloat("animAttackSpeed", playerAttackSpeed);
        } else {
            animator.SetBool("isAttacking", false);
        }
    }
    public void SetPlayerAttackSpeed(float pas){
        playerAttackSpeed = pas;
    }

}
