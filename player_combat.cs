using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_combat : MonoBehaviour
{
    [Header("Inputs")]
    public KeyCode MeleeKey;
    public KeyCode Attack2Key;
    public KeyCode Attack3Key;

    [Header("Components")]
    public Animator animator;

    [Header("Attack Parameters")]
    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    public int attackDamage = 40;
    public float attackRate = 2f;

    [Header("Health")]
    public int maxHealth = 100;

    float timeBetweenDmg = 0;
    bool dmgTimeStart = false;
    int currentHealth;

    [Header("Info")]
    public float coolDown;
    public float cdModBetweenAtt;
    public float cdModAfterFinalAtt;
    public bool damageEnemy = false;
    public bool attacking = false;

    public float animTimer1;
    public float animTimer2;
    public float animTimer3;

    bool attack1IsDone = false;
    bool attack2IsDone = false;
    bool attack3IsDone = false;

    bool attack2Primed = false;
    bool attack3Primed = false;

    float timer1;
    float timer2;
    float timer3;

    float attackLockTimer = 0;




    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(MeleeKey))
        {
            if (!attack1IsDone) //procts if no attack has been done or cooldown reset it
            {
                attack1IsDone = true; //sets variable to true to enable animation countdown
                animator.SetTrigger("Attack1"); // begins animation
                timer1 = animTimer1; // sets timer for duration of the animation
                coolDown = animTimer1 + cdModBetweenAtt; // edits cooldown
                attackLockTimer = animTimer1 + cdModBetweenAtt - 0.1f;
            }
            else if (attack1IsDone && !attack2Primed) // if key was pressed twice this is accessed
            {
                attack2Primed = true; // this allows the second attack to begin
            }
            else if (attack1IsDone && attack2Primed && !attack3Primed) // if key was pressed thrice this is accessed
            {
                attack3Primed = true; // this allows the third attack to begin
            }
        }

        if (attack1IsDone) // if key was pressed once this begins timers
        {
            timer1 -= Time.deltaTime; // timer for animation
        }


        if (attack2Primed && timer1 <= 0 && !attack2IsDone) // accessed if key was pressed twice timer for first 
        {                                                   // animation is done and has not been accessed before
            animator.SetTrigger("Attack2"); // plays animation for second attack
            timer2 = animTimer2; // sets timer for the animation
            coolDown = animTimer2 + cdModBetweenAtt; // modifys cooldown after second attack
            attack2IsDone = true; // sets boolean so that timers begin
            attackLockTimer = animTimer2 + cdModBetweenAtt - 0.1f;
        }

        if (attack2IsDone) // bool check for if second attack has been started
        {
            timer2 -= Time.deltaTime; // timer for second attack animation
        }
        

        if (attack2IsDone && timer2 <= 0 && attack3Primed && !attack3IsDone)
        {
            animator.SetTrigger("Attack3");
            coolDown = animTimer3 + cdModAfterFinalAtt;
            attack3IsDone = true;
            attackLockTimer += animTimer3;
            attackLockTimer = animTimer3 + cdModBetweenAtt + 0.2f;
        }

        if (attack3IsDone)
        {
            timer3 -= Time.deltaTime;
        }

        coolDown -= Time.deltaTime;

        attackLockTimer -= Time.deltaTime;
        if (attackLockTimer > 0)
        {
            attacking = true;
        }
        else attacking = false;

        if (coolDown <= 0)
        {
            attack1IsDone = false;
            attack2IsDone = false;
            attack3IsDone = false;
            attack2Primed = false;
            attack3Primed = false;
        }

        if (coolDown <= 0)
            coolDown = 0;
        if (timer1 <= 0)
            timer1 = 0;
        if (timer2 <= 0)
            timer2 = 0;
        if (attackLockTimer <= 0)
            attackLockTimer = 0;

        if (damageEnemy)
        {
            Attack();
        }

        if (dmgTimeStart)
        {
            timeBetweenDmg -= Time.deltaTime;
        }
        if (timeBetweenDmg <= 0)
        {
            dmgTimeStart = false;
        }
    }

    void Attack()
    {
        //Detect enemies
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("We hit" + enemy.name);
            enemy.GetComponent<Enemy>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int damage)
    {
        if (timeBetweenDmg <= 0)
        {
            currentHealth -= damage;
            dmgTimeStart = true;
            timeBetweenDmg = .5f;
            animator.SetTrigger("Hurt");
        }

        //Play hurt animation

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Character died!");

        //Die animation
        //animator.SetBool("Death", true);

        //Disable the enemy
        //GetComponent<CircleCollider2D>().enabled = false;
        //GetComponent<BoxCollider2D>().enabled = false;
        //GetComponent<EnemyBrain>().enabled = false;
        //this.enabled = false;
    }
}
