using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using YG;

public class Player : MonoBehaviour
{
    public int hp = 10;
    public Slider hpSlider;
    public TMP_Text textHp;

    public int xp;
    public int max_xp = 10;
    public Slider xpSlider;
    public TMP_Text textXp;
    public int lvl;

    public float SpawnFireball;
    public GameObject Fireball_pref;
    public int fb_damage;
    public int fb_Count;
    public float fb_speed;
    public bool isLive;

    public Spawn spawner;
    public GameObject Dead;
    public TMP_Text coinsText;
    void Start()
    {
        hp = YandexGame.savesData.lives;
        Dead.SetActive(false);
        hpSlider.maxValue = hp;
        hpSlider.minValue = 0;
        textHp.text = $"{hp}/{hpSlider.maxValue}";
        hpSlider.value = hp;

        xpSlider.maxValue = max_xp;
        xpSlider.minValue = 0;
        textXp.text = $"{xp}/{max_xp}";
        xpSlider.value = 0;
        xp = 0;

        lvl = 1;

        SpawnFireball = 0.5f;
        StartCoroutine(FireBall());
        fb_damage = 5 * YandexGame.savesData.damageMod;
        fb_speed = 5.0f;
        fb_Count = 1;

        isLive = true;
    }

    void FixedUpdate()
    {
        hpSlider.value = hp;
        textHp.text = $"{hp}/{hpSlider.maxValue}";

        textXp.text = $"{xp}/{max_xp}";
        xpSlider.value = xp;

        if (hp <= 0 && isLive) StartCoroutine(End());

        if (xp >= max_xp)
        {
            lvl += 1;
            xp = 0;
            max_xp *= 2;
            xpSlider.maxValue = max_xp;
            gameObject.GetComponent<Rain>().Zone += 0.2f;
            gameObject.GetComponent<Rain>().damage += 1;
            gameObject.GetComponent<Rain>().gameObjects[2].transform.localScale = new Vector3(gameObject.GetComponent<Rain>().Zone,
                gameObject.GetComponent<Rain>().gameObjects[2].transform.localScale.y,
                gameObject.GetComponent<Rain>().Zone);

            gameObject.GetComponent<Ghosts>().count += 4;

            fb_damage += 2;
            fb_speed += 0.2f;
            if (lvl % 3 == 0) fb_Count++;
        }
    }

    IEnumerator FireBall()
    {
        while (true)
        {
            for (int i = 0; i < fb_Count; i++)
            {
                Instantiate(Fireball_pref);
                yield return new WaitForSeconds(0.3f);
            }
            
            yield return new WaitForSeconds(SpawnFireball);
        }
    }

    IEnumerator End()
    {
        isLive = false;
        GetComponent<MouseMove>().anim.GetComponent<Animator>().SetBool("IsDead",true);
        GetComponent<MouseMove>().anim.GetComponent<Animator>().SetBool("IsFly", false);
        GetComponent<MouseMove>().enabled = false;
        
        yield return new WaitForSeconds(1.0f);
        spawner.StopAllCoroutines();
        //GetComponent<Ghosts>().StopAllCoroutines();
        //StopCoroutine(FireBall());
        foreach (var item in spawner.enemies) item.gameObject.SetActive(false);
        Dead.SetActive(true);
        coinsText.text = "+" + spawner.kills.ToString();
        YandexGame.savesData.coinsSum += spawner.kills;
        if (YandexGame.savesData.record < spawner.kills)
        {
            YandexGame.savesData.record = spawner.kills;
            YandexGame.NewLeaderboardScores("kills", YandexGame.savesData.record);
        }
        YandexGame.SaveProgress();
        yield return new WaitForSeconds(1.0f);
        spawner.End();
        //SceneManager.LoadScene(0);
    }

}
