using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ghosts : MonoBehaviour
{
    public GameObject pref;
    public int count = 2;

    private void Start()
    {
        count = 4;
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            StartCoroutine (SpawnGhost());
            yield return new WaitForSeconds(10);
        }
    }

    IEnumerator SpawnGhost()
    {
        int i = 0;
        while (i <= count)
        {
            GameObject new_g = Instantiate(pref, new Vector3(transform.position.x + Random.Range(-3.0f,3.0f), transform.position.y, transform.position.z + Random.Range(-3.0f, 3.0f)), Quaternion.identity);
            i++;
            yield return new WaitForSeconds(0.1f);
        }
        StopCoroutine(SpawnGhost());
    }
}
