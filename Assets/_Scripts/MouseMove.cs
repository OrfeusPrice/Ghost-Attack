using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public bool isMoving = false;
    private RaycastHit hitInfo;
    private Quaternion targetRotation;
    public Animator anim;
    int layerMaskOnlyPlayer;
    int layerMaskOnlyEnemy;
    int layerMaskOnlyIgnorePlayer;
    int layerMaskOnlyWall;
    int mask;


    private void Start()
    {
        layerMaskOnlyPlayer = 1 << LayerMask.NameToLayer("Player");
        layerMaskOnlyEnemy = 1 << LayerMask.NameToLayer("Enemy");
        layerMaskOnlyIgnorePlayer = 1 << LayerMask.NameToLayer("PlayerIgnore");
        layerMaskOnlyWall = 1 << LayerMask.NameToLayer("Wall");
        mask = ~layerMaskOnlyPlayer & ~layerMaskOnlyEnemy & ~layerMaskOnlyIgnorePlayer & ~layerMaskOnlyWall;
    }

    void FixedUpdate()
    {
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hitInfo, Mathf.Infinity, mask))
                {
                    isMoving = true;
                    targetRotation = Quaternion.LookRotation(hitInfo.point - transform.position, Vector3.up);
                }
            }

            if (isMoving)
            {
                anim.GetComponent<Animator>().SetBool("IsFly", true);
                Vector3 targetPosition = hitInfo.point;
                Vector3 currentPosition = transform.position;
                targetPosition = new Vector3(targetPosition.x, currentPosition.y, targetPosition.z);
                float speed = 5f;
                float rotationSpeed = 1000f;

                transform.position = Vector3.MoveTowards(currentPosition, targetPosition, speed * Time.deltaTime);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed);

                if (transform.position == targetPosition)
                {
                    isMoving = false;
                    anim.GetComponent<Animator>().SetBool("IsFly", false);
                }
            }

            if (!Input.GetMouseButton(0))
            {
                isMoving = false;
                anim.GetComponent<Animator>().SetBool("IsFly", false);
            }
        }
    }
}
