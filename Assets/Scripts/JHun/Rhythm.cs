using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    void Awake()
    {
        RhythmEffect.playbackSpeed = Speed;
        RhythmRenderer = RhythmEffect.GetComponent<ParticleSystemRenderer>();
    }


    void Update()
    {
        EffectTime = RhythmEffect.time;
        Sync = RhythmManager.Instance.RhythmSyncValue;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (EffectTime < Sync + Speed * Range && EffectTime > Sync - Speed * Range)
            {
                Debug.Log("Perfect!");
                PerfectEffect.Play();
                /*int i = Random.RandomRange(0, RythmMaterial.Length);
                RhythmRenderer.material = RythmMaterial[i];*/
            }
            else
            {
                Debug.Log("Bad!");
                /*int i = Random.RandomRange(0, RythmMaterial.Length);
                RhythmRenderer.material = RythmMaterial[i];*/
            }
        }
    }
}
