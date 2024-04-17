using UnityEngine;
using UnityEngine.UI;

namespace ElbowGames.Managers
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }
        [SerializeField] private AudioSource _soundSource;
        [SerializeField] private AudioSource _musicSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            else
            {
                Destroy(Instance);
                Instance = this;
            }
        }

        public void PlaySfx(AudioClip clip)
        {
            _soundSource.PlayOneShot(clip);
        }

        public void ChangeSound(Slider slider)
        {
            _soundSource.volume = slider.value;
        }

        public void ChangeMusic(Slider slider)
        {
            _musicSource.volume = slider.value;
        }
    }

}