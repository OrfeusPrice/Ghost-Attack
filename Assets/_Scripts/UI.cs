using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class UI : MonoBehaviour
{
    public GameObject S_on;
    public GameObject S_off;
    public GameObject M_on;
    public GameObject M_off;

    public GameObject Sound;
    public GameObject Music;

    public GameObject Player;
    public GameObject joystic;

    public bool soundIs = true;
    public bool musicIs = true;

    private void Awake()
    {
        if (Player != null)
            if (YandexGame.EnvironmentData.isMobile)
            {
                Player.GetComponent<Move>().enabled = true;
                Player.GetComponent<MouseMove>().enabled = false;
                joystic.SetActive(true);
            }
            else
            {
                Player.GetComponent<Move>().enabled = false;
                Player.GetComponent<MouseMove>().enabled = true;
                joystic.SetActive(false);
            }

        soundIs = YandexGame.savesData.soundIs;
        musicIs = YandexGame.savesData.musicIs;
    }

    void Start()
    {
        if (Sound != null && Music != null)
        {
            if (musicIs)
            {
                M_on.SetActive(true);
                M_off.SetActive(false);
                Music.SetActive(true);
            }
            else
            {
                M_on.SetActive(false);
                M_off.SetActive(true);
                Music.SetActive(false);
            }

            if (soundIs)
            {
                S_on.SetActive(true);
                S_off.SetActive(false);
                Sound.SetActive(true);
            }
            else
            {
                S_on.SetActive(false);
                S_off.SetActive(true);
                Sound.SetActive(false);
            }
        }
    }

    void Update()
    {

    }

    public void SoundBut()
    {
        S_on.SetActive(!S_on.active);
        S_off.SetActive(!S_off.active);
        Sound.SetActive(!Sound.active);
        soundIs = !soundIs;
        YandexGame.savesData.soundIs = soundIs;
        YandexGame.SaveProgress();
    }

    public void MusicBut()
    {
        M_on.SetActive(!M_on.active);
        M_off.SetActive(!M_off.active);
        Music.SetActive(!Music.active);
        musicIs = !musicIs;
        YandexGame.savesData.musicIs = musicIs;
        YandexGame.SaveProgress();
    }

    public void ExitBut()
    {
        SceneManager.LoadScene(0);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }



}
