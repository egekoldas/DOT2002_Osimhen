using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ElDuzenleyici : MonoBehaviour
{
    public Animator animator;
    public RigBuilder rigBuilder;

    [Header("Sað El (Silahýn Olduðu El)")]
    public Transform sagElKemigi;
    public Vector3 sagElKaydirma = Vector3.zero;

    [Header("Sol El (Niþan Alýnca Bozan El)")]
    public Transform solElKemigi;
    public Vector3 solElNisanKaydirma = new Vector3(0, -0.3f, 0);

    private bool sistemHazir = false;

    void Awake()
    {
        // GÜVENLÝK KÝLÝDÝ: Menüden gelirken zaman donuk kaldýysa zorla aç!
        Time.timeScale = 1f;
    }

    void Start()
    {
        sistemHazir = false;
        StartCoroutine(SokTedavisi());
    }

    IEnumerator SokTedavisi()
    {
        // Sistemin kendine gelmesi için saniyenin onda biri kadar bekle
        yield return new WaitForSecondsRealtime(0.1f);

        // EN KESÝN ÇÖZÜM: Rig sisteminin fiþini çekip geri takýyoruz
        if (rigBuilder != null)
        {
            rigBuilder.enabled = false; // Kapat
            yield return null; // 1 kare bekle
            rigBuilder.enabled = true; // Geri aç
            rigBuilder.Build(); // Þimdi zorla inþa et
        }

        if (animator != null)
        {
            animator.SetLayerWeight(1, 1f);
            animator.Rebind();
            animator.Update(0f);
        }

        sistemHazir = true;
    }

    void LateUpdate()
    {
        if (animator == null || !sistemHazir) return;

        // 1. SAÐ EL
        if (sagElKemigi != null)
        {
            sagElKemigi.position += transform.right * sagElKaydirma.x +
                                    transform.up * sagElKaydirma.y +
                                    transform.forward * sagElKaydirma.z;
        }

        // 2. SOL EL
        if (solElKemigi != null)
        {
            if (animator.GetBool("NisanAl"))
            {
                solElKemigi.position += transform.right * solElNisanKaydirma.x +
                                        transform.up * solElNisanKaydirma.y +
                                        transform.forward * solElNisanKaydirma.z;
            }
        }
    }
}