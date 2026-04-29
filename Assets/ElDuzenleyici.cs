using UnityEngine;

public class ElDuzenleyici : MonoBehaviour
{
    public Animator animator;

    [Header("Sað El (Silahýn Olduðu El)")]
    public Transform sagElKemigi;
    public Vector3 sagElKaydirma = Vector3.zero;

    [Header("Sol El (Niþan Alýnca Bozan El)")]
    public Transform solElKemigi;
    public Vector3 solElNisanKaydirma = new Vector3(0, -0.3f, 0);

    [Header("Hata Ayýklama (Debug)")]
    public bool testModu = false; // Bunu açarsan niþan almasan da el hareket eder

    void LateUpdate()
    {
        // 1. SAÐ EL
        if (sagElKemigi != null)
        {
            // Karakterin kendi yönlerine göre (Sað, Yukarý, Ýleri) kaydýrma yapar
            sagElKemigi.position += transform.right * sagElKaydirma.x +
                                    transform.up * sagElKaydirma.y +
                                    transform.forward * sagElKaydirma.z;
        }

        // 2. SOL EL (Niþan Alýrken)
        if (solElKemigi != null && animator != null)
        {
            // Eðer 'NisanAl' parametresi çalýþmýyorsa 'testModu' ile kontrol edebilirsin
            if (animator.GetBool("NisanAl") || testModu)
            {
                // transform.up karakterin kafasýna doðrudur, eksi (-) ile aþaðý çekeriz
                solElKemigi.position += transform.right * solElNisanKaydirma.x +
                                        transform.up * solElNisanKaydirma.y +
                                        transform.forward * solElNisanKaydirma.z;
            }
        }
    }
}