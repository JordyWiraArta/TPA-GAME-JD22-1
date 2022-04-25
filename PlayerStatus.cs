using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth;
    public static int currHealth;
    public HealthScript healtBar;


    void Start()
    {
        currHealth = maxHealth;
        healtBar.setMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        healtBar.setHealth(currHealth);
        if (Input.GetKeyDown(KeyCode.Z))
        {
            currHealth -= 20;
        }

        

        
    }
}
