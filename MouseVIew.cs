using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseVIew : MonoBehaviour
{
    [SerializeField] private Transform PlayerCamLookAt;
    [SerializeField] private float sensitivity;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Camera cam;
    [SerializeField] private Cinemachine.AxisState axisX, axisY;

    private Transform aimSphere;
    public RaycastHit hit;
    private Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
    [SerializeField] private LayerMask aimMask;
    [SerializeField] private LayerMask weaponMask;

    // Start is called before the first frame update
    void Start()
    {
        aimSphere = GameObject.Find("aimSphere").transform;
    }

    // Update is called once per frame
    void Update()
    {
        axisX.Update(Time.deltaTime);
        axisY.Update(Time.deltaTime);
        rayPoint();
    }

    private void LateUpdate()
    {
        PlayerCamLookAt.eulerAngles = new Vector3(axisY.Value, axisX.Value, 0) * sensitivity;

        float yCamPos = cam.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yCamPos, 0), rotationSpeed * Time.deltaTime);
    }

    public void rayPoint()
    {
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);

        if(Physics.Raycast(ray, out hit, Mathf.Infinity, aimMask))
        {
            aimSphere.position = hit.point;
        }
    }
    
}
