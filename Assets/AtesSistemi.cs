using UnityEngine;
using UnityEngine.Events;

public class AtesSistemi : MonoBehaviour
{
    [Header("Gerekli Objeler")]
    public Camera fpsCam;
    public GameObject mermiPrefab;
    public Transform atesNoktasi;
    public Animator animator;

    [Header("Ayarlar")]
    public float mermiHizi = 60f;
    public float mermiÖmrü = 3f;
    public float atesAraligi = 0.2f;
    private float sonrakiAtesZamani = 0f;

    [Header("Olaylar")]
    public UnityEvent atesEdildiEventi;

    void Update()
    {
        Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * 10f, Color.blue);

        if (Input.GetMouseButton(0) && Time.time >= sonrakiAtesZamani)
        {
            sonrakiAtesZamani = Time.time + atesAraligi;
            AtesEt();
        }

        animator.SetBool("NisanAl", Input.GetMouseButton(1));
    }

    void AtesEt()
    {
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 hedefNokta;

        ray.origin = ray.GetPoint(1.2f);

        int layerMask = ~LayerMask.GetMask("Oyuncu");

        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            hedefNokta = hit.point;

            Debug.DrawLine(ray.origin, hit.point, Color.red, 2f);

            // 1. NORMAL ZOMBÝ KONTROLÜ
            ZombiCan zombi = hit.transform.GetComponentInParent<ZombiCan>();
            if (zombi != null)
            {
                if (hit.collider.CompareTag("Kafa")) zombi.HasarAl(50f);
                else if (hit.collider.CompareTag("Gövde")) zombi.HasarAl(25f);
            }

            // 2. YENÝ EKLENEN KISIM: BOSS KONTROLÜ
            BossCan boss = hit.transform.GetComponentInParent<BossCan>();
            if (boss != null)
            {
                // Boss'u kafadan veya gövdeden vurma kontrolü
                if (hit.collider.CompareTag("Kafa")) boss.HasarAl(50f);
                else if (hit.collider.CompareTag("Gövde")) boss.HasarAl(25f);
                else boss.HasarAl(25f); // Eðer Boss'ta Kafa/Gövde etiketi yoksa standart 25 hasar vursun
            }
        }
        else
        {
            hedefNokta = ray.GetPoint(100f);
        }

        Vector3 atisYonu = (hedefNokta - atesNoktasi.position).normalized;
        GameObject yeniMermi = Instantiate(mermiPrefab, atesNoktasi.position, Quaternion.LookRotation(atisYonu));

        Rigidbody rb = yeniMermi.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = atisYonu * mermiHizi;
        }

        Destroy(yeniMermi, mermiÖmrü);
        if (atesEdildiEventi != null) atesEdildiEventi.Invoke();
    }
}