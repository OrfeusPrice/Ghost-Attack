using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using YG;

public class Spawn : MonoBehaviour
{
    public List<Enemy> prefs = new List<Enemy>();
    public List<Enemy> enemies = new List<Enemy>();
    public Player player;
    public int enemy_hp = 10;
    public int SpawnRate;

    public int sec;
    public int min;
    public int hr;
    public TMP_Text textTime;

    public int kills;
    public TMP_Text textKills;

    void Start()
    {
        SpawnRate = 1;
        StartCoroutine(Spawner());


        sec = 0;
        min = 0;
        hr = 0;
        StartCoroutine(Timer());

        kills = 0;
    }

    void FixedUpdate()
    {
        textKills.text = kills.ToString();
    }

    public void End()
    {
        player.gameObject.SetActive(false);
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            if (enemies.Count < 1000)
            for (int i = 0; i < SpawnRate; i++)
            {
                float randx = 0;
                float randz = 0;
                while (randx == 0)
                {
                    randx = Random.Range(-1, 2);
                }
                randx *= Random.Range(15.0f, 20.0f);
                while (randz == 0)
                {
                    randz = Random.Range(-1, 2);
                }
                randz *= Random.Range(15.0f, 20.0f);

                Enemy new_enemy = Instantiate(prefs[Random.Range(0, prefs.Count)], new Vector3(player.transform.position.x + randx, player.transform.position.y, player.transform.position.z + randz), Quaternion.identity);
                new_enemy.target = player.transform;
                new_enemy.max_hp = enemy_hp;
                enemies.Add(new_enemy);
            }
            yield return new WaitForSeconds(0.5f);
        }
    }


    IEnumerator Timer()
    {
        while (true)
        {
            sec++;
            if (sec >= 60)
            {
                min++;
                enemy_hp += Mathf.RoundToInt(enemy_hp * 0.5f);
                sec = 0;
                SpawnRate++;
                player.gameObject.GetComponent<Rain>().IsRain = !player.gameObject.GetComponent<Rain>().IsRain;
                if (min >= 60)
                {
                    hr++;
                    min = 0;
                }
            }
            textTime.text = $"{hr}:{min}:{sec}";
            yield return new WaitForSeconds(1);
        }
    }
}
