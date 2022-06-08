using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public GameObject door;
    public GameObject doorCollider;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = door.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            anim.SetBool("key", true);
            doorCollider.SetActive(true);
        }
        Destroy(gameObject);
    }
}
