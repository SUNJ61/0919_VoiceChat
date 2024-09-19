using UnityEngine;
using System.Threading.Tasks; //�񵿱� ��ü�� �����Ͽ� ��Ʈ��ũ ó�� ���� ���� ����
using Unity.Services.Vivox;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System;
using System.Collections;

/*
3D����ä�� �����. 
*/
public class VivoxController : MonoBehaviour
{
    [SerializeField] private Channel3DSetting channel3DSetting;
    [SerializeField] private float positionUpdateRate = 0.5f; //��ġ ������Ʈ �Ǵ� �ֱ�

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
    public async void Join3DChannel(GameObject speakeObj, string channelName) //��ġ ���� ä�� ����
    {
        await VivoxService.Instance.JoinPositionalChannelAsync(channelName, ChatCapability.AudioOnly, channel3DSetting.GetChannel3DProperties()); //3D ���� ä�ü����� �Ͽ� ���� ä�ÿ� ����.
        StartCoroutine(Update3DPosition(speakeObj, channelName));//����ä���� ��ġ�� �ֱ������� ������Ʈ �ϱ����ؼ� �ڷ�ƾ�� ������.
    }
    IEnumerator Update3DPosition(GameObject speakeObj, string channelName)
    {
        while (true)
        {
            VivoxService.Instance.Set3DPosition(speakeObj, channelName);
            yield return new WaitForSeconds(positionUpdateRate);
        }
    } //��ġ ä�� �Ӽ��� ������ �� positionChannel�� �����ϰ� �ֱ������� ��ġ�� ������Ʈ ���ش�.
}
