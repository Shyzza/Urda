using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : Photon.MonoBehaviour
{
    public PhotonView PhotonView;
    public Rigidbody2D rb;
    public Animator anim;
    public GameObject PlayerCamera;
    public GameObject healthSystem;
    public SpriteRenderer sr;
    public Text PlayerNameText;
    public Image FillImage;
    private SampleMessageListener lis;
    public GameObject fire;



    public bool IsGraunded = false;
    public float MoveSpeed;
    public float JumpForce;
    public string glob;

    public GameObject BulletObject;
    public GameObject BulletDot;
    public Transform FirePose;


    private void Awake()
    {

        if (PhotonNetwork.isMasterClient != true && PhotonView.isMine)
        {
            photonView.RPC("FlipTrue", PhotonTargets.AllBuffered);
            fire = GameObject.Find("11_fire_spritesheet_2");
        }
        else
        {
            fire = GameObject.Find("11_fire_spritesheet_0");

        }


        if (PhotonView.isMine)
        {
            PlayerNameText.text = PhotonNetwork.playerName;
            PlayerNameText.color = Color.green;

        }
        else
        {
            PlayerNameText.text = photonView.owner.NickName;
            PlayerNameText.color = Color.red;
        }
    }

    [PunRPC]
    private void Update()
    {
/*        CheckInput();
*/        if (PhotonView.isMine)
        {
            CheckInput();
            /*            PlayerCamera.SetActive(true);
            */
        }
else {
            this.GetComponent<SerialController>().enabled = false;
        }

        /*
                glob = SampleMessageListener.global;
                Debug.Log("Glob ="+glob);*/

        /*   if (glob == "1;048A8392226D81" )

           {
               Shoot();
               anim.SetTrigger("IsAttack");
               SampleMessageListener.global = default(string);
               photonView.RPC("MANA", PhotonTargets.AllBuffered);

           }*/

    }
    [PunRPC]
    void OnMessageArrived(string msg)
    {
        if (PhotonView.isMine)
        {
            if (msg == "1;048A8392226D81" || msg == "1;040E5592226D81")

            {
                Shoot();
                anim.SetTrigger("IsAttack");
                MANA(10);
                Debug.Log("Attack");


                StartCoroutine(LateCall(2));


            }
            if (msg == "1;04828392226D81" || msg == "1;040A5592226D81")

            {
                anim.SetTrigger("IsAttack");
                MANA(20);

                this.GetComponent<HealthSystem>().regenMana = 5;
                StartCoroutine(ManaRegDis(5));
                StartCoroutine(LateCall(2));
                Debug.Log("ManaReg");

            }    

            if (msg == "1;04868392226D81" || msg == "1;04065592226D81")

            {
                anim.SetTrigger("IsAttack");
                MANA(45);
                this.GetComponent<SerialController>().enabled = false;
                Dot();
                StartCoroutine(LateCall(5));
                Debug.Log("Dot");

            }
        }
    }

        private void CheckInput()
    {
        var move = new Vector3(Input.GetAxisRaw("Horizontal"), 0);
        transform.position += move * MoveSpeed * Time.deltaTime;

     /*   if (Input.GetKeyDown(KeyCode.A))
        {

            this.GetComponent<PhotonView>().RPC("MANA", PhotonTargets.AllBuffered);
            photonView.RPC("FlipTrue", PhotonTargets.AllBuffered);

        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            photonView.RPC("FlipFalse", PhotonTargets.AllBuffered);
        }    
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetTrigger("IsAttack");
            MANA();
            health();
        }*/

    }

    private void Shoot()
    {

        if (sr.flipX == false)
        {
            GameObject obj = PhotonNetwork.Instantiate(BulletObject.name, new Vector2(FirePose.transform.position.x, FirePose.transform.position.y), Quaternion.identity, 0);
        }
        if (sr.flipX == true)
        {
            GameObject obj = PhotonNetwork.Instantiate(BulletObject.name, new Vector2(FirePose.transform.position.x, FirePose.transform.position.y), Quaternion.identity, 0);
            obj.GetComponent<PhotonView>().RPC("ChangeDir_left", PhotonTargets.AllBuffered);
        }
    }  
    private void Dot()
    {

        if (sr.flipX == false)
        {
            GameObject obj = PhotonNetwork.Instantiate(BulletDot.name, new Vector2(FirePose.transform.position.x, FirePose.transform.position.y), Quaternion.identity, 0);
        }
        if (sr.flipX == true)
        {
            GameObject obj = PhotonNetwork.Instantiate(BulletDot.name, new Vector2(FirePose.transform.position.x, FirePose.transform.position.y), Quaternion.identity, 0);
            obj.GetComponent<PhotonView>().RPC("ChangeDir_left", PhotonTargets.AllBuffered);
        }
    }


[PunRPC]
    private void FlipTrue ()
    {
        sr.flipX = true;


    } 
    
    [PunRPC]
    private void FlipFalse ()
    {
        sr.flipX = false;

    } 


    [PunRPC]
    private void MANA(float amount)
    {
        PhotonView target = this.gameObject.GetComponent<PhotonView>();
        float BullletDamage = amount;

        target.RPC("UseMana", PhotonTargets.AllBuffered, BullletDamage);
    }
    [PunRPC]
    private void ManaReg()
    {

        PhotonView target = this.gameObject.GetComponent<PhotonView>();
        float BullletDamage = 10;

        target.RPC("SetMaxHealth", PhotonTargets.AllBuffered, BullletDamage);
    }


    [PunRPC]
    public void ReduceMana(float amount)
    {
        ModifyMana(amount);
    }

    private void ModifyMana(float amount)
    {
        if (photonView.isMine)
        {
            FillImage.fillAmount -= amount;
        }
        else
        {
            FillImage.fillAmount -= amount;
        }
    }

    IEnumerator LateCall(float amount)
    {

        this.GetComponent<SerialController>().enabled = false;
/*        fire.GetComponent<SpriteRenderer>().enabled = false;*/

        yield return new WaitForSeconds(amount);

        this.GetComponent<SerialController>().enabled = true;
/*        fire.GetComponent<SpriteRenderer>().enabled = true;
*/
    }
    IEnumerator ManaRegDis(float amount)
    {

        yield return new WaitForSeconds(amount);

        this.GetComponent<HealthSystem>().regenMana = 0.5f;
    }

}
