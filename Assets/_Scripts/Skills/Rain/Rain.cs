using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YG;

public class Rain : MonoBehaviour
{
    public List<GameObject> gameObjects = new List<GameObject>();
    public GameObject umbrella;
    public GameObject light;
    public bool IsRain = true;
    public float Zone;
    public int damage;
    void Start()
    {
        Zone = 5.0f;
        damage = 1 * YandexGame.savesData.damageMod;
        IsRain = true;
    }

    void FixedUpdate()
    {
        if (IsRain)
        {
            foreach(GameObject go in gameObjects)
            {
                go.SetActive(true);
            }
            umbrella.SetActive(false);
            light.SetActive(false);
        }
        else
        {
            foreach (GameObject go in gameObjects)
            {
                go.SetActive(false);
            }
            umbrella.SetActive(true);
            light.SetActive(true);
        }
    }
}
