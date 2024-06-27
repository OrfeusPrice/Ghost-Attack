using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;
    public Rigidbody Rigidbody;
    public Animator anim;
    public Joystick joy;

    void FixedUpdate()
    {
        float H = joy.Horizontal;
        float V = joy.Vertical;

        Vector3 movement = new Vector3(H, 0, V);
        Vector3 new_pos = transform.position + movement * speed * Time.deltaTime;
        
        transform.position = new_pos;
        
        if (movement != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation,targetRotation,1000*Time.deltaTime);
        }

        
        //Rigidbody.velocity = new Vector3();
        //float h = Input.GetAxis("Horizontal");
        //float v = Input.GetAxis("Vertical");

        if (H != 0 || V != 0) anim.GetComponent<Animator>().SetBool("IsFly",true);
        else anim.GetComponent<Animator>().SetBool("IsFly", false);

        //Rigidbody.rotation = Quaternion.Euler(0, Rotate(h, v), 0);

        //Vector3 movePose = new Vector3(h, Rigidbody.velocity.y, v) * Speed;
        //Rigidbody.velocity = movePose;

        //if (gameObject.GetComponent<Player>().hp <= 0) anim.GetComponent<Animator>().SetBool("IsDead", true);
    }

    //public float Rotate(float h, float v)
    //{
    //    if (v > 0)
    //    {
    //        if (h > 0) return 45;
    //        if (h < 0) return -45f;
    //        return 0;
    //    }
    //    if (v < 0)
    //    {
    //        if (h == 1) return 135;
    //        if (h == -1) return -135;
    //        return 180;
    //    }
    //    if (h > 0) return 90;
    //    if (h < 0) return -90;
    //    return Rigidbody.rotation.eulerAngles.y;
    //}
}
