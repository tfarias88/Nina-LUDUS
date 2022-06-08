using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    // Component variables
    private Animator anim;
    private BoxCollider2D col;
    private Rigidbody2D rig;

    [Header("Movement Variables")]
    public float moveSpeed;

    [Header("Health Variables")]
    public int currentHealth;
    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<BoxCollider2D>();
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive)
        {
            Move();
        }
    }

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
        //transform.position += movement * Time.deltaTime * moveSpeed;

        rig.velocity = new Vector2(movement.x * moveSpeed, movement.y * moveSpeed);

        // moving to right
        if(Input.GetAxis("Horizontal") > 0f)
        {
            transform.eulerAngles = new Vector3(0f,0f,0f);
            anim.SetBool("isMoving", true);  
        }
        // moving to left
        if(Input.GetAxis("Horizontal") < 0f)
        {  
            transform.eulerAngles = new Vector3(0f,180f,0f);
            anim.SetBool("isMoving", true);
        }
        // moving up or down
        if (Input.GetAxis("Vertical") > 0f || Input.GetAxis("Vertical") < 0f)
        {
            anim.SetBool("isMoving", true);
        }
        // not walking
        if(Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0f)
        {
            anim.SetBool("isMoving", false);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("damage");

        gameObject.GetComponent<LifeCount>().LoseLifeUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isAlive = false;
        anim.SetBool("death", true);
        Invoke("Restart", 1f);
    }

    void Restart()
    {
        SceneManager.LoadScene("lvl_1");
    }
}
