using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public float speed;
    public Player player;
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();

        speed = player.fb_speed;

        transform.position = new Vector3(player.transform.position.x,
            player.transform.position.y + 0.5f,
            player.transform.position.z);
        GetComponent<Rigidbody>().velocity = player.transform.forward * speed;
        StartCoroutine(DeleteFB());
    }

    void FixedUpdate()
    {
    }

    IEnumerator DeleteFB()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            Destroy(this.gameObject);
        }
    }
}
