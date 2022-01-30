using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandPresence : MonoBehaviour
{
    public bool showController = false;
    public InputDeviceCharacteristics controllerCharacteristic;
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab;

    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;
    private Animator handAnimator;

    // Start is called before the first frame update
    void Start()
    {
        Tryinitialize();
    }


    void UpdateHandAnimation()
    {
        // 검지와 엄지로 잡는 행위를 하면
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            // Blend Tree의 Parameter인 'Trigger'에 이 행위 만큼의 값을 보낸다
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        // 사용자가 잡는 행위를 하면 
        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            // // Blend Tree의 Parameter인 'Grip'에 잡는 행동 만큼의 값을 보낸다
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }


    void Tryinitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        
        // device에 현재 입력된 VR장치가 들어감
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristic, devices);


        foreach (var item in devices)
        {
            // 현재 장치의 명과 장치의 종류들이 console창에 출력됨
            Debug.Log(item.name + item.characteristics);
        }

        // 현재 장치가 입력이 됬으면
        if (devices.Count > 0)
        {
            // targetDevice에 장치를 대입
            targetDevice = devices[0];
            
            // 내가 넣어놓은(모든 VR controller 다 넣어놓음) controllerPrefabs 중 현재 장치와 같은 이름의 장치를 찾음
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            
            // 이름이 같은 장치가 있으면 
            if (prefab)
            {
                // 그 장치 prefab을 복사해서 spawnedController에 대입
                spawnedController = Instantiate(prefab, transform);
            }
            // 이름이 같은 장치가 없으면 
            else
            {
                Debug.LogError("컨트롤러 모델을 찾을 수 없습니다");

                // default controller로 지정(흰색 기다란 큐브로 만들어 놓음)
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }

            // handModelPrefab에다 손 model넣어둠
            // spawnedHandModel에 handModelPrefab 대입
            spawnedHandModel = Instantiate(handModelPrefab, transform);

            // handModelPrefab 안에 있는 애니메이터를 handAnimator에 대입
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!targetDevice.isValid)
        {
            // hand model과 controller model 찾는 함수 실행
            Tryinitialize();
        }
        else
        {
            // showController를 true로 바꾸면 controller가 나오게 할 수 있음
            if (showController)
            {
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            // 손 모양이 나오기를 원하기 때문에 여기 실행
            else
            {
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(false);

                // 손 애니메이션 변경하는 함수 실행
                UpdateHandAnimation();
            }
        }

        
    }
}
