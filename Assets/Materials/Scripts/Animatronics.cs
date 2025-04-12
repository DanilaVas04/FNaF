using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class Animatronics : MonoBehaviour
{
    Transform target;
    NavMeshAgent agent;
    public string nameAnimatronic;
    public float lookRadius;
    public float timeReload = 0;
    public Animator anime;
    public GameObject player;
    public Camera playerCamera;
    public Camera dieCamera;
    public AudioSource scream;
    private FirstPersonMovement movement;
    private Jump jump;
    private Crouch crouch;
    private FirstPersonLook look;
    private bool isDie = false;
    public string[] animeName;
    public GameObject[] map;
    public GameObject lightScream;
    private int difficult;
    public GameObject[] pointMove;

    private float timerMax = 15f;
    private float timeNow = 15f;
    private int random;
    private float distanceToTarget,dist;
    private int currentPointIndex = -1;
    private bool isAttack = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        movement = player.GetComponent<FirstPersonMovement>();
        jump = player.GetComponent<Jump>();
        crouch = player.GetComponent<Crouch>();
        look = playerCamera.GetComponent<FirstPersonLook>();
        SetDifficulty();
    }

    private void SetDifficulty()
    {
        switch (nameAnimatronic)
        {
            case "Freddy":
                difficult = Scenes.Freddy;
                break;
            case "Bonnie":
                difficult = Scenes.Bonnie;
                break;
            case "Chica":
                difficult = Scenes.Chica;
                break;
        }
    }

    private void FixedUpdate()
    {
        if (difficult != 0)
        {
            HandleTimer();
            distanceToTarget = Vector3.Distance(target.position, transform.position);
            if (timeNow < timerMax)
            {
                if (isAttack)
                {
                    RunToPlayer(distanceToTarget);
                }
                else if (random <= difficult + 30)
                {
                    dist = Vector3.Distance(pointMove[currentPointIndex].transform.position, transform.position);
                    RunToPoint(dist, pointMove[currentPointIndex].transform.position);
                }
                else
                {
                    StandInPlace(distanceToTarget);
                }
            }
            else if (!isAttack && pointMove.Length-1==currentPointIndex)
            {
                isAttack = true;
                timeNow = 0f;
                currentPointIndex = -1;
                timerMax = 20f;
            }
            else
            {
                timeNow = 0f;
                isAttack = false;
                timerMax = 15f;
            }
        }
    }

    private void HandleTimer()
    {
        if (timeNow == 0f)
        {
            random = Random.Range(1, 101);
            anime.SetBool("isRun", false);
            agent.isStopped = true;
            agent.ResetPath();       
            if (random <= difficult + 30)
            {
                currentPointIndex = (currentPointIndex + 1) % pointMove.Length;
            }
        }
        timeNow += Time.deltaTime;
    }

    private void RunToPlayer(float distance)
    {
        if (distance > agent.stoppingDistance && !isDie)
        {
            anime.SetBool("isRun", true);
            agent.SetDestination(target.position);
            LookTarget(target.position);
            anime.Play(animeName[1]);
        }
        else if (distance <= agent.stoppingDistance-0.5 && !isDie)
        {
            StopAgent();
        }
        else
        {
            anime.SetBool("isRun", false);
        }
    }

    private void RunToPoint(float distance, Vector3 point)
    {
        if (distanceToTarget <= agent.stoppingDistance-0.5 && !isDie)
        {
            StopAgent();
        }
        if (distance > agent.stoppingDistance && !isDie)
        {
            anime.SetBool("isRun", true);
            agent.SetDestination(point);
            LookTarget(point);
            anime.Play(animeName[1]);
        }
        else if (distance <= agent.stoppingDistance)
        {
            agent.ResetPath();
            anime.SetBool("isRun", false);
            agent.SetDestination(point);
            LookTarget(target.position);
            agent.isStopped = true;
            
        }
    }

    private void StandInPlace(float distance)
    {
        anime.SetBool("isRun", false);
        agent.isStopped = true;
        agent.ResetPath();
        LookTarget(target.position);

        if (distance <= agent.stoppingDistance-0.5 && !isDie)
        {
            StopAgent();
        }
    }

    private void StopAgent()
    {
        agent.isStopped = true;
        agent.ResetPath();
        isDie = true;
        DisablePlayerControls();
        lightScream.SetActive(true);
        playerCamera.depth = 0;
        dieCamera.depth = 1;
        scream.Play();
        anime.Play(animeName[0]);
        anime.SetBool("isDie", true);
        anime.SetBool("isRun", false);

        foreach (GameObject m in map)
        {
            m.SetActive(false);
        }

        PlayerManager.instance.death = true;
        LookTarget(target.position);
    }

    private void DisablePlayerControls()
    {
        movement.enabled = false;
        jump.enabled = false;
        crouch.enabled = false;
        look.enabled = false;
    }

    void LookTarget(Vector3 targetPoint)
    {
        Vector3 direction = (targetPoint - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
