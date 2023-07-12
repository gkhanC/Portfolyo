using System;
using HypeFire.Library.Utilities.Singleton;
using UnityEngine;

namespace HypeFire.Templates.Runner
{
	public class RunnerGameManager : MonoBehaviourSingleton<RunnerGameManager>
	{
		public static RunnerGameManager GloballAccess { get; set; }
		[field: SerializeField] private ScoreData scoreData = new ScoreData();

		public ScoreData score
		{
			get => scoreData;
			set => scoreData = value;
		}

		public override void Awake()
		{
			base.Awake();
			GloballAccess = this;
		}

		public void AddScore(float score)
		{
			this.score += score;
		}

		[Serializable]
		public class ScoreData
		{
			public float score;

			public ScoreData()
			{
			}

			public ScoreData(float score)
			{
				this.score = score;
			}

			public static ScoreData operator +(ScoreData data, int score)
			{
				return new ScoreData(data.score + score);
			}

			public static ScoreData operator +(ScoreData data, float score)
			{
				return new ScoreData(data.score + score);
			}

			public static ScoreData operator -(ScoreData data, int score)
			{
				return new ScoreData(data.score - score);
			}

			public static ScoreData operator -(ScoreData data, float score)
			{
				return new ScoreData(data.score - score);
			}

			public static int operator +(int score, ScoreData data)
			{
				data.score += score;
				return (int)data.score;
			}

			public static float operator +(float score, ScoreData data)
			{
				data.score += score;
				return data.score;
			}

			public static int operator -(int score, ScoreData data)
			{
				data.score -= score;
				return (int)data.score;
			}

			public static float operator -(float score, ScoreData data)
			{
				data.score -= score;
				return data.score;
			}
		}
	}
}