using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace GGJ
{
	[CreateAssetMenu(fileName = "Achievements List", menuName = "Achievements list")]
	public class AchievementsManager : ScriptableObject
	{
		private const string AchievementsPath = "achievements.save";

		[SerializeField] private AchievementInfo[] allGameAchievements;

		public event EventHandler AchievementCompleted = delegate { };

		public IReadOnlyCollection<AchievementInfo> Achievements => allGameAchievements;

        private void Awake()
        {
			foreach (var achievement in allGameAchievements)
			{
				achievement.AchievementCompleted += (s, e) => AchievementCompleted(s, e);
			}
        }

        public void Load()
		{
			if (File.Exists(AchievementsPath))
			{
				using StreamReader reader = new(AchievementsPath);
				while (!reader.EndOfStream)
				{
					string line = reader.ReadLine();
                    // Skip comments (maybe someone will write them)
                    if (line.StartsWith("#"))
						continue;
					string[] pair = reader.ReadLine().Split(':');
					var achievement = allGameAchievements.FirstOrDefault(x => x.Id.ToString().Equals(pair[0], StringComparison.InvariantCultureIgnoreCase));
					if (achievement != null)
					{
                        achievement.CurrentProgress = int.Parse(pair[1].Trim());
                    }
				}
			}
		}

		public void Save()
		{
			using StreamWriter writer = new(File.Create(AchievementsPath));
		    foreach (var achievement in allGameAchievements) 
			{
				// Just for the last achievement
				if (achievement.Id == Guid.Empty)
				{
					writer.WriteLine("# The secret achievement is here ^_^");
				}
				writer.WriteLine($"{achievement.Id}:{achievement.CurrentProgress}");
			}
		}
	}
}