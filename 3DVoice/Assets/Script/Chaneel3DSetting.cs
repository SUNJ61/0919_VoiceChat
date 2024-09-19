using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Unity.Services.Vivox;
using Unity.Services.Core;
using Unity.Services.Authentication;

public class Channel3DSetting : MonoBehaviour
{
    [SerializeField] private int audibleDistance = 32; //가청거리   
    [SerializeField] private int convertSationalDistance = 1; //작아지기 시작하는거리 (멀으면 소리가 작아져야하므로)

    [SerializeField] private float audioFadeIntensityByDistance = 1; //Fade모델에 따른 감쇠 강도 (소리가 작아질 때의 강도)
    [SerializeField] private AudioFadeModel audioFadeModel = AudioFadeModel.InverseByDistance; //위치에 따른 음량 감쇠 모델 (멀어지면 소리 감소)

    private void Start()
    {
        VivoxController.V_Instance.OnLoginEndEvent += HandleLoginEnd;
    }
    public Channel3DProperties GetChannel3DProperties() => new Channel3DProperties(audibleDistance, convertSationalDistance, audioFadeIntensityByDistance, audioFadeModel);
    //위에 변수들로 설정한 Channel3DProperties를 반환한다.
    private void HandleLoginEnd()
    {
        SceneManager.LoadScene("MainScene");
    }
}
