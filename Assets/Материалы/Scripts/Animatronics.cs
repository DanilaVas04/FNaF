using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

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
    private int difficult = 2;
    public GameObject[] pointMove;

    private float timerMax = 10f;
    private float timeNow = 10f;
    private int random,randomPoint;
    private float distance;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = PlayerManager.instance.player.transform;
        movement = player.GetComponent<FirstPersonMovement>();
        jump = player.GetComponent<Jump>();
        crouch = player.GetComponent<Crouch>();
        look = playerCamera.GetComponent<FirstPersonLook>();
        //switch (nameAnimatronic)
        //{
        //    case "Freddy":
        //        difficult = Scenes.Freddy;
        //        break;
        //    case "Bonnie":
        //        difficult = Scenes.Bonnie;
        //        break;
        //    case "Chika":
        //        difficult = Scenes.Chica;
        //        break;
        //    case "Foxy":
        //        difficult = Scenes.Foxy;
        //        break;
        //}
    }

    private void Update()
    {     
        if (difficult != 0)
        {
            if (timeNow == 0f) { random = Random.Range(1, 101); Debug.Log(random.ToString()); }
            if (timeNow == 0f) { randomPoint = Random.Range(0, pointMove.Length); Debug.Log("====" + randomPoint.ToString()); }
            timeNow += Time.deltaTime;

            distance = Vector3.Distance(target.position, transform.position);
            if (random <= difficult && timeNow < timerMax)
            {
                distance = Vector3.Distance(target.position, transform.position);
                runningToPlayer(distance);
            }
            else if (random>difficult && random<=60 && timeNow < timerMax)
            {              
                distance = Vector3.Distance(pointMove[randomPoint].transform.position, transform.position);
                runningToPoint(distance, pointMove[randomPoint].transform.position);
            }
            else if (timeNow < timerMax)
            {
                distance = Vector3.Distance(target.position, transform.position);
                standInPlace(distance);             
            }
            else
            {
                timeNow = 0f;
            }
        }
    }

    private void runningToPlayer(float distance)
    {
        if (distance > agent.stoppingDistance)
        {
            anime.SetBool("isRun", true);
            if (Vector3.Distance(target.position, transform.position) <= agent.stoppingDistance && !isDie)
            {
                StopAgent();
            }
            else if (!isDie)
            {
                // Двигаться к цели
                agent.SetDestination(target.position);
                LookTarget(target.position);
                anime.Play(animeName[1]);
            }else
            {
                anime.SetBool("isRun", false);
            }
        }
        else
        {
            anime.SetBool("isRun", false);
        }
    }

    private void runningToPoint(float distance, Vector3 point)
    {
        anime.SetBool("isRun", true);
        if (Vector3.Distance(target.position, transform.position) <= agent.stoppingDistance && !isDie)
        {
            StopAgent();
        }
        else if (distance <= agent.stoppingDistance)
        {
            anime.SetBool("isRun", false);
            agent.isStopped = true;
            agent.ResetPath();
        }
        else if (!isDie)
        {
            agent.SetDestination(point);
            LookTarget(point);
            anime.Play(animeName[1]);
        }
    }

    private void standInPlace(float distance)
    {
        anime.SetBool("isRun", false);
        agent.isStopped = true;
        agent.ResetPath();
        if (distance <= agent.stoppingDistance && !isDie)
        {
            StopAgent();
        }
    }

    private async Task StopAgent()
    {
        agent.isStopped = true;
        agent.ResetPath();
        isDie = true;
        movement.enabled = false;
        jump.enabled = false;
        crouch.enabled = false;
        look.enabled = false;
        lightScream.SetActive(true);
        playerCamera.depth = 0;
        dieCamera.depth = 1;
        scream.Play();
        anime.Play(animeName[0]);
        anime.SetBool("isDie", true);
        anime.SetBool("isRun", false);
        foreach (GameObject m in map) { m.SetActive(false); }       
        await Task.Delay(4000);
        PlayerManager.instance.death = true;
        LookTarget(target.position);
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
