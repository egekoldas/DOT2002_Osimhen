using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel; // Hazýrladýðýn Pause Panelini buraya sürükle
    public static bool isPaused = false;

    void Update()
    {
        // ESC tuþuna basýnca menüyü aç/kapat
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }
    }

    // Durdurma Butonuna basýnca çalýþacak
    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Zamaný durdur! (Zombiler ve dünya donar)
        isPaused = true;

        // Fareyi göster
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        // TEKNÝK ZORUNLULUK: Oyuncunun buraya kadar geldiðini hafýzaya kaydet
        PlayerPrefs.SetInt("SonKalýnanBolum", 1);
        PlayerPrefs.Save();
    }

    // Resume (Devam) veya Çarpý butonuna basýnca çalýþacak
    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Zamaný devam ettir
        isPaused = false;

        // Fareyi tekrar oyun için gizle
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Quit (Menüye Dön) butonuna basýnca
    public void QuitToMenu()
    {
        Time.timeScale = 1f; // Zamaný düzeltmeyi unutma!
        SceneManager.LoadScene(0); // Menü sahnesine dön
    }
}