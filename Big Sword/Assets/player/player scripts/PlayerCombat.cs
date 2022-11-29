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
 
    public Transform player; 
    GameObject HPObj;
   // public TextMeshProUGUI FlaskNum;



    public float MaxHP, StrMod, ResMod, ATK1DMG, ATK2DMG;
    public float HP;
    private float  atk1Time, atk2Time, atk3Time, atk4Time, atk5Time, healT;

    public int flasks;
    public int MaxFlask;

    private float EnemyDMG, EnemyDMG2;//SlugDMG,

    private float _youDiedTime = 0f;
    private bool _youDied = false;

    public bool heal;
    private bool attack1, attack2charge, attack2release, attack3, attack4, attack5,timestart1, timestart3, timestart4, timestart5;
      
    private bool canatk = true;
    private bool CD = true;
   
   
    void Start()
    {
        //anim = GetComponentInParentInChildren<Animator>();
        anim = GetComponentInParent<Animator>();
        HPObj = GameObject.Find("HPObj");


        CD = true;
        
        StrMod = 1f;
        ResMod = 1f;

     //   SlugDMG = 100f;
        EnemyDMG = 6f;
        EnemyDMG2 = 12f;
        flasks = MaxFlask;
    }


    void Update()
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////HEALTH BAR
        if (HP > 0)
        {
            HPObj.GetComponentInParent<RectTransform>().localScale = new Vector3((HP / 15), 1, 1);
            HPObj.GetComponentInParent<RectTransform>().anchoredPosition = new Vector2(15f + HP / 1.5f, 10);
        }
        else
        {
            HPObj.GetComponentInParent<RectTransform>().localScale = new Vector3(0, 1, 1);
            HPObj.GetComponentInParent<RectTransform>().anchoredPosition = new Vector2(0, 10);
            _youDied = true;
        }
        if (_youDied) { _youDiedTime += Time.deltaTime; YouDiedAnim.SetTrigger("Active"); player.GetComponent<PlayerMove>().enabled = false; CD = false; }
        if (_youDiedTime >= 4.25f) { SceneManager.LoadScene(RespawnScene.name); }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////RIGHT TRIGGER ATTACK

        if ((CD == true) && (GetComponentInParent<PlayerMove>().stamina >= 10f))
        {
            if ((Input.GetAxis("RT")) == 1f) { attack1 = true; }
        }

            if (attack1 == true) { timestart1 = true; atk1Time = 0f; CD = false; attack1 = false;
            anim.SetBool("HeavyAttack", true);
            GetComponentInParent<PlayerMove>().stamina -= 10f; 
            GetComponentInParent<PlayerMove>().StaminaHolt = true; 
            GetComponentInParent<PlayerMove>().canwalk= false;  
            GetComponentInParent<PlayerMove>().canSpDoOB = false; }
            if (timestart1 == true) { atk1Time += Time.deltaTime;  attack1 = false; GetComponentInParent<PlayerMove>().Speed = 0f; }
            if ((atk1Time >= 1.1f)&&(atk1Time<=2f)) { anim.SetBool("HeavyAttack", false); }
            if (atk1Time > 1.5f) {GetComponentInParent<PlayerMove>().canSpDoOB = true;}
            if ((atk1Time > 1.5f) && (Input.GetKey(KeyCode.JoystickButton1))) {
            timestart1 = false; CD = true; atk1Time = 0f;
            GetComponentInParent<PlayerMove>().canwalk = true;
            GetComponentInParent<PlayerMove>().StaminaHolt = false;
            }
            if (atk1Time > 2f) { timestart1 = false;  CD = true;  atk1Time = 0f;
            GetComponentInParent<PlayerMove>().canwalk= true;    
            GetComponentInParent<PlayerMove>().StaminaHolt = false;
            attack1 = false;
            }
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////LEFT TRIGGER ATTACK

        if ((canatk == true) && (CD == true) && (atk2Time<=0f)&& (GetComponentInParent<PlayerMove>().stamina >= 30f))
        {
            if ((Input.GetAxis("LT")) > 0f) { attack2charge = true; 
            GetComponentInParent<PlayerMove>().StaminaHolt = true;
            GetComponentInParent<PlayerMove>().canwalk= false;  
            GetComponentInParent<PlayerMove>().canSpDoOB = false; }
        }
        if (((Input.GetAxis("LT")) == 0f)&&(atk2Time>0.1f)&&(atk2Time<1f)&&(attack2release==false)) { atk2Time = 0f; attack2charge = false; anim.SetBool("SpecialAttackCharge", false); CD = true;
            GetComponentInParent<PlayerMove>().canwalk= true;  
            GetComponentInParent<PlayerMove>().canSpDoOB = true;
            GetComponentInParent<PlayerMove>().StaminaHolt = false; }
        if (((Input.GetAxis("LT")) == 0f) && (atk2Time >= 1f)&&(attack2charge==true)) { attack2release = true; atk2Time = 0f; attack2charge = false; GetComponentInParent<PlayerMove>().stamina -= 30f; }
        if (attack2charge==true) {  atk2Time += Time.deltaTime;  anim.SetBool("SpecialAttackCharge", true); CD = false;   }  
        if (attack2release==true) {
            atk2Time += Time.deltaTime; 
            
            anim.SetBool("SpecialAttackRelease", true); 
            anim.SetBool("SpecialAttackCharge", false);
            GetComponentInParent<PlayerMove>().canwalk = true;
            GetComponentInParent<PlayerMove>().StaminaHolt = false;
            GetComponentInParent<PlayerMove>().canSpDoOB = true;
 
            if (atk2Time >= 2f) { atk2Time = 0f;  attack2release = false; CD = true;}
        }
        
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////RIGHT BUMPER ATTACK 1
        
        if ((CD == true) && (GetComponentInParent<PlayerMove>().stamina >= 10f))      
        {
            if ((Input.GetKeyDown(KeyCode.JoystickButton5))) { attack3 = true;  atk3Time = 0f; }
        }
        if (attack3 == true) { attack3 = false; timestart3 = true;  CD = false; 
            GetComponentInParent<PlayerMove>().StaminaHolt = true;
            GetComponentInParent<PlayerMove>().stamina -= 10f; }
        if (timestart3 == true) { atk3Time += Time.deltaTime; anim.SetBool("LightAttack1", true); attack3 = false; }
        if ((atk3Time > 0.1f)) { 
            GetComponentInParent<PlayerMove>().canSpDoOB = false;
            anim.SetBool("LightAttack1", false);
        }
        if ((atk3Time > 0.5f)){ 
            GetComponentInParent<PlayerMove>().StaminaHolt = false;
            GetComponentInParent<PlayerMove>().canSpDoOB = true;
            attack3 = false;
            CD = true;
            timestart3 = false;
            atk3Time = 0f;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////RIGHT BUMPER ATTACK 2

        if ((atk3Time > 0.15f) && (atk3Time<0.4f) && (GetComponentInParent<PlayerMove>().stamina >= 5f))
        {
            if ((Input.GetKeyDown(KeyCode.JoystickButton5))) { attack4 = true; atk4Time = 0f;}
        }
        if ((atk1Time > 0.8f) && (atk1Time < 1.3f) && (GetComponentInParent<PlayerMove>().stamina >= 5f))
        {
            if ((Input.GetKeyDown(KeyCode.JoystickButton5))) { attack4 = true; atk4Time = 0f; }
        }
        if (attack4 == true)
        {
           
          //  attack3 = false; atk3Time = 0f; timestart3 = false;
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
        
        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////RIGHT TRIGGER ATTACK 2

        if ((atk1Time > 1f) && (atk1Time < 1.5f) && (GetComponentInParent<PlayerMove>().stamina >= 5f))
        {
            if ((Input.GetAxis("RT")) == 1f) { attack5 = true; atk5Time = 0f; timestart1 = false; atk1Time = 0f; attack1 = false; }
        }
        if ((atk4Time > 0.5f) && (atk4Time < 0.9f) && (GetComponentInParent<PlayerMove>().stamina >= 5f))
        {
            if ((Input.GetAxis("RT")) == 1f) { attack5 = true; atk5Time = 0f; timestart4 = false; atk4Time = 0f; attack4 = false; }
        }
        if (attack5 == true)
        {
            CD = false;
           
            timestart5 = true;
            GetComponentInParent<PlayerMove>().StaminaHolt = true;
            GetComponentInParent<PlayerMove>().canwalk = false;
            GetComponentInParent<PlayerMove>().canSpDoOB = false;
            GetComponentInParent<PlayerMove>().stamina -= 5f;
        }
        if (timestart5 == true) { atk5Time += Time.deltaTime; anim.SetBool("HeavyAttack2", true); attack5 = false;  }

        if ((atk5Time > 0.5) && (atk5Time < 1))
            { 
            GetComponentInParent<PlayerMove>().canwalk = true;
            anim.SetBool("HeavyAttack2", true);
            anim.SetBool("HeavyAttack", false);
            anim.SetBool("LightAttack2", false);
        }
        if (atk5Time > 1.5f)
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
        if (atk5Time > 1f) {
            anim.SetBool("HeavyAttack2", false);
            GetComponentInParent<PlayerMove>().canSpDoOB = true; 
            if (Input.GetKey(KeyCode.JoystickButton1)) { 
                timestart5 = false; 
                atk5Time = 0f;
                CD = true;
                GetComponentInParent<PlayerMove>().StaminaHolt = false;
                GetComponentInParent<PlayerMove>().canwalk = true;
                attack5 = false;
            }
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
        if ((healT >= 0.75)&&(heal==true)) { HP += 40f; heal = false;flasks = flasks - 1; GetComponentInParent<PlayerMove>().canwalk = true; GetComponentInParent<PlayerMove>().canSpDoOB = true; }
      //  FlaskNum.text = flasks.ToString();




        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////DAMAGE TAKEN



    }




    private void OnTriggerEnter(Collider other)
    {

        if (GetComponentInParent<PlayerMove>().rollstart == false)
               {
                   if (other.CompareTag("EnemyAttack"))
                   {
                       HP -= EnemyDMG ;
                   }
            if (other.CompareTag("EnemyAttack2"))
            {
                HP -= EnemyDMG2;
            }
        }   
        if (other.CompareTag("instant kill"))
               {
                   HP -= 1000;
               }
    }
}

    
 
  


