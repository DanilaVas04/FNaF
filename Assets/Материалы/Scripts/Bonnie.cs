using UnityEngine;
using UnityEngine.AI;

public class Bonnie : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    public float lookRadius;
    public float timeReload = 0;
    public Animator anime;
    public GameObject player;
    public Camera playerCamera;
    public Camera dieCamera;
    public AudioSource scream;
    private FirstPersonMovement movment;
    private Jump jump;
    private Crouch crouch;
    private FirstPersonLook look;
    public string[] animeName;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        movment = player.GetComponent<FirstPersonMovement>();
        jump = player.GetComponent<Jump>();
        crouch = player.GetComponent<Crouch>();
        look = playerCamera.GetComponent<FirstPersonLook>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            anime.SetBool("isRun", true);
            if (distance <= agent.stoppingDistance)
            {
                timeReload += 1f;
                if (timeReload == 2f)
                {
                    agent.isStopped = true;
                    agent.ResetPath();

                    movment.enabled = false;
                    jump.enabled = false;
                    crouch.enabled = false;
                    look.enabled = false;
                    playerCamera.depth = 0;
                    dieCamera.depth = 1;
                    scream.Play();
                    anime.Play(animeName[0]);
                    anime.SetBool("isDie", true);
                    anime.SetBool("isRun", false);
                    
                }else if (timeReload >= 100f) { PlayerManager.instance.death = true; }
                LookTarget();
            }
            else 
            {
                agent.SetDestination(target.position);
                anime.Play(animeName[1]); 
            }
        }
        else
        {
            anime.SetBool("isRun", false);
        }
    }

    void LookTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }


}
