using UnityEngine;

public class BossArena : MonoBehaviour
{
    [Header("Arena Ayarlarý")]
    public BossAI bossYapayZeka;

    [Header("Görsel Kapýlar (Gerçek Tahta Kapýlar)")]
    public GameObject[] acikKapilar;   // Sahnede açýk duran modeller
    public GameObject[] kapaliKapilar; // Savaþ baþlayýnca belirecek kapalý modeller

    [Header("Görünmez Engeller")]
    public GameObject[] gorunmezDuvarlar; // Geçiþi engelleyen kutularýmýz

    private bool savasBasladi = false;

    void OnTriggerEnter(Collider other)
    {
        // 1. Fiziksel bir obje deðdiðinde onun sensörden geçiþini algýla
        if (other.CompareTag("Oyuncu") && !savasBasladi)
        {
            savasBasladi = true;

            // 2. Görünmez duvarlarý aktif et ki oyuncu kaçamasýn
            foreach (GameObject duvar in gorunmezDuvarlar)
            {
                if (duvar != null) duvar.SetActive(true);
            }

            // 3. ÝLLÜZYON: Açýk duran kapýlarý GÝZLE
            foreach (GameObject acikKapi in acikKapilar)
            {
                if (acikKapi != null) acikKapi.SetActive(false);
            }

            // 4. ÝLLÜZYON: Kapalý duran kapýlarý GÖSTER (Kapýlar çat diye kapanmýþ hissi verir)
            foreach (GameObject kapaliKapi in kapaliKapilar)
            {
                if (kapaliKapi != null) kapaliKapi.SetActive(true);
            }

            // 5. Boss'u uyandýr ve saldýrýya geçir
            if (bossYapayZeka != null)
            {
                bossYapayZeka.uykuda = false;
            }
        }
    }
}