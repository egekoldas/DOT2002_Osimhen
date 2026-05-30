using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 3f;
    public GameObject interactUI;

    // Kamerayý koda tanýttýk
    public Camera mainCamera;

    public static bool hasKey = false;

    void Start()
    {
        // Eðer kamerayý sürüklemeyi unutursan, kod otomatik olarak sahnede MainCamera etiketli kamerayý bulur
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        if (interactUI != null)
        {
            interactUI.SetActive(false);
        }

        // Iþýnýn (Raycast) çýkýþ noktasýný artýk karakterin merkezi deðil, kameranýn merkezi yaptýk
        if (mainCamera != null)
        {
            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                if (hit.collider.CompareTag("Anahtar"))
                {
                    interactUI.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hit.collider.GetComponent<KeyPickup>().TakeKey();
                    }
                }
                else if (hit.collider.CompareTag("Kapi"))
                {
                    interactUI.SetActive(true);

                    if (Input.GetKeyDown(KeyCode.E) && hasKey)
                    {
                        hit.collider.GetComponentInParent<GateController>().OpenGate();
                    }
                }
            }
        }
    }
}