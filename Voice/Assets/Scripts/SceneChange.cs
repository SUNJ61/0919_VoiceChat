using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    private void Start()
    {
        vivoxManager.V_Instance.OnLoginEndEvent += HandleLoginEnd; //vivoxManager�� �ִ� OnLoginEndEvent�� �̺�Ʈ ���
    } //�ʱ�ȭ�� ���� �� �� ��ȯ

    void HandleLoginEnd()
    {
        Debug.Log("����ȯ");
        SceneManager.LoadScene("VivoxSampleScene");
    }
}
