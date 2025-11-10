using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonLook : MonoBehaviour
{
    private Vector2 _look;
    private GameObject _maincamera;
    public GameObject TargetCamera;
    public float TopClamp = 70f;
    public float BottomClamp = -30f;
    private float _threshold = 0.01f;
    private float cinemachineYaw;
    private float cinemachinePitch;
    void Start()
    {
        if(_maincamera == null)
        {
            _maincamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        cinemachineYaw = TargetCamera.transform.rotation.eulerAngles.y;
    }

    public void OnLook(InputValue value)
    {
        _look = value.Get<Vector2>();
    }
    // Update is called once per frame
    void Update()
    {
        if(_look.sqrMagnitude >= _threshold)
        {
            cinemachineYaw += _look.x/10;
            cinemachinePitch += -_look.y/10;
        }
        cinemachineYaw = ClampAngle(cinemachineYaw,float.MinValue,float.MaxValue);
        cinemachinePitch = ClampAngle(cinemachinePitch,BottomClamp,TopClamp);
        TargetCamera.transform.rotation = Quaternion.Euler(cinemachinePitch,cinemachineYaw,0);
    }

    private static float ClampAngle(float angle,float min,float max)
    {
        if (angle < -360) angle += 360;
        if (angle > 360) angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
