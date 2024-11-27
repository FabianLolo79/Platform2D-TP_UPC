using Cinemachine;
using System.Collections;
using UnityEngine;

public class CamController : MonoBehaviour
{
    public CinemachineVirtualCamera vcCamera;
    public GameObject player, Door;
    public bool stopPlayerMovement = false;

    private void Start()
    {
        vcCamera.Follow = player.transform;
    }

    private void OnTriggerEnter2D()
    {
        StartCoroutine(CutSceneCoroutine());
    }

    IEnumerator CutSceneCoroutine()
    {
        vcCamera.Follow = Door.transform;
        stopPlayerMovement = true;

        yield return new WaitForSeconds(3);
        vcCamera.Follow = player.transform;
        gameObject.SetActive(false);
    }


}
