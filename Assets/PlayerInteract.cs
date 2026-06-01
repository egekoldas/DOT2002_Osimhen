using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 3f;
    public GameObject interactUI;
    public Camera mainCamera;

    public static bool hasKey = false;
    public static bool hasBossKey = false; // YENÝ: Boss anahtarý kontrolü

    void Start()
    {
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

        if (mainCamera != null)
        {
            Ray ray = new Ray(mainCamera.transform.position, mainCamera.transform.forward);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactDistance))
            {
                // 1. NORMAL ANAHTAR
                if (hit.collider.CompareTag("Anahtar"))
                {
                    interactUI.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E)) hit.collider.GetComponent<KeyPickup>().TakeKey();
                }
                // 2. NORMAL KAPI
                else if (hit.collider.CompareTag("Kapi"))
                {
                    interactUI.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E) && hasKey) hit.collider.GetComponentInParent<GateController>().OpenGate();
                }
                // 3. BOSS ANAHTARI
                else if (hit.collider.CompareTag("BossAnahtar"))
                {
                    interactUI.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E)) hit.collider.GetComponent<KeyPickup>().TakeKey();
                }
                // 4. BOSS KAPISI
                else if (hit.collider.CompareTag("BossKapi"))
                {
                    interactUI.SetActive(true);
                    if (Input.GetKeyDown(KeyCode.E) && hasBossKey) hit.collider.GetComponentInParent<GateController>().OpenGate();
                }
            }
        }
    }
}