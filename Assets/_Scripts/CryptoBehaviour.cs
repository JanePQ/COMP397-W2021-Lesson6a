using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum CryptoState
{
    IDLE,
    RUN,
    JUMP
}

public class CryptoBehaviour : MonoBehaviour
{
    [Header("Line if Sight")]
    //public LayerMask collisionLayer;
    //public Vector3 LOSoffset = new Vector3(0.0f, 2.0f, -5.0f);
    public bool HasLOS;
    public GameObject player;

    private NavMeshAgent agent;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        var size = new Vector3(4.0f, 2.0f, 10.0f);
        RaycastHit hit;
        HasLOS = Physics.BoxCast(transform.position + LOSoffset, size, transform.forward, out hit, transform.rotation, 10.0f, collisionLayer );
        //HasLOS = Physics.BoxCast(transform.position + LOSoffset, size * 0.5f, transform.forward, transform.rotation, 20.0f, collisionLayer);
        //HasLOS = Physics.BoxCast(transform.position + LOSoffset, size * 0.5f, transform.forward, out hit, transform.rotation, 10.0f);
       

        if (HasLOS)
        {
            Debug.Log(hit.transform.gameObject.name);
        }
         */
        if (HasLOS)
        {
            agent.SetDestination(player.transform.position);
        }

        if(HasLOS && Vector3.Distance(transform.position, player.transform.position) < 2.5)
        {
           // could be an attack
             animator.SetInteger("AnimState", (int)CryptoState.IDLE);
             transform.LookAt(transform.forward - player.transform.forward);

            if (agent.isOnOffMeshLink)
            {
                animator.SetInteger("Animate", (int)CryptoState.JUMP);
            }
        }
        else
        {
             animator.SetInteger("AnimState", (int)CryptoState.RUN);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HasLOS = true;
            player = other.transform.gameObject;

        }
     
    }
}
