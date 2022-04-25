using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoClass : MonoBehaviour
{
    public int currAmmo, extraAmmo, megazineSize;

    public void reload(int currAmmo, int extraAmmo, int megazineSize)
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

        this.currAmmo = currAmmo;
        this.extraAmmo = extraAmmo;
    }

    public int plusAmmo(int extraAmmo, int megazineSize)
    {
        return extraAmmo + megazineSize;
    }

}
