using UnityEngine;

namespace Contexts.MainContext
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configurations/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private string boxerConfig;
        [SerializeField] private string bossConfig;
        [Space, SerializeField] private string winText;
        [SerializeField] private string loseText;

        /// <summary>
        /// Return character config
        /// </summary>
        public BoxerConfig GetBoxerConfig => Resources.Load<BoxerConfig>(boxerConfig);

        /// <summary>
        /// Return enemy config
        /// </summary>
        public BossConfig GetBossConfig => Resources.Load<BossConfig>(bossConfig);

        /// <summary>
        /// Return win text
        /// </summary>
        public string GetWinText => winText;

        /// <summary>
        /// Return lose text
        /// </summary>
        public string GetLoseText => loseText;

        public static GameConfig Load()
        {
            var data = Resources.LoadAll<GameConfig>("");
            if (data.Length != 1)
            {
                Debug.LogError($"Can't find <b>{nameof(GameConfig)}</b> asset in Resource Folder!");
            }

            return data[0];
        }
    }
}
