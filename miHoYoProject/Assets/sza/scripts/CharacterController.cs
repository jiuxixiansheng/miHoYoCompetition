using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Charactercontroller : MonoBehaviour
{
    private Vector2 _move;
    private CharacterController _characterController;
    private GameObject _maincamera;
    public float speed = 6;
    private float _targetRot = 0;//目标旋转角度
    //平滑旋转
    public float SmoothRotateTime = 0.1f;
    private float rotateVelocity;
    // 本人修改部分
    public Transform _targetCamera;
    public Animator _animator;
    private float jumpSpeed = 0f;//跳跃速度
    float targetHeight = 10;//跳跃目标高度
    float gravity = -9.8f;//重力加速度
    bool isOnAir = false;
    private int ballnum = 0;//玩家持有小球的数量
    public GameObject ballprefeb;//小球预制体
    private Transform handpoint;//投掷点
    private float throwForce = 35f;//投掷力度
    private bool isThrow = false;//是否在投掷

    private void OnEnable()
    {
        EventDispatcher.Instance.AddListener("getball", getball);
        EventDispatcher.Instance.AddListener("fail", fail);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener("getball", getball);
        EventDispatcher.Instance.RemoveListener("fail", fail);
    }

    void Start()
    {
        if (_maincamera == null)
        {
            _maincamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        _characterController = GetComponent<CharacterController>();
        handpoint = GameObject.Find("handpoint").transform;
    }

    public void OnMove(InputValue value)
    {
        _move = value.Get<Vector2>();
    }

    public void OnJump()
    {
        if (!isOnAir)
        {
            jumpSpeed = Mathf.Sqrt(targetHeight * -2f * gravity);
            isOnAir = true;
            _animator.SetBool("startJump", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_characterController.isGrounded&&jumpSpeed<=0)
        {
            jumpSpeed = -0.5f;
            isOnAir = false;
            _animator.SetBool("startJump", false);
        }
        else
        {
            jumpSpeed += gravity * Time.deltaTime;
        }
        if (_move != Vector2.zero&&!isThrow)
        {
            _animator.SetInteger("AnimationPar", 1);
            Vector3 inputdir = new Vector3(_move.x, 0, _move.y).normalized;
            _targetRot = Mathf.Atan2(inputdir.x, inputdir.z) * Mathf.Rad2Deg + _targetCamera.rotation.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.rotation.eulerAngles.y, _targetRot, ref rotateVelocity, SmoothRotateTime);
            transform.rotation = Quaternion.Euler(0, rotation, 0);
            Vector3 targetDir = Quaternion.Euler(0, _targetRot, 0) * Vector3.forward;
            _characterController.Move(targetDir.normalized * speed * Time.deltaTime);
        }
        else _animator.SetInteger("AnimationPar", 0);
        _characterController.Move(new Vector3(0, jumpSpeed, 0)*Time.deltaTime);
    }

    private void getball()
    {
        ballnum++;
    }

    public void OnThrow()
    {
        AnimatorStateInfo animInfo = _animator.GetCurrentAnimatorStateInfo(0);
        if (ballnum > 0&&(animInfo.IsName("Idle")||animInfo.IsName("Run")))
        {
            ballnum--;
            _animator.SetBool("startThrow", true);
            isThrow = true;
            StartCoroutine(Throwanim());
        }
    }

    IEnumerator Throwanim()
    {
        // 等待
        yield return new WaitForSeconds(1f);

        // 5 秒后执行的代码
        GameObject ball = Instantiate(ballprefeb, handpoint.position, handpoint.rotation);
        Rigidbody rd = ball.GetComponent<Rigidbody>();
        rd.AddForce(handpoint.forward * throwForce, ForceMode.Impulse);
        yield return new WaitForSeconds(1f);
        _animator.SetBool("startThrow", false);
        isThrow = false;
    }

    private void fail()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        this.enabled = false;
    }

}
