using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Ghost : MonoBehaviour
{
    public Spawn spawner;
    public Enemy cur_enemy;
    public int enemy_ind = 0;
    public Player player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawn>();
        //if (spawner.enemies.Count > 0)
        //{
        //    enemy_ind = Random.Range(0, spawner.enemies.Count);
        //    cur_enemy = spawner.enemies[enemy_ind];
        //    cur_enemy.ghosts.Add(this);
        //    GetComponent<NavMeshAgent>().destination = cur_enemy.transform.position;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (spawner.enemies.Count > 0)
        {
            if (cur_enemy == null)
            {
                enemy_ind = Random.Range(0, spawner.enemies.Count);
                cur_enemy = spawner.enemies[enemy_ind];
            }
            if (cur_enemy != null)
                GetComponent<NavMeshAgent>().destination = cur_enemy.transform.position;

            if (GetComponent<NavMeshAgent>().remainingDistance < 1 &&
                cur_enemy != null && cur_enemy.gameObject.active == true &&
                Mathf.Abs(cur_enemy.transform.position.x - transform.position.x) <= 1.5f &&
                Mathf.Abs(cur_enemy.transform.position.z - transform.position.z) <= 1.5f)
            {
                spawner.enemies.Remove(cur_enemy);
                Destroy(this.gameObject);
                Destroy(cur_enemy.gameObject);
            }
        }
        else GetComponent<NavMeshAgent>().destination = player.gameObject.transform.position;
    }
}
