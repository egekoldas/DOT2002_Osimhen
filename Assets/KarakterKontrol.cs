using UnityEngine;

public class KarakterKontrol : MonoBehaviour
{
    [Header("Hareket Ayarlarý")]
    public CharacterController controller;
    public Animator animator;
    public float yürümeHýzý = 4f;
    public float koþmaHýzý = 8f;
    public float yerçekimi = -15f;
    public float zýplamaGücü = 3f;

    [Header("Fare & Kamera Ayarlarý")]
    public Transform boyunObjesi; // Hiyerarþideki 'KameraSistemi_Merkez' objesini buraya koy
    public float fareHassasiyeti = 200f;
    private float xRotasyonu = 0f;

    Vector3 hýzVektörü;
    bool yerdemi;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // --- 1. FARE ÝLE ETRAFA BAKMA (DÜZELTÝLMÝÞ) ---
        float mouseX = Input.GetAxis("Mouse X") * fareHassasiyeti * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * fareHassasiyeti * Time.deltaTime;

        // Karakterin gövdesini saða-sola döndürür
        transform.Rotate(Vector3.up * mouseX);

        if (boyunObjesi != null)
        {
            // SÝHÝRLÝ DOKUNUÞ: Kamerayý (Boyun Objesi) saða-sola da karakterle ayný anda döndürüyoruz
            // Böylece kamera baðýmsýz olsa bile gövdeyle beraber döner.
            boyunObjesi.Rotate(Vector3.up * mouseX, Space.World);

            // Yukarý Aþaðý Bakýþ (Kendi ekseninde)
            xRotasyonu -= mouseY;
            xRotasyonu = Mathf.Clamp(xRotasyonu, -80f, 80f);

            // Y eksenindeki dönüþü koruyarak sadece X ekseninde (yukarý-aþaðý) eðiyoruz
            boyunObjesi.localRotation = Quaternion.Euler(xRotasyonu, boyunObjesi.localRotation.eulerAngles.y, 0f);
        }

        // --- 2. HAREKET MANTIÐI (WASD) ---
        yerdemi = controller.isGrounded;
        if (yerdemi && hýzVektörü.y < 0) hýzVektörü.y = -2f;

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 hareketYönü = transform.right * x + transform.forward * z;

        if (hareketYönü.magnitude >= 0.1f)
        {
            float anlýkHýz = Input.GetKey(KeyCode.LeftShift) ? koþmaHýzý : yürümeHýzý;
            controller.Move(hareketYönü * anlýkHýz * Time.deltaTime);

            float animHýz = Input.GetKey(KeyCode.LeftShift) ? 1f : 0.5f;
            animator.SetFloat("Hiz", animHýz);
        }
        else
        {
            animator.SetFloat("Hiz", 0f);
        }

        // --- 3. ZIPLAMA ---
        if (Input.GetButtonDown("Jump") && yerdemi)
        {
            hýzVektörü.y = Mathf.Sqrt(zýplamaGücü * -2f * yerçekimi);
            animator.SetTrigger("Zipla");
        }

        hýzVektörü.y += yerçekimi * Time.deltaTime;
        controller.Move(hýzVektörü * Time.deltaTime);
    }
}