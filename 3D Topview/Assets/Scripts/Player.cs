using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Character move
    private Camera carmera;
    private Animator animator;
    private bool IsMove;
    private Vector3 destination;
    private float speed = 5f;
    private Vector3 Carmeraopos = new Vector3(0, 15, -10);
    private float rotateSpeed = 10.0f;
    private Vector3 UnitPos;
    private Transform character;
    private Transform playerswaptr;
    private Transform playerobj;

    #endregion
    private bool grace = false;
    public enum States
    {
        Create,
        Idle,
        Battle,
        Attack,
        Death
    }

    public States myState;
    private void Awake()
    {
        carmera = Camera.main;
        animator = GetComponentInChildren<Animator>();
        character = GetComponentInChildren<Transform>();
        ChangeState(States.Idle);
        playerswaptr =GetComponentInChildren<Transform>();
        playerobj = GetComponentInChildren<Transform>();
    }

    private void Update()
    {
        StateProcess();
        if (Input.GetMouseButton(1)&&animator.GetBool("IsRoll")==false)
        {
            RaycastHit hit;
            if (Physics.Raycast(carmera.ScreenPointToRay(Input.mousePosition), out hit))
            {
                SetDestination(hit.point);
             
                
            }
          
        }
        Move();
        if(Input.GetKeyDown(KeyCode.Space) && animator.GetBool("IsRoll") == false)
        {
            IsMove = false;
            animator.SetBool("IsMove", false);
            animator.SetTrigger("Roll");
            StartCoroutine(Roll(0.3f));
           
        }
    }

    void ChangeState(States s)
    {
        if (myState == s) return;
       
        switch(myState)
        {
            case States.Create:
                CreateExit();
                break;
            case States.Idle:
                IdleExit();
                break;
            case States.Battle:
                BattleExit();
                break;
            case States.Attack:
                AttackExit();
                break;
            case States.Death:
                DeathExit();    
                break;

        }

        myState = s;

        switch (myState)
        {
            case States.Create:
                CreateEnter();
                break;
            case States.Idle:
                IdleEnter();
                break;
            case States.Battle:
                BattleEnter();
                break;
            case States.Attack:
                AttackEnter();
                break;
            case States.Death:
                DeathEnter();
                break;

        }

    }
    void StateProcess()
    {
        switch(myState)
        {
            case States.Create:
                Create();
                break;
            case States.Idle:
                Idle();
                break;
            case States.Battle:
                Battle();
                break;
            case States.Attack:
                Attack();   
                break;
            case States.Death:
                Death();
                break;
        }

    }

    IEnumerator ChangeIdle()
    {
        var WaitTime = 5f;
        while(myState==States.Battle)
        {
            if(Input.GetMouseButtonDown(1))
            {
                ChangeState(States.Attack); 
            }
            yield return new WaitForSeconds(1f);
            WaitTime--;

            if(WaitTime<0)
            {
                ChangeState(States.Idle);
            }
        }
    }

    #region Character Move ÇÔ¼ö
    public void SetDestination(Vector3 dest)
    {
        destination = dest;
        IsMove = true;
        animator.SetBool("IsMove", true);
    }

    IEnumerator Roll(float time)
    {
        playerswaptr.transform.position = playerobj.transform.position;
        playerswaptr.transform.rotation = playerobj.transform.rotation;
        yield return new WaitForSeconds(0.3f);

        grace = true;
        yield return new WaitForSeconds(time);
        animator.SetTrigger("EndRoll");
        grace = false;
        playerobj.transform.position=playerswaptr.transform.position;
        playerobj.transform.rotation=playerswaptr.transform.rotation;   
    }
    public void Move()
    {
       
        
        UnitPos.x = transform.position.x;
        UnitPos.y = 0;
        UnitPos.z = transform.position.z;
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
            transform.position += dir.normalized * Time.deltaTime * speed;
        }
        carmera.transform.position = transform.position + Carmeraopos;

    }

    private void Turn(Vector3 pos)
    {
        Quaternion newrot = Quaternion.LookRotation(pos);
        character.rotation = Quaternion.Slerp(character.rotation, newrot, rotateSpeed * Time.deltaTime);
    }
    #endregion

    #region StateCreate
    private void CreateEnter()
    {
        ChangeState(States.Idle);
    }
    private void Create()
    {

    }
    private void CreateExit()
    { 

    }
    #endregion

    #region StateIdle
    private void IdleEnter()
    {

    }
    private void Idle()
    {
       
        if(Input.GetMouseButton(0))
        {
            ChangeState(States.Battle);
        }
    }
    private void IdleExit()
    {

    }
    #endregion

    #region StateBattle
    private void BattleEnter()
    {
        StartCoroutine(ChangeIdle());
    }
    private void Battle()
    {
      
    }
    private void BattleExit()
    {

    }
    #endregion

    #region StateAttack
    private void AttackEnter()
    {

    }
    private void Attack()
    {

    }
    private void AttackExit()
    {

    }
    #endregion

    #region StateDeath
    private void DeathEnter()
    {

    }
    private void Death()
    {

    }
    private void DeathExit()
    {

    }
    #endregion
}






