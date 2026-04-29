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
        // MERKEZÝ KONTROL ETMEK ÝÇÝN: Sahne ekranýnda (Scene) her zaman mavi bir çizgi çizer
        // Eðer bu çizgi beyaz noktanla ayný yere bakmýyorsa, kamera baðlantýn yanlýþtýr.
        Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * 10f, Color.blue);

        // --- GÜNCELLEME BURADA ---
        // Sadece Farenin Sol Týk (0) tuþuna basýldýðýnda çalýþýr. CTRL tuþunu iptal ettik.
        if (Input.GetMouseButton(0) && Time.time >= sonrakiAtesZamani)
        {
            sonrakiAtesZamani = Time.time + atesAraligi;
            AtesEt();
        }

        animator.SetBool("NisanAl", Input.GetMouseButton(1));
    }

    void AtesEt()
    {
        // 1. Ekranýn tam ortasýný (0.5, 0.5) hedef al
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 hedefNokta;

        // Iþýný vücudun 1.2 metre ilerisinden baþlat (Karaktere çarpmamasý için)
        ray.origin = ray.GetPoint(1.2f);

        // Oyuncu layer'ýný yok say
        int layerMask = ~LayerMask.GetMask("Oyuncu");

        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            hedefNokta = hit.point;

            // HATA AYIKLAMA: Ateþ ettiðin an sahne ekranýnda KIRMIZI bir çizgi çýkar
            // Eðer zombiye vurduðun halde caný gitmiyorsa sorun Zombi scriptindedir.
            Debug.DrawLine(ray.origin, hit.point, Color.red, 2f);

            ZombiCan zombi = hit.transform.GetComponentInParent<ZombiCan>();
            if (zombi != null)
            {
                if (hit.collider.CompareTag("Kafa")) zombi.HasarAl(50f);
                else if (hit.collider.CompareTag("Gövde")) zombi.HasarAl(25f);
            }
        }
        else
        {
            hedefNokta = ray.GetPoint(100f);
        }

        // 2. Mermiyi OLUÞTUR ve HEDEFE fýrlat
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