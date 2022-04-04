using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerGetPistolScript : MonoBehaviour
{
    [SerializeField] private GameObject pistol;
    [SerializeField] private Rig rightHand;
    [SerializeField] private GameObject pistolOnField;


    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask weaponMask;
    private Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
    // Start is called before the first frame update
    void Start()
    {
        pistol.SetActive(false);
        rightHand.weight = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        if (Physics.Raycast(ray, out RaycastHit hit, 4, weaponMask) &&  Input.GetKeyDown(KeyCode.F))
        {
            pistolOnField.SetActive(false);
            pistol.SetActive(true);
            rightHand.weight = 1;
        }


    }
}
