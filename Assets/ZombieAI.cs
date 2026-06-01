using UnityEngine;
using UnityEngine.AI;

public class ZombieAI : MonoBehaviour
{
    public float gorusMesafesi = 10f;
    public float saldiriMesafesi = 2f;
    public int hasar = 10;
    public float saldiriHizi = 1.5f;

    private Transform player;
    private NavMeshAgent agent;
    private float sonSaldiriZamani;

    // YENÝ: Animasyonlarý kontrol edecek bileþen
    private Animator anim;

    public bool isDead = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>(); // Animator'ü bul

        agent.stoppingDistance = saldiriMesafesi;

        if (GameObject.FindGameObjectWithTag("Oyuncu") != null)
        {
            player = GameObject.FindGameObjectWithTag("Oyuncu").transform;
        }
    }

    void Update()
    {
        if (player == null || isDead) return;

        float aradakiMesafe = Vector3.Distance(transform.position, player.position);

        if (aradakiMesafe <= gorusMesafesi)
        {
            if (aradakiMesafe > saldiriMesafesi)
            {
                // Yaklaþtýn, koþmaya baþla
                agent.isStopped = false;
                agent.SetDestination(player.position);

                // YENÝ: Koþma animasyonunu aktif et
                if (anim != null) anim.SetBool("Kosu", true);
            }
            else
            {
                // Saldýrý mesafesine girdi, DUR
                agent.isStopped = true;

                // YENÝ: Koþmayý býrak (bekleme/idle pozisyonuna geç)
                if (anim != null) anim.SetBool("Kosu", false);

                // Yüzünü sana dön
                Vector3 yon = (player.position - transform.position).normalized;
                yon.y = 0;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(yon), Time.deltaTime * 5f);

                if (Time.time >= sonSaldiriZamani + saldiriHizi)
                {
                    Saldir();
                }
            }
        }
        else
        {
            // Oyuncu uzaktaysa dur ve koþma animasyonunu kapat
            agent.isStopped = true;
            if (anim != null) anim.SetBool("Kosu", false);
        }
    }

    void Saldir()
    {
        sonSaldiriZamani = Time.time;

        // YENÝ: Vurma animasyonunu (Trigger) tetikle
        if (anim != null) anim.SetTrigger("Saldiri");

        PlayerHealth oyuncuCan = player.GetComponent<PlayerHealth>();
        if (oyuncuCan != null)
        {
            oyuncuCan.TakeDamage(hasar);
        }
    }

    public void ZombiyiOldur()
    {
        isDead = true;

        if (agent != null)
        {
            agent.isStopped = true;
            agent.enabled = false;
        }

        Collider zombiCollider = GetComponent<Collider>();
        if (zombiCollider != null)
        {
            zombiCollider.enabled = false;
        }

        // Ölünce arkada koþma animasyonu takýlý kalmasýn diye kapatýyoruz
        if (anim != null) anim.SetBool("Kosu", false);
    }
}