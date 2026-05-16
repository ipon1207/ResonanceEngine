using Domains.Session;
using VContainer;
using VContainer.Unity;

namespace App
{
    public class RootLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // プロジェクト全体で共有するセッションデータ（状態）
            builder.Register<IGameSessionModel, GameSessionModel>(Lifetime.Singleton);

            // シーン遷移サービス
            builder.Register<ISceneTransitionService, SceneTransitionService>(Lifetime.Singleton);
        }
    }
}