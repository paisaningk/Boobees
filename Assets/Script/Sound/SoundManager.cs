using System;
using UnityEngine;

namespace Sound
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private SoundClip[] soundClips;

        public static SoundManager Instance { get; private set; }
        //private AudioSource audioSource;

        public void Awake()
        {
            Debug.Assert(soundClips != null && soundClips.Length != 0, "Sound clips need to be setup");
            //audioSource = GetComponent<AudioSource>();

            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            Play(Sound.BGM);
        }

        public enum Sound
        {
            BGM,
            PlayerMovement,
            PlayerDash,
            PlayerHit1,
            PlayerHit2,
            PlayerHit3,
            WitchAttack,
            EnemyTakeHit,
            PlayerTakeHit,
            Coin,
            SpawnEnemy,
            Shop,
            ExecutionerAttack,
            TankAttack,
            Pickup,
            TalkWithShop,
            OpenShop,
            NoMoney,
            ThankYou,
            PlayerDie,
            PlayerDieBGM
        }

        [Serializable]
        public class SoundClip
        {
            public Sound sound;
            public AudioClip audioClip;
            [Range(0, 1)] public float soundVolume;
            public bool loop = false;
            [HideInInspector]
            public AudioSource audioSource;
        }

        //public void Play(AudioSource audioSource, Sound sound)
        public void Play(Sound sound)
        {
            var soundClip = GetSoundClip(sound);
            if (soundClip.audioSource == null)
            {
                soundClip.audioSource = gameObject.AddComponent<AudioSource>();
            }
            soundClip.audioSource.clip = soundClip.audioClip;
            soundClip.audioSource.volume = soundClip.soundVolume;
            soundClip.audioSource.loop = soundClip.loop;
            soundClip.audioSource.Play();
        }

        public void Playfrompause(Sound sound)
        {
            var soundClip = GetSoundClip(sound);
            soundClip.audioSource.Play();
        }

        private SoundClip GetSoundClip(Sound sound)
        {
            foreach (var soundClip in soundClips)
            {
                if (soundClip.sound == sound)
                {
                    return soundClip;
                }
            }
            return null;
            //return default(SoundClip);
        }
        
        public void Stop(Sound sound)
        {
            var soundClip = GetSoundClip(sound);
            soundClip.audioSource.Stop();
        }
    }
}