using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections; // YENÝ: Zamanlayýcý (Coroutine) kullanabilmek için ÞART!

public class AtesSistemi : MonoBehaviour
{
    [Header("Gerekli Objeler")]
    public Camera fpsCam;
    public GameObject mermiPrefab;
    public Transform atesNoktasi;
    public Animator animator;

    [Header("Mermi Sistemi Ayarlarý")]
    public int mevcutMermi = 30;
    public int sarjorKapasitesi = 30;
    public int yedekMermi = 60;
    public TextMeshProUGUI mermiYazisiUI;

    [Header("Ateþ Ayarlarý")]
    public float mermiHizi = 60f;
    public float mermiÖmrü = 3f;
    public float atesAraligi = 0.2f;
    private float sonrakiAtesZamani = 0f;

    [Header("Ses ve Animasyon Kilidi")]
    public AudioSource reloadSesi; // YENÝ: Sürükleyip býrakacaðýmýz ses kaynaðý
    private bool mermiYenileniyorMu = false; // YENÝ: Ateþ etmeyi engelleyecek kilit

    [Header("Olaylar")]
    public UnityEvent atesEdildiEventi;

    void Start()
    {
        MermiArayuzunuGuncelle();
    }

    void Update()
    {
        // ÇOK KRÝTÝK: Eðer mermi yenileniyorsa, bu satýrdan aþaðýsý ÇALIÞMAZ! (Ateþ edemez)
        if (mermiYenileniyorMu) return;

        Debug.DrawRay(fpsCam.transform.position, fpsCam.transform.forward * 10f, Color.blue);

        if (Input.GetMouseButton(0) && Time.time >= sonrakiAtesZamani && mevcutMermi > 0)
        {
            sonrakiAtesZamani = Time.time + atesAraligi;
            AtesEt();
        }

        if (Input.GetKeyDown(KeyCode.R) && mevcutMermi < sarjorKapasitesi && yedekMermi > 0)
        {
            // Normal fonksiyon yerine "Zamanlayýcýlý" sistemi baþlatýyoruz
            StartCoroutine(MermiDegistirmeSistemi());
        }

        animator.SetBool("NisanAl", Input.GetMouseButton(1));
    }

    void AtesEt()
    {
        mevcutMermi--;
        MermiArayuzunuGuncelle();

        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Vector3 hedefNokta;

        ray.origin = ray.GetPoint(1.2f);
        int layerMask = ~LayerMask.GetMask("Oyuncu");

        if (Physics.Raycast(ray, out hit, 100f, layerMask))
        {
            hedefNokta = hit.point;
            Debug.DrawLine(ray.origin, hit.point, Color.red, 2f);

            ZombiCan zombi = hit.transform.GetComponentInParent<ZombiCan>();
            if (zombi != null)
            {
                if (hit.collider.CompareTag("Kafa")) zombi.HasarAl(50f);
                else if (hit.collider.CompareTag("Gövde")) zombi.HasarAl(25f);
            }

            BossCan boss = hit.transform.GetComponentInParent<BossCan>();
            if (boss != null)
            {
                if (hit.collider.CompareTag("Kafa")) boss.HasarAl(50f);
                else if (hit.collider.CompareTag("Gövde")) boss.HasarAl(25f);
                else boss.HasarAl(25f);
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

    // YENÝ: ZAMAN AYARLI MERMÝ DEÐÝÞTÝRME FONKSÝYONU
    IEnumerator MermiDegistirmeSistemi()
    {
        mermiYenileniyorMu = true; // 1. Kilidi kapat (Artýk farenin sol týký çalýþmaz)

        float beklemeSuresi = 2f; // Ses koymayý unutursan diye oyunu bozmamak için 2 saniye varsayýlan süre

        // Eðer ses efekti eklenmiþse;
        if (reloadSesi != null && reloadSesi.clip != null)
        {
            reloadSesi.Play(); // 2. Sesi çal
            beklemeSuresi = reloadSesi.clip.length; // Sesin uzunluðu kaç saniyeyse (örneðin 1.5s), bekleme süresini ona eþitle
        }

        // 3. Ses bitene kadar sistemi tam burada dondurarak bekle
        yield return new WaitForSeconds(beklemeSuresi);

        // 4. Ses bittiðine göre artýk mermi matematiðini yapabiliriz
        int eksikMermi = sarjorKapasitesi - mevcutMermi;

        if (yedekMermi >= eksikMermi)
        {
            mevcutMermi += eksikMermi;
            yedekMermi -= eksikMermi;
        }
        else
        {
            mevcutMermi += yedekMermi;
            yedekMermi = 0;
        }

        MermiArayuzunuGuncelle();
        mermiYenileniyorMu = false; // 5. Ýþlem bitti, Kilidi aç (Tekrar ateþ edilebilir)
    }

    public void YedekMermiEkle(int miktar)
    {
        yedekMermi += miktar;
        MermiArayuzunuGuncelle();
    }

    void MermiArayuzunuGuncelle()
    {
        if (mermiYazisiUI != null)
        {
            mermiYazisiUI.text = mevcutMermi + " / " + yedekMermi;
        }
    }
}