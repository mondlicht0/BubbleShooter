using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ElbowGames.Managers
{
	public class LoadingScreen : MonoBehaviour
	{
		[Header("Slider")]
		[SerializeField] private Slider _slider;
		[SerializeField] private string _gameplayLevelName;
		
		private void Start()
		{
			LoadLevel(_gameplayLevelName);
		}
		
		public void LoadLevel(string levelToLoad) 
		{
			StartCoroutine(LoadLeveAsync(levelToLoad));
		}
		
		private IEnumerator LoadLeveAsync(string levelToLoad) 
		{
			AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
			
			while (!loadOperation.isDone) 
			{
				float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
				_slider.value = progressValue;
				yield return null;
			}
		}
	}
}
