using UnityEngine;

public class GameScene : MonoBehaviour
{
    [SerializeField] private string channelName = "TestVivox";

    private void Awake()
    {
        vivoxManager.V_Instance.JoinVoiceChannel(channelName);
    }
}
