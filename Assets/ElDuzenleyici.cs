using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging; // YENÝ: Rig sistemi için gerekli kütüphane

public class ElDuzenleyici : MonoBehaviour
{
    public Animator animator;
    public RigBuilder rigBuilder; // YENÝ: Unity'deki RigBuilder bileþeni

    [Header("Sað El (Silahýn Olduðu El)")]
    public Transform sagElKemigi;
    public Vector3 sagElKaydirma = Vector3.zero;

    [Header("Sol El (Niþan Alýnca Bozan El)")]
    public Transform solElKemigi;
    public Vector3 solElNisanKaydirma = new Vector3(0, -0.3f, 0);

    private bool sistemHazir = false;

    void Start()
    {
        sistemHazir = false;
        StartCoroutine(TamYuklenmeBekle());
    }

    IEnumerator TamYuklenmeBekle()
    {
        // 1. ADIM: Karakter doðduðunda bozulan Rig (Ýskelet) sistemini zorla yeniden inþa et!
        if (rigBuilder != null)
        {
            rigBuilder.Build();
        }

        // 2. ADIM: Animator hafýzasýný temizle ve SilahKatmani'ni aktif et
        if (animator != null)
        {
            // SilahKatmani (Index 1) aðýrlýðýný zorla 1 yapýyoruz
            animator.SetLayerWeight(1, 1f);

            animator.Rebind();
            animator.Update(0f);
        }

        // Oyun motorunun kemikleri yerine oturtmasý için çok kýsa bir süre bekle
        yield return new WaitForSeconds(0.1f);

        sistemHazir = true; // Her þey yerine oturdu, kaydýrmaya baþlayabilirsin!
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