using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GGJ
{
    public class FastTravelUI : MonoBehaviour
    {
        public void OnLocationPressed(LocationInfo locationIndex)
        {
            SceneManager.LoadScene(locationIndex.ToString());
        }

        [Serializable]
        public enum GameLocation
        {
            Lobby,
            Fountain,
            Home,
            Market,
            River,
            Dungeon,
            Snowfields
        }
    }
}