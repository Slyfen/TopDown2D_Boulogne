using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LemmingSM : MonoBehaviour
{
    public enum LemmingState
    {
        IDLE,
        FALLING,
        WALK_RIGHT,
        WALK_LEFT,
        STOP

    }

    public LemmingState currentState;

    [SerializeField] Color idleColor;
    [SerializeField] Color fallingColor;
    [SerializeField] Color walkRColor;
    [SerializeField] Color walkLColor;
    [SerializeField] Color stopColor;


    Rigidbody2D rb2d;
    SpriteRenderer sr;

    bool right = true;


    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        currentState = LemmingState.IDLE;

        OnStateEnter();

    }

    // Update is called once per frame
    void Update()
    {
        OnStateUpdate();
    }

    // QUAND JE RENTRE DANS UN ETAT
    void OnStateEnter()
    {
        switch (currentState)
        {
            case LemmingState.IDLE:
                sr.color = idleColor;
                break;
            case LemmingState.FALLING:
                sr.color = fallingColor;
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
                break;
            case LemmingState.WALK_RIGHT:
                sr.color = walkRColor;
                
                break;
            case LemmingState.WALK_LEFT:
                sr.color = walkLColor;
                
                break;
            case LemmingState.STOP:
                sr.color = stopColor;
                rb2d.velocity = new Vector2(0f, 0);
                gameObject.layer = 7;
                rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
                break;
            default:
                break;
        }
    }
    
    // QUAND JE SUIS DANS UN ETAT
    void OnStateUpdate()
    {
        switch (currentState)
        {
            case LemmingState.IDLE:

                // TO FALLING
                if(rb2d.velocity.y < 0)
                {
                    TransitionToState(LemmingState.FALLING);
                }

                // TO WALK RIGHT
                if(rb2d.velocity.y == 0)
                {
                    TransitionToState(LemmingState.WALK_RIGHT); 
                }

                break;


            case LemmingState.FALLING:

                


                // TO WALK RIGHT
                if(rb2d.velocity.y == 0)
                {
                    TransitionToState(right ? LemmingState.WALK_RIGHT : LemmingState.WALK_LEFT);
                }


                break;

            case LemmingState.WALK_RIGHT:

                rb2d.velocity = new Vector2(3f, rb2d.velocity.y);

                // TO FALLING
                if (rb2d.velocity.y < 0)
                {
                    TransitionToState(LemmingState.FALLING);
                }

                // TO STOP WITH RAYCAST
                //if (Input.GetMouseButtonDown(0))
                //{
                //    RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));

                //    if (hit.collider.gameObject == this.gameObject)
                //    {
                //        TransitionToState(LemmingState.STOP);
                //    }

                //}

                break;


            case LemmingState.WALK_LEFT:
                rb2d.velocity = new Vector2(-3f, rb2d.velocity.y);

                // TO FALLING
                if (rb2d.velocity.y < 0)
                {
                    TransitionToState(LemmingState.FALLING);
                }
                break;

            case LemmingState.STOP:
                
                break;


            default:
                break;
        }


    }

    // QUAND JE QUITTE UN ETAT
    void OnStateExit()
    {
        switch (currentState)
        {
            case LemmingState.IDLE:
                break;
            case LemmingState.FALLING:
                break;
            case LemmingState.WALK_RIGHT:
                break;
            case LemmingState.WALK_LEFT:
                break;
            case LemmingState.STOP:
                gameObject.layer = 6;
                rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
                break;
            default:
                break;
        }
    }

    private void OnMouseDown()
    {
        //SWITCH
        switch (currentState)
        {
            case LemmingState.IDLE:
                break;
            case LemmingState.FALLING:
                break;
            case LemmingState.WALK_RIGHT:
            case LemmingState.WALK_LEFT:

                //if (right)
                //    right = false;

                //else
                //    right = true;



                TransitionToState(LemmingState.STOP);


                break;





            case LemmingState.STOP:

                right = !right;

                //if(right)
                //{
                //    TransitionToState(LemmingState.WALK_RIGHT);

                //} else
                //{
                //    TransitionToState(LemmingState.WALK_LEFT);
                //}

                TransitionToState(right ? LemmingState.WALK_RIGHT : LemmingState.WALK_LEFT);
                
               


                break;
            default:
                break;
        }
        //SI ON EST DANS L'ETAT WALK R OU WALK L

        //ON VA DANS L'ETAT STOP
    }

    // POUR PASSER D'UN ETAT A UN AUTRE
    void TransitionToState(LemmingState nextState)
    {
        // JE QUITTE MON ETAT ACTUEL
        OnStateExit();

        // CHANGER MON ETAT ACTUEL
        currentState = nextState;

        // JE RENTRE DANS MON NOUVEL ETAT
        OnStateEnter();

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Lemming")
        {
            switch (currentState)
            {
                case LemmingState.IDLE:
                    break;
                case LemmingState.FALLING:
                    break;
                case LemmingState.WALK_RIGHT:
                    right = false;
                    TransitionToState(LemmingState.WALK_LEFT);
                    break;
                case LemmingState.WALK_LEFT:
                    right = true;
                    TransitionToState(LemmingState.WALK_RIGHT);
                    break;
                case LemmingState.STOP:
                    break;
                default:
                    break;
            }
        }
    }


}
