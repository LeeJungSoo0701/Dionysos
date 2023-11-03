using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class Rhythm : MonoBehaviour
{
    [SerializeField] private ParticleSystem RhythmEffect; // ������ ���߱� ���� ����Ʈ
    [SerializeField] private ParticleSystem PerfectEffect; // ����Ʈ �� �߻��ϴ� ����Ʈ
    ParticleSystemRenderer RhythmRenderer; // ����Ʈ ������
    [SerializeField] private Material[] RythmMaterial; // ����Ʈ ���׸���
    public float Speed; // ����Ʈ �ӵ�
    private float EffectTime; // ���� ����Ʈ�� �ð�
    [SerializeField] private float Sync; // ���� ��ũ
    [SerializeField] float Range; // ����Ʈ üũ ����
    [SerializeField] private Player player;
    [SerializeField] private bool SyncScene;
    void Awake()
    {
        //Speed = 60 / GameManager.Instance.Stages[UnityEngine.SceneManagement.SceneManager.GetActiveScene().name].bpm;
        Speed = 128f / 60f;
        RhythmEffect.playbackSpeed = Speed;
        RhythmRenderer = RhythmEffect.GetComponent<ParticleSystemRenderer>();
    }


    void Update()
    {
        EffectTime = RhythmEffect.time;
        Sync = RhythmManager.Instance.RhythmSyncValue;
        if (Input.GetKeyDown(KeyCode.X) && SyncScene)
        {
            InputAction("X");
        }
    }
    public void InputAction(string input)
    {
        if (EffectTime < Sync + Speed * Range && EffectTime > Sync - Speed * Range)
        {
            Perfect(input);
        }
        else
        {
            Bad(input);
        }
    }
    void Perfect(string input)
    {
        if (player != null && !player.fever)
            player.PlusFever(20);
        Debug.Log("Perfect!");
        PerfectEffect.Play();
        switch(input)
        {
            case "Attack":
                player.attackPowerUp = true;
                break;
            case "Dash":
                player.SkillInterface.powerUp = true;
                break;
            case "Slash":
                player.SkillInterface.powerUp = true;
                break;
            case "FireBall":
                player.fireBallPowerUP = true;
                break;
            default:
                break;
        }
    }

    void Bad(string input)
    {
        Debug.Log("Bad!");
        switch (input)
        {
            case "Attack":
                player.attackPowerUp = false;
                break;
            case "Dash":
                player.SkillInterface.powerUp = false;
                break;
            case "Slash":
                player.SkillInterface.powerUp = false;
                break;
            case "FireBall":
                player.fireBallPowerUP = false;
                break;
            default:
                break;
        }
    }  
}
