using UnityEngine;

public class BossCan : MonoBehaviour
{
    [Header("Boss Ayarlarý")]
    public float can = 500f;
    public Animator animator;
    private bool olduMu = false;

    [Header("Ses Ayarý")]
    public AudioSource bossSesi; // Ölünce kapanacak olan ses bileþeni

    [Header("Zafer Ayarlarý (Kapýlar)")]
    public GameObject[] acikKapilar;
    public GameObject[] kapaliKapilar;
    public GameObject[] gorunmezDuvarlar;

    public void HasarAl(float miktar)
    {
        if (olduMu) return;

        can -= miktar;
        if (can <= 0)
        {
            Oldu();
        }
    }

    void Oldu()
    {
        olduMu = true;

        if (animator != null) animator.SetTrigger("Olme");

        // Boss ölür ölmez kükreme sesini çat diye kesiyoruz
        if (bossSesi != null)
        {
            bossSesi.Stop();
        }

        BossAI yapayZeka = GetComponent<BossAI>();
        if (yapayZeka != null) yapayZeka.BossDurdur();

        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Debug.Log("BOSS ÖLDÜRÜLDÜ! Kapýlar açýlýyor!");

        foreach (GameObject kapali in kapaliKapilar)
        {
            if (kapali != null) kapali.SetActive(false);
        }

        foreach (GameObject acik in acikKapilar)
        {
            if (acik != null) acik.SetActive(true);
        }

        foreach (GameObject duvar in gorunmezDuvarlar)
        {
            if (duvar != null) duvar.SetActive(false);
        }

        // DÜZELTÝLEN KISIM: Oyun Yönetim sistemine Boss'un öldüðünü haber ver (Þýrýnga aþamasý baþlar)
        OyunYoneticisi yonetici = FindObjectOfType<OyunYoneticisi>();
        if (yonetici != null)
        {
            yonetici.BossOlduSistemi();
        }
    }
}