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

    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        axisX.Update(Time.deltaTime);
        axisY.Update(Time.deltaTime);
    }

    private void LateUpdate()
    {
        PlayerCamLookAt.eulerAngles = new Vector3(axisY.Value, axisX.Value, 0) * sensitivity;

        float yCamPos = cam.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, yCamPos, 0), rotationSpeed * Time.deltaTime);
    }

}
