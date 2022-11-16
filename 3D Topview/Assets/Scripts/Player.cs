using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Camera carmera;
    private Animator animator;
    private bool IsMove;
    private Vector3 destination;
    private float speed = 5f;
    private Vector3 Carmeraopos=new Vector3(0,15,-10);
    private float rotateSpeed = 10.0f;
    private Vector3 UnitPos;
    private Transform character;
    private void Awake()
    {
        carmera=Camera.main;
        animator=GetComponentInChildren<Animator>();
        character=GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
       
        if(Input.GetMouseButton(1))
        {
            RaycastHit hit;
            if(Physics.Raycast(carmera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                SetDestination(hit.point);
            }
        }
        Move();
        carmera.transform.position =transform.position+Carmeraopos;
    }
    private void SetDestination(Vector3 dest)
    {
        destination = dest;
        IsMove=true;
        animator.SetBool("IsMove", true);
    }
  
  
    private void Move()
    {
        UnitPos.x=transform.position.x;
        UnitPos.y = 0;
        UnitPos.z=transform.position.z;
        if (IsMove)
        {

            if (Vector3.Distance(destination, UnitPos) < 0.1f)
            {
                IsMove = false;
                animator.SetBool("IsMove", false);
                return;
            }
            var dir = destination - transform.position;
            dir.y = 0;
            Turn(dir);
            //character.transform.forward = dir;
            transform.position += dir.normalized * Time.deltaTime * speed;
        }
    }

    private void Turn(Vector3 pos )
    {
        Quaternion newrot = Quaternion.LookRotation(pos);
        character.rotation=Quaternion.Slerp(character.rotation,newrot,rotateSpeed*Time.deltaTime);
    }

}
