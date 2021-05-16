using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MANA : Photon.MonoBehaviour
{
    public Image FillImage;


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
}
