using Features.Battle.Presenters;
using Features.Battle.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Features.Battle
{
    public class BattleLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private BattleView _battleView;
        protected override void Configure(IContainerBuilder builder)
        {
            // Viewの登録
            builder.RegisterComponent<IBattleView>(_battleView);

            // Presenterの登録
            // ISceneTransitionServiceは、RootLifetimeScopeから自動で注入される
            builder.RegisterEntryPoint<BattlePresenter>(Lifetime.Scoped);
        }
    }
}
