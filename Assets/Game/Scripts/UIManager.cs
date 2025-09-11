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
    [SerializeField] Sprite[] musicImages;
    [SerializeField] Sprite[] sfxImages;

    [Header("Win Canvas")]
    [SerializeField] Transform winCanvas;

    [Header("Beckground Canvas")]
    [SerializeField] Transform beckgroundImage;

    bool musicValue = true;
    bool sfxValue = true;

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
            StartCoroutine(Win());
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

    private IEnumerator Win()
    {
        yield return new WaitForSeconds(3);

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

    public void MusicVolume(Image image)
    {
        if (musicValue)
        {
            audioMixer.SetFloat("musicVolume", -80);
            image.sprite = musicImages[1];
        }
        else
        {
            audioMixer.SetFloat("musicVolume", 0);
            image.sprite = musicImages[0];
        }

        musicValue = !musicValue;
    }

    public void SFXVolume(Image image)
    {
        if (sfxValue)
        {
            audioMixer.SetFloat("sfxVolume", -80);
            image.sprite = sfxImages[1];
        }
        else
        {
            audioMixer.SetFloat("sfxVolume", 0);
            image.sprite = sfxImages[0];
        }

        sfxValue = !sfxValue;
    }
}
