using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dot : Photon.MonoBehaviour
{
    public bool MoveDir = false; //false (right)
    public float DestroyTime ;
    public float MoveSpeed ;
    public float BullletDamage;

    private void Awake()
    {
/*        StartCoroutine("DestroyObject");
*/    }

    IEnumerator DestroyByTime()
    {
        yield return new WaitForSeconds(DestroyTime);
        this.GetComponent<PhotonView>().RPC("DestroyObject", PhotonTargets.AllBuffered);
    }

    [PunRPC]
    public void ChangeDir_left()
    {
        MoveDir = true;
    }

    [PunRPC]
    public void DestroyObject()
    {

        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (!MoveDir)
        {
            transform.Translate(Vector2.right * MoveSpeed * Time.deltaTime);

        }
        else
            transform.Translate(Vector2.left * MoveSpeed * Time.deltaTime);
    }
    [PunRPC]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.isMine)
            return;
        PhotonView target = collision.gameObject.GetComponent<PhotonView>();
        if (target != null && (!target.isMine || target.isSceneView))
        {
            if (target.tag == "Player")
            {
                        target.RPC("Dot", PhotonTargets.AllBuffered, BullletDamage);
                
            }
            this.GetComponent<PhotonView>().RPC("DestroyObject", PhotonTargets.AllBuffered);
        }
    }
}
