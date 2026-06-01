using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // YENÝ: Arayüz (UI) kütüphanesini koda dahil ettik

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    // YENÝ: Unity'den sürükleyip býrakacaðýmýz can barý deðiþkeni
    public Slider canBariUI;

    void Start()
    {
        currentHealth = maxHealth;

        // Oyun baþladýðýnda can barýnýn maksimum sýnýrýný ve mevcut doluluðunu 100 yap
        if (canBariUI != null)
        {
            canBariUI.maxValue = maxHealth;
            canBariUI.value = currentHealth;
        }
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Zombi vurdu! Kalan Can: " + currentHealth);

        // YENÝ: Hasar yediðinde can barýnýn görselini yeni can deðerine göre güncelle (Azalt)
        if (canBariUI != null)
        {
            canBariUI.value = currentHealth;
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Öldün! Oyun baþtan baþlýyor...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}