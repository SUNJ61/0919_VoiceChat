using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Vivox; 
using System.Threading.Tasks; //Task 사용시 추가.
using System;
/*
1. 비복스 로그인. (우리가 발급 받은 전용 서버로 접속)
   유저들이 접속할 클라우드 만들기.
*/
public class vivoxManager : MonoBehaviour
{
    public event Action OnLoginEndEvent; //로그인 종료시 발생되는 이벤트 (반환값이 없는 함수를 대리함)
    public static vivoxManager V_Instance { get; private set; }
    private void Awake() 
    {
        DontDestroyOnLoad(gameObject);
        V_Instance = this;
    }
    private async void Start() //async와 await는 해당 함수, 코드들을 비동기로 작업 시켜 시간이 오래 걸리는 작업을 효율적으로 처리한다.
    {
        await UnityServices.InitializeAsync(); //유니티 서비스 초기화
        await AuthenticationService.Instance.SignInAnonymouslyAsync(); //Authentication를 사용하여 익명 인증을 한다.
        await VivoxService.Instance.InitializeAsync(); //vivox를 초기화 한다.
        Debug.Log("초기화 완료");
        await LoginAsync(); //로그인
        Debug.Log("로그인 완료");

        OnLoginEndEvent?.Invoke();
    }
    private async Task LoginAsync()
    {
        LoginOptions options = new LoginOptions(); //로그인 옵션 생성
        options.DisplayName = Guid.NewGuid().ToString(); //디스플레이 이름 설정
        await VivoxService.Instance.LoginAsync(options); //로그인
        Debug.Log($"Login : {options.DisplayName}");
    }
    public async void JoinVoiceChannel(string channelName)
    {
        await VivoxService.Instance.JoinGroupChannelAsync(channelName, ChatCapability.AudioOnly); //channelName 이름을 가진 음성채팅만 되는 방에 접속한다.
    } //channelName 이름을 가지고 있는 채널이 없으면 vivox는 해당하는 채널을 자동으로 생성해 준다.
}
