using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MouseVIew : MonoBehaviour
{
    [SerializeField] private Transform PlayerCamLookAt;
    [SerializeField] private float sensitivity;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Camera cam;
    public Cinemachine.AxisState axisX, axisY;

    [SerializeField] private Transform asunaPos;

    // zoom in
    [SerializeField] private CinemachineVirtualCamera vcam;
    private float zoomFov = 20;
    [SerializeField] private float zoomSpeed;
    private float normalFov;

    // aim at
    public Transform aimSphere;
    [SerializeField] private LayerMask aimMask;
    private Vector2 screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);


    // Start is called before the first frame update
    void Start()
    {
        normalFov = vcam.m_Lens.FieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        rayAim(Mathf.Infinity);
        if (!questScript.talk)
        {
            axisX.Update(Time.deltaTime);
            axisY.Update(Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            vcam.m_Lens.FieldOfView = Mathf.Lerp(vcam.m_Lens.FieldOfView, zoomFov, zoomSpeed * Time.deltaTime);
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            vcam.m_Lens.FieldOfView = normalFov;
        }
    }

    private void LateUpdate()
    {
        if (!questScript.talk)
        {
            PlayerCamLookAt.eulerAngles = new Vector3(axisY.Value, axisX.Value, 0) * sensitivity;

            float yCamPos = cam.transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yCamPos, 0), rotationSpeed * Time.deltaTime);
        } else
        {
            Quaternion targetRot = Quaternion.LookRotation(asunaPos.position - transform.position);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, 100 * Time.deltaTime);
        }
    }

    public RaycastHit rayAim(float range)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, range, aimMask))
        {
            aimSphere.position = Vector3.Lerp(aimSphere.position, hit.point, sensitivity * Time.deltaTime);
        }

        return hit;
    }
    
}
