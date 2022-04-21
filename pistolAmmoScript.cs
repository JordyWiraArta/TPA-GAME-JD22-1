using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pistolAmmoScript : MonoBehaviour
{
    public int currAmmo, extraAmmo, megazineSize;

    private void Update()
    {
        if (PickUpForPistolScript.plusAmmo)
        {
            extraAmmo += megazineSize;
            PickUpForPistolScript.plusAmmo = false;
        }
        if ((currAmmo == 0 && extraAmmo != 0) || Input.GetKeyDown(KeyCode.R)) pistolReload();
    }

    public void pistolReload()
    {
        if(extraAmmo > megazineSize)
        {
            int reloadAmmo = megazineSize - currAmmo;
            extraAmmo -= reloadAmmo;
            currAmmo += reloadAmmo;
        } if(extraAmmo > 0)
        {
            if(currAmmo + extraAmmo > megazineSize)
            {
                int leftAmmo = (currAmmo + extraAmmo) - megazineSize;
                currAmmo = megazineSize;
                extraAmmo = leftAmmo;
            }
            else
            {
                currAmmo += extraAmmo;
                extraAmmo = 0;
            }
        }
    }

}
