using UnityEngine;

public class ZombiCan : MonoBehaviour
{
    [Header("Can Ayarlarý")]
    public float can = 50f;
    public Animator animator;
    private bool olduMu = false;

    [Header("Ses Ayarlarý")]
    public AudioSource sesKaynagi; // Zombinin üzerindeki AudioSource
    public AudioClip hirlamaSesi;  // Boþta dururken çýkacak ses
    public AudioClip olmeSesi;     // Öldüðünde çýkacak ses

    void Start()
    {
        // Oyun baþladýðýnda hýrýltý sesini baþlat ve döngüye al
        if (sesKaynagi != null && hirlamaSesi != null)
        {
            sesKaynagi.clip = hirlamaSesi;
            sesKaynagi.loop = true; // Sürekli çalmasý için
            sesKaynagi.Play();
        }
    }

    public void HasarAl(float miktar)
    {
        if (olduMu) return; // Zaten ölüyse iþlem yapma

        can -= miktar;

        if (can <= 0)
        {
            Oldu();
        }
    }

    void Oldu()
    {
        olduMu = true;

        // 1. Hýrýltýyý durdur ve ölüm sesini bir kez çal
        if (sesKaynagi != null)
        {
            sesKaynagi.Stop(); // Hýrýltýyý kes
            if (olmeSesi != null)
            {
                sesKaynagi.PlayOneShot(olmeSesi); // Ölüm sesini patlat
            }
        }

        // 2. Animasyonu tetikle
        if (animator != null)
        {
            animator.SetTrigger("Olme");
        }

        // 3. Collider'ý kapat (Ölü zombiye ateþ edilmesin)
        Collider col = GetComponent<Collider>();
        if (col != null) col.enabled = false;

        Debug.Log("Zombi etkisiz hale getirildi.");

        // 4. Öldükten 5 saniye sonra (sesin bitmesi için süre tanýdýk) objeyi kaldýr
        Destroy(gameObject, 5f);
    }
}