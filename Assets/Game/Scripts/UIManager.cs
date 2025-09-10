using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Player Info")]
    [SerializeField] TextMeshProUGUI woodAmountText;
    [SerializeField] TextMeshProUGUI xpAmountText;
    [SerializeField] Slider xpSlider;

    [Header("Settings")]
    [SerializeField] Transform settingsButton;
    [SerializeField] Transform settingsCanvas;

    [Header("Sound")]
    [SerializeField] AudioMixer audioMixer;

    [Header("Win Canvas")]
    [SerializeField] Transform winCanvas;

    [Header("Beckground Canvas")]
    [SerializeField] Transform beckgroundImage;

    public void SetPlayerUI(int xpMax)
    {
        woodAmountText.text = "0";
        xpAmountText.text = "XP: 0";
        xpSlider.maxValue = xpMax;
        xpSlider.value = 0;
    }

    public void UpdateWoodUI(int woodAmount)
    {
        woodAmountText.text = woodAmount.ToString();
    }

    public void UpdateXPSlider(int xp)
    {
        xpSlider.value = xp;
        xpAmountText.text = "XP: " + xp.ToString();
        if (xpSlider.value >= xpSlider.maxValue)
            Win();
    }

    public void OpenSettings()
    {
        settingsButton.gameObject.SetActive(false);
        woodAmountText.gameObject.SetActive(false);
        xpSlider?.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
        beckgroundImage.gameObject.SetActive(true);

        Time.timeScale = 0;
    }

    public void CloseSettings()
    {
        settingsButton.gameObject.SetActive(true);
        woodAmountText.gameObject.SetActive(true);
        xpSlider?.gameObject.SetActive(true);
        settingsCanvas.gameObject.SetActive(false);
        beckgroundImage.gameObject.SetActive(false);

        Time.timeScale = 1;
    }

    private void Win()
    {
        settingsButton.gameObject.SetActive(false);
        woodAmountText.gameObject.SetActive(false);
        xpSlider.gameObject.SetActive(false);
        winCanvas.gameObject.SetActive(true);
        beckgroundImage.gameObject.SetActive(true);

        Time.timeScale = 0;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("GameScene");
    }
}
