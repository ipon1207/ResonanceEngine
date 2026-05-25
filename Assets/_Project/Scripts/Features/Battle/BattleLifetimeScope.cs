using Features.Battle.Models;
using Features.Battle.Presenters;
using Features.Battle.Views;
using Domains.Battle;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Features.Battle
{
    public class BattleLifetimeScope : LifetimeScope
    {
        [SerializeField] private BattleCommandView _commandView;
        [SerializeField] private VictoryResultView _victoryView;
        [SerializeField] private GameOverView _gameOverView;

        protected override void Configure(IContainerBuilder builder)
        {
            // Model
            builder.Register<IBattleStateModel, BattleStateModel>(Lifetime.Scoped);
            builder.Register<IActionGaugeCalculator, SimpleActionGaugeCalculator>(Lifetime.Scoped);

            // Views
            builder.RegisterComponent<IBattleCommandView>(_commandView);
            builder.RegisterComponent<IVictoryResultView>(_victoryView);
            builder.RegisterComponent<IGameOverView>(_gameOverView);

            // Presenters (Battle UI用 Factory)
            builder.Register<BattleCharacterPresenterFactory>(Lifetime.Scoped);

            // Presenters (既存)
            // IGameSessionModelとISceneTransitionServiceはRootLifetimeScopeから自動注入される
            builder.RegisterEntryPoint<BattleCommandPresenter>(Lifetime.Scoped);
            builder.RegisterEntryPoint<BattleResultPresenter>(Lifetime.Scoped);
        }
    }
}
