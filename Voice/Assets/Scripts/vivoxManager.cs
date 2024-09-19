using UnityEngine;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Vivox; 
using System.Threading.Tasks; //Task ���� �߰�.
using System;
/*
1. �񺹽� �α���. (�츮�� �߱� ���� ���� ������ ����)
   �������� ������ Ŭ���� �����.
*/
public class vivoxManager : MonoBehaviour
{
    public event Action OnLoginEndEvent; //�α��� ����� �߻��Ǵ� �̺�Ʈ (��ȯ���� ���� �Լ��� �븮��)
    public static vivoxManager V_Instance { get; private set; }
    private void Awake() 
    {
        DontDestroyOnLoad(gameObject);
        V_Instance = this;
    }
    private async void Start() //async�� await�� �ش� �Լ�, �ڵ���� �񵿱�� �۾� ���� �ð��� ���� �ɸ��� �۾��� ȿ�������� ó���Ѵ�.
    {
        await UnityServices.InitializeAsync(); //����Ƽ ���� �ʱ�ȭ
        await AuthenticationService.Instance.SignInAnonymouslyAsync(); //Authentication�� ����Ͽ� �͸� ������ �Ѵ�.
        await VivoxService.Instance.InitializeAsync(); //vivox�� �ʱ�ȭ �Ѵ�.
        Debug.Log("�ʱ�ȭ �Ϸ�");
        await LoginAsync(); //�α���
        Debug.Log("�α��� �Ϸ�");

        OnLoginEndEvent?.Invoke();
    }
    private async Task LoginAsync()
    {
        LoginOptions options = new LoginOptions(); //�α��� �ɼ� ����
        options.DisplayName = Guid.NewGuid().ToString(); //���÷��� �̸� ����
        await VivoxService.Instance.LoginAsync(options); //�α���
        Debug.Log($"Login : {options.DisplayName}");
    }
    public async void JoinVoiceChannel(string channelName)
    {
        await VivoxService.Instance.JoinGroupChannelAsync(channelName, ChatCapability.AudioOnly); //channelName �̸��� ���� ����ä�ø� �Ǵ� �濡 �����Ѵ�.
    } //channelName �̸��� ������ �ִ� ä���� ������ vivox�� �ش��ϴ� ä���� �ڵ����� ������ �ش�.
}
