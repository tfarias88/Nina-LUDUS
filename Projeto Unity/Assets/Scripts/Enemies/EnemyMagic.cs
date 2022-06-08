using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagic : MonoBehaviour
{
    public float speed;
    public int attackDamage;

    private Transform player;
    private Vector2 target;
    public GameObject particle;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if ((transform.position.x == target.x) && (transform.position.y == target.y))
        {
            GameObject effect = Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if ((collider.gameObject.tag != "ShooterEnemy") && (collider.gameObject.tag != "Enemy")
        && (collider.gameObject.tag != "Orb"))
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                collider.gameObject.GetComponent<Player>().TakeDamage(attackDamage);
            }

            GameObject effect = Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
            Destroy(gameObject);
        }
    }
}
