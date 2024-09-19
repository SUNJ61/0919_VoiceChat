using UnityEngine;
public class GMR : MonoBehaviour
{
    [SerializeField] private string ChannelName = "3DChannel";

    private void Awake()
    {
        VivoxController.V_Instance.Join3DChannel(gameObject, ChannelName);
    }
}
