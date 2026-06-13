using UnityEngine;
using UnityEngine.AI;

public class BossAI : MonoBehaviour
{
    [Header("Durum")]
    public bool uykuda = true; // YENÝ: Boss oyun baþýnda uykuda olacak

    [Header("Saldýrý Ayarlarý")]
    public float saldiriMesafesi = 2.5f;
    public int hasar = 30;
    public float saldiriHizi = 2f;

    private Transform player;
    private NavMeshAgent agent;
    private Animator anim;
    private float sonSaldiriZamani;

    public bool isDead = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        agent.stoppingDistance = saldiriMesafesi;

        if (GameObject.FindGameObjectWithTag("Oyuncu") != null)
        {
            player = GameObject.FindGameObjectWithTag("Oyuncu").transform;
        }
    }

    void Update()
    {
        // YENÝ: Eðer Boss uykudaysa, ölmüþse veya oyuncu yoksa hiçbir þey yapma, bekle!
        if (uykuda || player == null || isDead) return;

        float aradakiMesafe = Vector3.Distance(transform.position, player.position);

        if (aradakiMesafe > saldiriMesafesi)
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
            if (anim != null) anim.SetBool("Kosu", true);
        }
        else
        {
            agent.isStopped = true;
            if (anim != null) anim.SetBool("Kosu", false);

            Vector3 yon = (player.position - transform.position).normalized;
            yon.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(yon), Time.deltaTime * 5f);

            if (Time.time >= sonSaldiriZamani + saldiriHizi)
            {
                Saldir();
            }
        }
    }

    void Saldir()
    {
        sonSaldiriZamani = Time.time;
        if (anim != null) anim.SetTrigger("Saldiri");

        PlayerHealth oyuncuCan = player.GetComponent<PlayerHealth>();
        if (oyuncuCan != null) oyuncuCan.TakeDamage(hasar);
    }

    public void BossDurdur()
    {
        isDead = true;
        if (agent != null)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }
        if (anim != null) anim.SetBool("Kosu", false);
    }
}