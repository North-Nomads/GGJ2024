using System;
using UnityEngine;

namespace GGJ.Quests
{
    public class QuestStepGoalString
    {
        public string Verb { get; set; }
        public string Goal { get; set; }
        public string CurrentCount { get; set; }
        public string GoalCount { get; set; }
        public string Result => $"{Verb} {Goal} {CurrentCount}/{GoalCount}";
    }
}