using UnityEngine.SceneManagement;

namespace Domains.Session
{
    public class SceneTransitionService : ISceneTransitionService
    {
        public void LoadBattleScene()
        {
            SceneManager.LoadScene("BattleScene");
        }

        public void LoadMapScene()
        {
            SceneManager.LoadScene("SampleScene"); // 仮でSampleSceneをマップとみなす
        }
    }
}
