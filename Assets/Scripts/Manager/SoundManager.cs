using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class SoundManager : Singleton<SoundManager>
    {
        public AudioSource BGMPlayer; // ����� ����ϱ� ���� �÷��̾�
        public AudioSource SFXPlayer; // ȿ���� ����ϱ� ���� �÷��̾�

        [SerializeField] public AudioClip[] MainAudioClip; // ����� ����
        [SerializeField] private AudioClip[] SFXAudioClips; // ȿ���� ����

        public static float VolumeBGM = 1.0f; // ����� ����
        public static float VolumeSFX = 1.0f; // ȿ���� ����

        //Dictionary<key, value>�� ������� �ҷ��´�
        Dictionary<string, AudioClip> audioclipdic = new Dictionary<string, AudioClip>();
        private void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(gameObject);
            //BGMPlayer = GameObject.Find("BGM").GetComponent<AudioSource>(); // BGM�÷��̾�
            //SFXPlayer = GameObject.Find("SFX").GetComponent<AudioSource>(); // ȿ�����÷��̾�

            // SFXAudioClips���� �����Ŭ������ Dictionary���� ����
            foreach (AudioClip audioclip in SFXAudioClips)
            {
                audioclipdic.Add(audioclip.name, audioclip);
            }

            //PlayBGMSound();
        }

        public void PlaySFXSound(string name, float volum = 1.0f)
        {
            if (audioclipdic.ContainsKey(name) == false) // ȿ������ ���ٸ�
            {
                Debug.Log(name + "�� audioClipsDic�� �����ϴ�.");
                return;
            }

            SFXPlayer.PlayOneShot(audioclipdic[name], volum * VolumeSFX);

        }

        public void PlayBGMSound()
        {
            //BGMPlayer.clip = MainAudioClip[0];
            BGMPlayer.loop = true;
            BGMPlayer.volume *= 0.5f;
        }

        private void Update()
        {
            BGMPlayer.volume = VolumeBGM;
            SFXPlayer.volume = VolumeSFX;
            if(Input.GetKeyDown(KeyCode.O))
            {
                Debug.Log(VolumeBGM);
                Debug.Log(VolumeSFX);
            }
        }
    }
}

