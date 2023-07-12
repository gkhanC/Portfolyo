using System;
using HypeFire.Library.Utilities.Extensions.Object;
using Managers;
using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class UIController : MonoBehaviour
	{
		public static UIController GloballAccess { get; private set; }

		public GameObject successPanel;
		public GameObject failedPanel;
		public GameObject timePanel;

		public TextMeshProUGUI successScoreText;

		private float _levelLifeTime = 60f;
		private float _levelTimer = 0f;
		private float _remainingTime;
		private float _percent;
		public Slider timeBar;
		public TextMeshProUGUI timeTex;

		private void Awake()
		{
			GloballAccess = this;
		}

		private void Start()
		{
			if (LevelManager.GloballAccess.IsNotNull())
			{
				_levelLifeTime = LevelManager.GloballAccess.levelLifeTime;
			}
		}


		private void Update()
		{
			if (LevelManager.GloballAccess.IsNotNull())
			{
				if (LevelManager.GloballAccess.isLevelStarted)
				{
					if (_levelTimer < _levelLifeTime)
					{
						_levelTimer += Time.deltaTime;
						_percent = 1 - (_levelTimer / _levelLifeTime);
						_remainingTime = _levelLifeTime - _levelTimer;
					}
					else
					{
						_percent = 0;
						_remainingTime = 0;
					}

					timeBar.value = _percent;
					timeTex.text = _remainingTime.ToString("0.0");

					if (_remainingTime <= 0)
					{
						LevelManager.GloballAccess.LevelFailed(0);
					}
				}
			}
		}


		public void NextLevel()
		{
			LevelManager.GloballAccess.NextLevel();
		}

		public void Retry()
		{
			LevelManager.GloballAccess.Retry();
		}

		public void OnFailedMenu()
		{
			timePanel.SetActive(false);
			failedPanel.SetActive(true);
		}

		public void OnSuccessMenu(int score)
		{
			successScoreText.text = $"Score: {score.ToString()}";
			successPanel.SetActive(true);
			timePanel.SetActive(false);
		}
	}
}