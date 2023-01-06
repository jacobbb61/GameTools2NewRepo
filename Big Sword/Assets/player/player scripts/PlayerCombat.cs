using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerCombat : MonoBehaviour
{
    public Object RespawnScene;
    public Animator YouDiedAnim;
    Animator anim;
    public AudioSource PlayerSource;
    public AudioClip Death1, Death2, Death3;
    public AudioClip Swipe;
 
    public Transform player; 
    GameObject HPObj;
    public TextMeshProUGUI FlaskNum;

    private int num = 1;

    public float MaxHP, ATK1DMG, ATK2DMG;
    public float HP;
    public float  atk1Time, atk2Time, atk3Time, atk4Time, atk5Time, healT;

    public int flasks;
    public int MaxFlask;

    private float EnemyDMG, EnemyDMG2;//SlugDMG,

    private float youDiedTime = 0f;
    public bool youDied = false;

    public bool heal, attack2charge, attack2release;
    private bool attack1, attack3, attack4, attack5,timestart1, timestart3, timestart4, timestart5;

    public bool CD = true;
   
   
    void Start()
    {
        //anim = GetComponentInParentInChildren<Animator>();
        anim = GetComponentInParent<Animator>();
        HPObj = GameObject.Find("HPObj");


        CD = true;


        EnemyDMG = 6f;
        EnemyDMG2 = 12f;
        flasks = MaxFlask;
    }


    void Update()
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////HEALTH BAR
        if (HP > 0)
        {
            HPObj.GetComponentInParent<RectTransform>().localScale = new Vector3((HP / 15), 1, 1);
            HPObj.GetComponentInParent<RectTransform>().anchoredPosition = new Vector2(15f + HP / 1.5f, 10);
        }
        else
        {
            HPObj.GetComponentInParent<RectTransform>().localScale = new Vector3(1, 1, 1);
            HPObj.GetComponentInParent<RectTransform>().anchoredPosition = new Vector2(0, 10);
            youDied = true;
        }
        if (youDied) { youDiedTime += Time.deltaTime; YouDiedAnim.SetTrigger("Active"); player.GetComponent<PlayerMove>().enabled = false; CD = false;}
        if (youDiedTime >= 4.25f) { SceneManager.LoadScene("RespawnScene"); }
        if (youDiedTime >= 0.1f && youDiedTime <= 0.25f) { anim.SetTrigger("Stun"); }

        if (atk1Time > 0f || atk2Time > 0f || atk3Time > 0f || atk4Time > 0f || atk5Time>0f ) { GetComponentInParent<PlayerMove>().turnSmoothTime = 0.3f; } else { GetComponentInParent<PlayerMove>().turnSmoothTime = 0.1f; }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////RIGHT TRIGGER ATTACK
       

        if ((CD == true) && (attack1 == false) && (atk1Time==0) && (GetComponentInParent<PlayerMove>().stamina >= 10f)) //start attack
        {
            if (Input.GetAxis("RT") == 1f && (CD==true)) { attack1 = true; CD = false;           }
        }

        if (attack1 == true) //attack
        {
            CD = false;
            timestart1 = true;
            atk1Time = 0f;
            PlayerSource.clip = Swipe; PlayerSource.Play();
            anim.SetBool("HeavyAttack", true);
            GetComponentInParent<PlayerMove>().StaminaHolt = true;
            GetComponentInParent<PlayerMove>().canSpDoOB = false;
            GetComponentInParent<PlayerMove>().stamina -= 10f; 
            attack1 = false;
        }

        if (timestart1 == true) { atk1Time += Time.deltaTime; attack1 = false; GetComponentInParent<PlayerMove>().Speed = 0.1f; }

        if ((atk1Time > 0f) && (atk1Time < 0.01f) && (timestart1==true)) {}

        if ((atk1Time >= 0.8f) && (atk1Time <= 1.2f)) { anim.SetBool("HeavyAttack", false); GetComponentInParent<PlayerMove>().canSpDoOB = true; }

        if ((atk1Time > 0.8f) && (Input.GetKey(KeyCode.JoystickButton1)))
        {
            timestart1 = false; CD = true; atk1Time = 0f;
            GetComponentInParent<PlayerMove>().StaminaHolt = false;
        }

        if (atk1Time >= 1.25f)
        {
            timestart1 = false; 
            CD = true; 
            atk1Time = 0f;
            GetComponentInParent<PlayerMove>().StaminaHolt = false;
            attack1 = false;
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////LEFT TRIGGER ATTACK    

        if ((CD == true) && (atk2Time == 0f) && (attack2charge==false) && (GetComponentInParent<PlayerMove>().stamina >= 30f)) //first charge
        {
            if ((Input.GetAxis("LT")) > 0f)
            {
                attack2charge = true; CD = false;
                GetComponentInParent<PlayerMove>().StaminaHolt = true;
                GetComponentInParent<PlayerMove>().canwalk = false;
                GetComponentInParent<PlayerMove>().canSpDoOB = false;
            }
        }

        if (attack2charge == true)  // in charge
        {
           
            atk2Time += Time.deltaTime; CD = false;
            anim.SetBool("SpecialAttackCharge", true); 
        }

        if (((Input.GetAxis("LT")) == 0f) && (atk2Time < 0.5f) && (CD == false)) //release early
        { 
            atk2Time = 0f; 
            attack2charge = false; 
            anim.SetBool("SpecialAttackCharge", false); 
            CD = true;
            GetComponentInParent<PlayerMove>().canwalk = true;
            GetComponentInParent<PlayerMove>().canSpDoOB = true;
            GetComponentInParent<PlayerMove>().StaminaHolt = false;
        }
        
        if (((Input.GetAxis("LT")) == 0f) && (atk2Time >= 0.5f) && (attack2charge == true) && (CD == false)) //release on time
        {
            anim.SetBool("SpecialAttackRelease", true);
            attack2release = true; 
            atk2Time = 0.5f; 
            attack2charge = false; 
            GetComponentInParent<PlayerMove>().stamina -= 30f;
        }

        if (attack2release == true) //attack
        {
            atk2Time += Time.deltaTime;
            attack2charge = false;
            anim.SetBool("SpecialAttackRelease", true);
            anim.SetBool("SpecialAttackCharge", false);
            GetComponentInParent<PlayerMove>().canwalk = true;
            GetComponentInParent<PlayerMove>().StaminaHolt = false;
            GetComponentInParent<PlayerMove>().canSpDoOB = true;
            if (atk2Time >= 2.5f) //finish
            { 
                attack2release = false; 
                CD = true; 
                atk2Time = 0f; 
                attack2charge = false;
                anim.SetBool("SpecialAttackRelease", false);
            } 
        }

           //////////////////////////////////////////////////////////////////////////////////////////RIGHT BUMPER ATTACK 1

            if ((CD == true) && (GetComponentInParent<PlayerMove>().stamina >= 15f))      
        {
            if ((Input.GetKeyDown(KeyCode.JoystickButton5))) { attack3 = true;  atk3Time = 0f; CD = false; }
        }
        if (attack3 == true) { attack3 = false; timestart3 = true;  CD = false; 
            GetComponentInParent<PlayerMove>().StaminaHolt = true;
            GetComponentInParent<PlayerMove>().stamina -= 10f;
            PlayerSource.clip = Swipe; PlayerSource.Play();
        }
        if (timestart3 == true) { atk3Time += Time.deltaTime; anim.SetBool("LightAttack1", true); attack3 = false; }
        if ((atk3Time > 0.1f)) { 
            GetComponentInParent<PlayerMove>().canSpDoOB = false;
            anim.SetBool("LightAttack1", false);
        }
        if ((atk3Time >= 1f)){ 
            GetComponentInParent<PlayerMove>().StaminaHolt = false;
            GetComponentInParent<PlayerMove>().canSpDoOB = true;
            attack3 = false;
            CD = true;
            timestart3 = false;
            atk3Time = 0f;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////RIGHT BUMPER ATTACK 2

        if ((atk3Time > 0.15f) && (atk3Time<0.5f) && (GetComponentInParent<PlayerMove>().stamina >= 5f))
        {
            if ((Input.GetKeyDown(KeyCode.JoystickButton5))) { attack4 = true; atk4Time = 0f;}
        }
        if ((atk1Time > 0.8f) && (atk1Time < 1.3f) && (GetComponentInParent<PlayerMove>().stamina >= 5f))
        {
            if ((Input.GetKeyDown(KeyCode.JoystickButton5))) { attack4 = true; atk4Time = 0f; }
        }
        if (attack4 == true)
        {

            PlayerSource.clip = Swipe; PlayerSource.Play();
            CD = false;  timestart4 = true;
            GetComponentInParent<PlayerMove>().StaminaHolt = true;
            GetComponentInParent<PlayerMove>().stamina -= 5f;
            attack4 = false;
        }
        if (timestart4 == true) {
            atk4Time += Time.deltaTime;
            anim.SetBool("LightAttack2", true); 
            anim.SetBool("LightAttack1", false);
            attack4 = false; 
            GetComponentInParent<PlayerMove>().canwalk = true;
            GetComponentInParent<PlayerMove>().canSpDoOB = true; } 
        if (atk4Time >= 0.75f)
        {   
            timestart4 = false;
            atk4Time = 0f;
            attack4 = false;
            anim.SetBool("LightAttack2", false);
            anim.SetBool("HeavyAttack", false); 
            GetComponentInParent<PlayerMove>().StaminaHolt = false;
            CD = true; 
        } 
        
      ////////////////////////////////////////////////////////////////////////////////////RIGHT TRIGGER ATTACK 2

        if ((atk1Time > 1f) && (atk1Time < 1.2f) && (atk5Time == 0) && (GetComponentInParent<PlayerMove>().stamina >= 10f))
        {
            if ((Input.GetAxis("RT")) == 1f) { attack5 = true; atk5Time = 0f; timestart1 = false; atk1Time = 0f; attack1 = false; }
        }
        if ((atk4Time > 0.5f) && (atk4Time < 0.9f) && (atk5Time == 0) && (GetComponentInParent<PlayerMove>().stamina >= 10f))
        {
            if ((Input.GetAxis("RT")) == 1f) { attack5 = true; atk5Time = 0f; timestart4 = false; atk4Time = 0f; attack4 = false; }
        }
        if (attack5 == true)
        {
            CD = false;
            PlayerSource.clip = Swipe; PlayerSource.Play();
            timestart5 = true;
            GetComponentInParent<PlayerMove>().StaminaHolt = true;
            GetComponentInParent<PlayerMove>().canwalk = false;
            GetComponentInParent<PlayerMove>().canSpDoOB = false;
            GetComponentInParent<PlayerMove>().stamina -= 10f;
        }
        if (timestart5 == true) { atk5Time += Time.deltaTime; anim.SetBool("HeavyAttack2", true); attack5 = false;  }

        if ((atk5Time > 0.5) && (atk5Time < 1))
            { 
            GetComponentInParent<PlayerMove>().canwalk = true;
            anim.SetBool("HeavyAttack2", true);
            anim.SetBool("HeavyAttack", false);
            anim.SetBool("LightAttack2", false);
        }
        if (atk5Time > 1.1f)
        {
            anim.SetBool("HeavyAttack2", false);
            timestart5 = false; 
            CD = true; 
            GetComponentInParent<PlayerMove>().StaminaHolt = false;
            GetComponentInParent<PlayerMove>().canwalk = true;
            GetComponentInParent<PlayerMove>().canSpDoOB = true;
            attack5 = false;
            atk5Time = 0f;
        }
       
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////FLASKS

        if (HP > MaxHP) { HP = MaxHP; }

        if ((Input.GetKeyDown(KeyCode.JoystickButton2))&&(HP<MaxHP)&&(flasks>0))
        {
            heal = true; healT = 0f;
            
        } 

        if (heal == true)
        {
            healT += Time.deltaTime;
            GetComponentInParent<PlayerMove>().canwalk = false; GetComponentInParent<PlayerMove>().canSpDoOB = false;
        }
        if ((healT >= 0.75)&&(heal==true)) { HP = MaxHP; heal = false;flasks = flasks - 1; GetComponentInParent<PlayerMove>().canwalk = true; GetComponentInParent<PlayerMove>().canSpDoOB = true; }

        FlaskNum.text = flasks.ToString();

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////DAMAGE TAKEN



    }


    void DiedAudio()
    {
        if (num == 1) { PlayerSource.clip = Death1; PlayerSource.Play(); }
        if (num == 2) { PlayerSource.clip = Death2; PlayerSource.Play(); }
        if (num == 3) { PlayerSource.clip = Death3; PlayerSource.Play(); num = 0; } 
    }

    private void OnTriggerEnter(Collider other)
    {

        if (GetComponentInParent<PlayerMove>().rollstart == false)
               {
                   if (other.CompareTag("EnemyAttack"))
                   {
                       HP -= EnemyDMG ;              
                       DiedAudio(); num++;
                   }
                   else if (other.CompareTag("EnemyAttack2"))
                   {
                       HP -= EnemyDMG2;
                       DiedAudio(); num++;
                   }
                   else if (other.CompareTag("FinalBossGrab"))
                   {
                       other.GetComponentInParent<FinalBossCombat>().Grab=true;
                       anim.SetTrigger("Stun");
                       GetComponentInParent<PlayerMove>().enabled = false;
                       CD = false;
                   }
        }   
        if (other.CompareTag("instant kill"))
               {
                   HP -= 1000;
            DiedAudio(); num++;
        }
    }
}

    
 
  


