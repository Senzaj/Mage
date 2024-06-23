using System.Collections.Generic;
using Agava.YandexGames;
using Sources.Modules.YandexSDK.Scripts;
using UnityEngine;

namespace Sources.Modules.UI.Scripts.LeaderBoard
{
    public class LeaderList : MonoBehaviour
    {
        [SerializeField] private YandexSdk _yandex;
        [SerializeField] private string _leaderboardName = "Leaderboard";
        [SerializeField] private ProfilePanel _resultTemplate;
        [SerializeField] private GameObject _content;
        [SerializeField] private int _minPlayersCount = 1;
        [SerializeField] private int _maxPlayersCount;

        private List<ProfilePanel> _results = new();

        public void SetLeaderboardScore(int score)
        {
            if (_yandex.IsInitialized)
            {
                Leaderboard.GetPlayerEntry(_leaderboardName, (result) =>
                {
                    if (result == null || result.score <= score)
                        Leaderboard.SetScore(_leaderboardName, score);
                });
            }
        }

        public void ShowResults()
        {
            if (_yandex.IsInitialized)
            {
                if (PlayerAccount.HasPersonalProfileDataPermission == false)
                    _yandex.RequestPersonalProfileDataPermission();

                GetLeaderboardEntries(_leaderboardName);
            }
        }

        public void Clear()
        {
            if (_results.Count > 0)
            {
                while (_results.Count > 0)
                {
                    Destroy(_results[0].gameObject);
                    _results.Remove(_results[0]);
                }
            }
        }
        
        private void GetLeaderboardEntries(string boardName)
        {
            Leaderboard.GetEntries(boardName, (result) =>
            {
                var results = result.entries.Length;
                results = Mathf.Clamp(results, _minPlayersCount, _maxPlayersCount);

                for (var i = 0; i < results; i++)
                    AddResult(result.entries[i].player.publicName, result.entries[i].score);
            });
        }

        private void AddResult(string userName, int score)
        {
            ProfilePanel result = Instantiate(_resultTemplate, _content.transform);
            _results.Add(result);
            result.SetParams(userName, score);
        }
    }
}
