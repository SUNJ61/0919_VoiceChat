using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Unity.Services.Vivox;
using Unity.Services.Core;
using Unity.Services.Authentication;

public class Channel3DSetting : MonoBehaviour
{
    [SerializeField] private int audibleDistance = 32; //��û�Ÿ�   
    [SerializeField] private int convertSationalDistance = 1; //�۾����� �����ϴ°Ÿ� (������ �Ҹ��� �۾������ϹǷ�)

    [SerializeField] private float audioFadeIntensityByDistance = 1; //Fade�𵨿� ���� ���� ���� (�Ҹ��� �۾��� ���� ����)
    [SerializeField] private AudioFadeModel audioFadeModel = AudioFadeModel.InverseByDistance; //��ġ�� ���� ���� ���� �� (�־����� �Ҹ� ����)

    private void Start()
    {
        VivoxController.V_Instance.OnLoginEndEvent += HandleLoginEnd;
    }
    public Channel3DProperties GetChannel3DProperties() => new Channel3DProperties(audibleDistance, convertSationalDistance, audioFadeIntensityByDistance, audioFadeModel);
    //���� ������� ������ Channel3DProperties�� ��ȯ�Ѵ�.
    private void HandleLoginEnd()
    {
        SceneManager.LoadScene("MainScene");
    }
}
