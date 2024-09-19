using UnityEngine;
using System.Threading.Tasks; //비동기 객체를 생성하여 네트워크 처럼 쓰기 위해 선언
using Unity.Services.Vivox;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System;
using System.Collections;

/*
3D음성채팅 만들기. 
*/
public class VivoxController : MonoBehaviour
{
    [SerializeField] private Channel3DSetting channel3DSetting;
    [SerializeField] private float positionUpdateRate = 0.5f; //위치 업데이트 되는 주기

    public event Action OnLoginEndEvent;
    public static VivoxController V_Instance { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        V_Instance = this;

        channel3DSetting = GetComponent<Channel3DSetting>();
    }
    private async void Start()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        await VivoxService.Instance.InitializeAsync();
        await LoginAsync();

        OnLoginEndEvent?.Invoke();
    }
    private async Task LoginAsync()
    {
        LoginOptions options = new LoginOptions();
        options.DisplayName = Guid.NewGuid().ToString();
        await VivoxService.Instance.LoginAsync(options);
    }
    public async void JoinVoiceCnannel(string channelName)
    {
        await VivoxService.Instance.JoinGroupChannelAsync(channelName, ChatCapability.AudioOnly);
    }
    public async void Join3DChannel(GameObject speakeObj, string channelName) //위치 음성 채널 접속
    {
        await VivoxService.Instance.JoinPositionalChannelAsync(channelName, ChatCapability.AudioOnly, channel3DSetting.GetChannel3DProperties()); //3D 음성 채팅설정을 하여 음성 채팅에 접속.
        StartCoroutine(Update3DPosition(speakeObj, channelName));//음성채팅할 위치를 주기적으로 업데이트 하기위해서 코루틴을 돌린다.
    }
    IEnumerator Update3DPosition(GameObject speakeObj, string channelName)
    {
        while (true)
        {
            VivoxService.Instance.Set3DPosition(speakeObj, channelName);
            yield return new WaitForSeconds(positionUpdateRate);
        }
    } //위치 채널 속성을 생성한 후 positionChannel에 접속하고 주기적으로 위치를 업데이트 해준다.
}
