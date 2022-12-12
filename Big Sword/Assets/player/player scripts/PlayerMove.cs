using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class PlayerMove : MonoBehaviour
{
    [Header("Objects")]

    Animator anim;
    public Transform Cam;
    public GameObject StaminaObj;
    CharacterController controller;


    [Header("Floats")]

    public float MaxStamina;
    public float DexMod;
    public float Speed; // players current speed
    public float PLspeed, SRspeed; // player speed(walking), Sprint speed
    public float RP; //roll power/direction/direction sides
    public float stamina;

    private float rollTime;
    private float turnSmoothTime = 0.1f;
    private float turnSmoothVel;


    [Header("Bools")]
    public bool sprint = false;
    private bool moving;
    public bool amIGrounded;
    private bool idle;
    public bool roll, rollstart;
    public bool canwalk;
    public bool STdrain;
    public bool canSpDoOB;
    public bool StaminaHolt;

    private bool SPreset; // Reset players current speed to PLspeed (waling)

    Vector3 dodgedir;

    void Start()
    {
        StaminaObj = GameObject.Find("StaminaObj");


        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        rollstart = false;
        canSpDoOB = true;
        canwalk = true;
        MaxStamina = 100f;
        DexMod = 30f;
    }

 
    void FixedUpdate()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(Horizontal, 0f, Vertical);
        Vector3 gravity = new Vector3(0f, -8f, 0f);
       

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothTime);
        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

        controller.SimpleMove(gravity);


        if ((direction.magnitude >= 0.1f)&&(canwalk == true))
            {      
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                controller.SimpleMove(moveDir.normalized * Speed);
                moving = true;idle = false; 
            } else { moving = false;  idle = true;  }

        if ((Input.GetKey(KeyCode.JoystickButton4) && (amIGrounded == true)&&(canSpDoOB==true)&& (stamina >= 5f))) { sprint = true; }           // sprint
        if ((Input.GetKeyUp(KeyCode.JoystickButton4)) && (canSpDoOB==true)) { sprint = false; }
        if (sprint == true) { Speed = SRspeed; SPreset = false;  STdrain = true;}
        if (sprint == false) { SPreset = true;  STdrain = false;}
        if (stamina<=1) { sprint = false; }
        if  (moving==false) { sprint = false; }

        if ((Input.GetKey(KeyCode.JoystickButton1)) && (amIGrounded == true) && (canSpDoOB==true) && (stamina >= 5f)) { roll = true; canSpDoOB = false; }                          // roll
        if (roll == true){ rollstart = true; rollTime = 0f; canwalk = false; stamina -= 15; dodgedir = moveDir; StaminaHolt = true; }
        if (rollstart == true) { roll = false; rollTime += Time.deltaTime; }
        if ((rollTime > 0.1f) && (rollTime < 0.6f))
        { 
            controller.SimpleMove(dodgedir * RP);
            float dodgeAngle = Mathf.Atan2(dodgedir.x, dodgedir.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, dodgeAngle, 0f);
        }
        if ((rollTime < 0.5f)&&(rollTime>0f)) { anim.SetBool("Roll", true); anim.SetBool("Sprint", false); anim.SetBool("Walk", false); anim.SetBool("Idle", false); }
        if (rollTime > 0.5f) { anim.SetBool("Roll", false);}
        if (rollTime>= 0.6f) { rollstart = false; roll = false;  rollTime = 0f; canwalk = true; StaminaHolt = false; canSpDoOB = true; }      
        
        if (SPreset == true) { Speed = PLspeed; }                                                  // player speed reset

 

                                               
        if ((moving == true) && (sprint==false)) { anim.SetBool("Walk", true); } else { anim.SetBool("Walk", false); }                                // anim triggers
        if (idle == true) { anim.SetBool("Idle",true); } else { anim.SetBool("Idle", false); }
        if ((sprint==true) && (moving == true)) { anim.SetBool("Sprint", true); } else { anim.SetBool("Sprint", false); }
        //if ((GetComponentInChildren<PlayerCombat>().healT >0f)&&((GetComponentInChildren<PlayerCombat>().healT < 0.5f))) { anim.SetBool("Heal", true); } else { anim.SetBool("Heal", false); }


        if ((STdrain==true)&&(stamina>=0f)) { stamina -= Time.deltaTime*(20f-(DexMod/3f)); }                                              //stamina
        if ((STdrain==false)&&(stamina<=MaxStamina)&&(StaminaHolt==false)) { stamina += Time.deltaTime*(6f+DexMod/2f); }

        if ((stamina <= MaxStamina)&&(stamina >= 0))
        {
            StaminaObj.GetComponentInParent<RectTransform>().localScale = new Vector3((stamina / 15), 1, 1);
            StaminaObj.GetComponentInParent<RectTransform>().anchoredPosition = new Vector2(15f + stamina / 1.5f, 0);
        } 



        if (controller.isGrounded) { amIGrounded = true; } else { amIGrounded = false; } // ground check

        if ((controller.isGrounded)&&(controller.velocity.y<=-10f)) { GetComponentInChildren<PlayerCombat>().HP += controller.velocity.y * 3; }
       // Debug.Log(controller.velocity.y);
    }
}
