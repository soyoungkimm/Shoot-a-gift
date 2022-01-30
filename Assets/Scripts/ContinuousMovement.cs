using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ContinuousMovement : MonoBehaviour
{
    public float speed = 1;
    public XRNode inputSource;
    public float gravity = -9.81f;
    public LayerMask groundLayer;
    public float additionalHeight = 0.2f;

    private float fallingSpeed;
    private XRRig rig;
    private Vector2 inputAxis;
    private CharacterController character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        rig = GetComponent<XRRig>();
    }

    // Update is called once per frame
    void Update()
    {
        // 지정된 XRNode인 inputSource의 endpoint 에서 입력 장치를 가져옴
        InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);

        // 현재 VR 장치에 액세스
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);
    }

    private void FixedUpdate()
    {
        // player(character controller)의 높이를 헤드셋(VR 장치)에 맞춤 
        CapsuleFollowHeadset();

        // 회전
        Quaternion headYaw = Quaternion.Euler(0, rig.cameraGameObject.transform.eulerAngles.y, 0);

        // 회전 * 움직임
        Vector3 direction = headYaw * new Vector3(inputAxis.x, 0, inputAxis.y);

        // player(character controller)를 이동시킴
        character.Move(direction * Time.fixedDeltaTime * speed);

        // ---- gravity 구현 부분 ----
        bool isGrounded = CheckIfGrounded();

        // 땅에 닫고 있으면 
        if (isGrounded)
        {
            // 떨어지는 속도를 0으로
            fallingSpeed = 0;
        }
        else
        {
            // 땅에 안 닫고 있으면 떨어지는 속도를 중력값과 Time.fixedDeltaTime을 곱해서 점점 빠르게 함
            fallingSpeed += gravity * Time.fixedDeltaTime;
        }
        
        // 플레이어를 아래로 떨어트림
        character.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
    }

    
    void CapsuleFollowHeadset()
    {
        // player(character controller)의 높이 설정
        character.height = rig.cameraInRigSpaceHeight + additionalHeight;

        // VR camera의 position을 world space에서 local space로 변환
        Vector3 capsuleCenter = transform.InverseTransformPoint(rig.cameraGameObject.transform.position);

        // player의 center 위치를 설정
        character.center = new Vector3(capsuleCenter.x, character.height / 2 + character.skinWidth, capsuleCenter.z);
    }


    // 땅에 닿고 있는지 체크하는 함수
    private bool CheckIfGrounded()
    {
        Vector3 rayStart = transform.TransformPoint(character.center);
        float rayLength = character.center.y + 0.01f;

        // SphereCast는 ray가 ray cast보다 두꺼워서 넓은 범위를 검출할 수 있음, ray를 쏴서 ray에 맞은 것이 있으면 true, 아니면 false를 반환
        bool hasHit = Physics.SphereCast(rayStart, character.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);
        return hasHit;
    }
}
