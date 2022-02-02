using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : MonoBehaviour
{
    public Rigidbody2D rb;

    public Animator animator;

    [Header("Patrol")]
    public Transform patrolPoint1;
    public Transform patrolPoint2;
    public Transform patrolPoint3;
    public Transform patrolPoint4;
    public Transform patrolPoint5;
    public Transform patrolPoint6;

    public Transform returnPoint;

    [Header("Target Info")]
    public LayerMask victimLayer;
    public Transform target;
    public Transform enemyPosition;

    [Header("Range And Speed")]
    public float agroRange;
    public float followRange;
    public float minRange;
    public float moveSpeed;

    [Header("Attack Info")]
    public Transform attackPoint;
    public LayerMask characterLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 20;
    public bool damageTarget = false;
    public float atkCoolDown;

    bool targetAquired = false;
    bool patroling = false;
    float agroRangeBase = 0;

    bool pPoint1Reached = false;
    bool pPoint2Reached = false;
    bool pPoint3Reached = false;
    bool pPoint4Reached = false;
    bool pPoint5Reached = false;
    bool pPoint6Reached = false;

    float coolDown;

    float moveX;
    float moveY;




    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<player_movement>().transform;
        agroRangeBase = agroRange;
    }

    // Update is called once per frame
    void Update()
    {
        /****Patrol and follow****/
        if (targetAquired)
        {
            agroRange = followRange;
        }
        if (!targetAquired)
        {
            agroRange = agroRangeBase;
        }

        if (Vector3.Distance (enemyPosition.position, returnPoint.position) == 0)
        {
            patroling = true;
        }

        if (Vector3.Distance(target.position, enemyPosition.position) <= agroRange && Vector3.Distance(target.position, enemyPosition.position) >= minRange)
        {
            followPlayer();
            targetAquired = true;
            patroling = false;
        }
        else if (Vector3.Distance(target.position, enemyPosition.position) > agroRange && !patroling)
        {
            returnToStart();
            targetAquired = false;

            pPoint1Reached = false;
            pPoint2Reached = false;
            pPoint3Reached = false;
            pPoint4Reached = false;
            pPoint5Reached = false;
            pPoint6Reached = false;
        }
        else if (patroling)
        {
            patrol();
        }
        /****End patrol and follow****/



        /****Attack****/

        if (Vector3.Distance(target.position, enemyPosition.position) <= minRange + .02f && coolDown <= 0)
        {
            TryToAttack();
            coolDown = atkCoolDown;
        }

        coolDown -= Time.deltaTime;
        
        if (coolDown <= 0)
        {
            coolDown = 0;
        }


        if (damageTarget)
        {
            Attack();
            damageTarget = false;
        }

        if (coolDown <= 0)
        {
            animator.SetFloat("moveX", moveX);
            animator.SetFloat("moveY", moveY);

            moveX = target.position.x - enemyPosition.position.x;
            moveY = target.position.y - enemyPosition.position.y;
        }

    }

    public void followPlayer()
    {
        //animator.SetBool("enemyWalk", true); //sets walking animation
        //animator.SetFloat("moveX", (target.position.x - enemyPosition.position.x)); //sets enemy facing direction X axis
        //animator.SetFloat("moveY", (target.position.y - enemyPosition.position.y)); //sets enemy facing direction Y axis
        enemyPosition.position = Vector3.MoveTowards(enemyPosition.position, target.position, moveSpeed * Time.deltaTime);
    }

    public void returnToStart()
    {
        //animator.SetFloat("moveX", (returnPoint.position.x - enemyPosition.position.x)); //sets enemy facing direction X axis
        //animator.SetFloat("moveY", (returnPoint.position.y - enemyPosition.position.y)); //sets enemy facing direction Y axis
        
        enemyPosition.position = Vector3.MoveTowards(enemyPosition.position, returnPoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance (enemyPosition.position, returnPoint.position) == 0)
        {
            //animator.SetBool("enemyWalk", false);
        }
        
    }

    public void patrol()
    {
        if (!pPoint1Reached && patrolPoint1 != null)
        {
            enemyPosition.position = Vector3.MoveTowards(enemyPosition.position, patrolPoint1.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance (enemyPosition.position, patrolPoint1.position) == 0)
            {
                pPoint1Reached = true; 
            }
        }
        else if (pPoint1Reached && !pPoint2Reached && patrolPoint2 != null)
        {
            enemyPosition.position = Vector3.MoveTowards(enemyPosition.position, patrolPoint2.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance (enemyPosition.position, patrolPoint2.position) == 0)
            {
                pPoint2Reached = true; 
            }
        }
        else if (pPoint1Reached && pPoint2Reached && !pPoint3Reached && patrolPoint3 != null)
        {
            enemyPosition.position = Vector3.MoveTowards(enemyPosition.position, patrolPoint3.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance (enemyPosition.position, patrolPoint3.position) == 0)
            {
                pPoint3Reached = true; 
            }
        }
        else if (pPoint1Reached && pPoint2Reached && pPoint3Reached && !pPoint4Reached && patrolPoint4 != null)
        {
            enemyPosition.position = Vector3.MoveTowards(enemyPosition.position, patrolPoint4.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance (enemyPosition.position, patrolPoint4.position) == 0)
            {
                pPoint4Reached = true; 
            }
        }
        else if (pPoint1Reached && pPoint2Reached && pPoint3Reached && pPoint4Reached && !pPoint5Reached && patrolPoint5 != null)
        {
            enemyPosition.position = Vector3.MoveTowards(enemyPosition.position, patrolPoint5.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance (enemyPosition.position, patrolPoint5.position) == 0)
            {
                pPoint5Reached = true; 
            }
        }
        else if (pPoint1Reached && pPoint2Reached && pPoint3Reached && pPoint4Reached && pPoint5Reached && !pPoint6Reached && patrolPoint6 != null)
        {
            enemyPosition.position = Vector3.MoveTowards(enemyPosition.position, patrolPoint6.position, moveSpeed * Time.deltaTime);
            if (Vector3.Distance (enemyPosition.position, patrolPoint6.position) == 0)
            {
                pPoint6Reached = true; 
            }
        }

        if (pPoint6Reached)
        {
            pPoint1Reached = false;
            pPoint2Reached = false;
            pPoint3Reached = false;
            pPoint4Reached = false;
            pPoint5Reached = false;
            pPoint6Reached = false;
        }
        else if (pPoint5Reached && patrolPoint6 == null)
        {
            pPoint1Reached = false;
            pPoint2Reached = false;
            pPoint3Reached = false;
            pPoint4Reached = false;
            pPoint5Reached = false;
        }
        else if (pPoint4Reached && patrolPoint5 == null)
        {
            pPoint1Reached = false;
            pPoint2Reached = false;
            pPoint3Reached = false;
            pPoint4Reached = false;
        }
        else if (pPoint3Reached && patrolPoint4 == null)
        {
            pPoint1Reached = false;
            pPoint2Reached = false;
            pPoint3Reached = false;
        }
        else if (pPoint2Reached && patrolPoint3 == null)
        {
            pPoint1Reached = false;
            pPoint2Reached = false;
        }
        else if (pPoint1Reached && patrolPoint2 == null)
        {
            pPoint1Reached = false;
        }
    }

    void TryToAttack()
    {
        animator.SetTrigger("Attack");
    }

    void Attack()
    {
        //Detect enemies
        Collider2D[] hitCharacters = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, characterLayers);
        foreach (Collider2D character in hitCharacters)
        {
            Debug.Log("We hit " + character.name);
            character.GetComponent<player_combat>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
