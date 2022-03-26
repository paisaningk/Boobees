using Cinemachine;
using Script.Controller;
using Script.Spawn;
using UnityEngine;

public class SetupSecen : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    public GameObject PlayerRonin;
    public GameObject PlayerGun;
    public Transform Spawn;
    void Start()
    {
        Time.timeScale = 1;
        if (SpawnPlayer.instance.PlayerType == PlayerType.SwordMan)
        {
            PlayerRonin.SetActive(true);
            PlayerRonin.transform.position = Spawn.position;
            VirtualCamera.m_Follow = PlayerRonin.transform;
        }
        else
        {
            PlayerGun.SetActive(true);
            PlayerGun.transform.position = Spawn.position;
            VirtualCamera.m_Follow = PlayerGun.transform;
        }
    }
}
