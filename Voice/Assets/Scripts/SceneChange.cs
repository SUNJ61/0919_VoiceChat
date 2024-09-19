using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private void Start()
    {
        vivoxManager.V_Instance.OnLoginEndEvent += HandleLoginEnd; //vivoxManager에 있는 OnLoginEndEvent에 이벤트 등록
    } //초기화가 끝난 후 씬 전환

    void HandleLoginEnd()
    {
        Debug.Log("씬전환");
        SceneManager.LoadScene("VivoxSampleScene");
    }
}
