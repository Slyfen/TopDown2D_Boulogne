using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSM : MonoBehaviour
{
    [SerializeField] AnimationClip rollClip;
    [SerializeField] Animator animator;
    [SerializeField] float speed = 5f;
    [SerializeField] float sprintSpeed = 10f;
    [SerializeField] float rollSpeed = 10f;

    Vector2 dirInput;
    Vector2 rollDirection;


    Rigidbody2D rb2D;

    public enum PlayerState
    {
        IDLE,
        RUN,
        SPRINT,
        ROLL
    }

    public PlayerState currentState;


    // Start is called before the first frame update
    void Start()
    {
        currentState = PlayerState.IDLE;
        rb2D = GetComponent<Rigidbody2D>();
        OnStateEnter();
    }

    // Update is called once per frame
    void Update()
    {
        dirInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(dirInput != Vector2.zero && currentState != PlayerState.ROLL)
        {
            animator.SetFloat("InputX", dirInput.x);
            animator.SetFloat("InputY", dirInput.y);

        }

        OnStateUpdate();
    }

    private void FixedUpdate()
    {
        OnStateFixedUpdate();
    }


    void OnStateEnter()
    {
        switch (currentState)
        {
            case PlayerState.IDLE:
                animator.SetBool("IDLE", true);
                rb2D.velocity = Vector2.zero;
                break;
            case PlayerState.RUN:
                animator.SetBool("RUN", true);
                break;
            case PlayerState.SPRINT:
                animator.SetBool("SPRINT", true);
                break;

            case PlayerState.ROLL:
                animator.SetTrigger("ROLL");
                rollDirection = new Vector2(animator.GetFloat("InputX"), animator.GetFloat("InputY"));
                StartCoroutine(WaitForRoll());
                break;


            default:
                break;
        }
    }

    void OnStateUpdate()
    {

        switch (currentState)
        {
            

            case PlayerState.IDLE:

                // TO RUN OR SPRINT
                if (dirInput.magnitude != 0)
                {
                    TransitionToState(Input.GetKey(KeyCode.LeftShift) ? PlayerState.SPRINT :  PlayerState.RUN);

                }

                // TO ROLL
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    TransitionToState(PlayerState.ROLL);
                }

                break;


            case PlayerState.RUN:

                // TO IDLE
                if (dirInput.magnitude == 0)
                {
                    TransitionToState(PlayerState.IDLE);

                }

                // TO SPRINT
                if(Input.GetKeyDown(KeyCode.LeftShift))
                {
                    TransitionToState(PlayerState.SPRINT);
                }

                // TO ROLL
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    TransitionToState(PlayerState.ROLL);
                }


                break;

            case PlayerState.SPRINT:

                // TO IDLE
                if (dirInput.magnitude == 0)
                {
                    TransitionToState(PlayerState.IDLE);
                    break;
                }

                // TO SPRINT
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    TransitionToState(PlayerState.RUN);
                    break;
                }

                // TO ROLL
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    TransitionToState(PlayerState.ROLL);
                }


                break;

            case PlayerState.ROLL:

                
                break;

            default:
                break;
        }

    }

    void OnStateFixedUpdate()
    {
        switch (currentState)
        {
            case PlayerState.IDLE:
                
                break;
            case PlayerState.RUN:
                rb2D.velocity = dirInput.normalized * speed;
                break;

            case PlayerState.SPRINT:
                rb2D.velocity = dirInput.normalized * sprintSpeed;

                break;


            case PlayerState.ROLL:
                rb2D.velocity = rollDirection.normalized * rollSpeed;
                break;


            default:
                break;
        }
    }

    void OnStateExit()
    {
        switch (currentState)
        {
            case PlayerState.IDLE:
                animator.SetBool("IDLE", false);
                break;
            case PlayerState.RUN:
                animator.SetBool("RUN", false);
                break;

            case PlayerState.SPRINT:
                animator.SetBool("SPRINT", false);
                break;

            case PlayerState.ROLL:
                break;

            default:
                break;
        }
    }

    void TransitionToState(PlayerState nextState)
    {
        OnStateExit();
        currentState = nextState;
        OnStateEnter();
    }


    IEnumerator WaitForRoll() 
    {

        yield return new WaitForSeconds(rollClip.length);
        TransitionToState(PlayerState.IDLE);
    
    }

}
