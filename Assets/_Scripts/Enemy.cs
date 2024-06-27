using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using YG;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public Spawn spawner;
    public bool canAttack = false;
    public AudioSource AS;
    public int max_hp = 10;
    public int hp = 10;
    public List<Material> materials = new List<Material>();
    public GameObject ghostMesh;
    Renderer render;
    public bool getDamageRain = true;
    //public bool getDamageFire = true;
    void Start()
    {
        spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawn>();
        hp = max_hp;
        render = ghostMesh.GetComponent<Renderer>();
        if (GameObject.FindWithTag("EnemySound") != null)
            AS = GameObject.FindWithTag("EnemySound").GetComponent<AudioSource>();
        Invoke("CanAttack", 1);
        GetComponent<NavMeshAgent>().destination = target.position;
    }
    private void FixedUpdate()
    {
        GetComponent<NavMeshAgent>().destination = target.position;

        if (hp <= max_hp * 0.7f)
            render.material = materials[0];
        if (hp <= max_hp * 0.4f)
            render.material = materials[1];
        if (hp <= max_hp * 0.1f)
            render.material = materials[2];

        if (GetComponent<NavMeshAgent>().remainingDistance < 0.5f && canAttack &&
                Mathf.Abs(target.transform.position.x - transform.position.x) <= 1.5f &&
                Mathf.Abs(target.transform.position.z - transform.position.z) <= 1.5f)
        {
            target.GetComponent<Player>().hp -= 1;
            Destroy(this.gameObject);
        }

        if (hp <= 0)
            Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        if (AS != null)
            AS.PlayOneShot(AS.clip);
        spawner.enemies.Remove(this);
        if (target != null)
            target.GetComponent<Player>().xp += 1 * YandexGame.savesData.XPmod;
        spawner.kills += 1;
    }
    public void CanAttack()
    {
        canAttack = true;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Zone" && getDamageRain)
        {
            hp -= target.gameObject.GetComponent<Rain>().damage;
            StartCoroutine(Rain());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "FireBall")
        {
            hp -= target.gameObject.GetComponent<Player>().fb_damage;
            //Destroy(this.gameObject);


            //StartCoroutine(Fire());
        }
    }

    IEnumerator Rain()
    {
        getDamageRain = false;
        yield return new WaitForSeconds(0.5f);
        getDamageRain = true;
        StopCoroutine(Rain());
    }
    //IEnumerator Fire()
    //{
    //    getDamageFire = false;
    //    yield return new WaitForSeconds(0.02f);
    //    getDamageFire = true;
    //    StopCoroutine(Fire());
    //}
}
