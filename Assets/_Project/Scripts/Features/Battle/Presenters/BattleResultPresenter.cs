using Core.Utilities;
using Domains.Session;
using Features.Battle.Models;
using Features.Battle.Views;
using R3;
using System;
using VContainer.Unity;

namespace Features.Battle.Presenters
{
    public class BattleResultPresenter : IInitializable, IDisposable
    {
        private readonly IBattleStateModel _stateModel;
        private readonly IVictoryResultView _victoryView;
        private readonly IGameOverView _gameOverView;
        private readonly IGameSessionModel _sessionModel;
        private readonly ISceneTransitionService _transitionService;
        private readonly CompositeDisposable _disposables = new();

        public BattleResultPresenter(
            IBattleStateModel stateModel,
            IVictoryResultView victoryView,
            IGameOverView gameOverView,
            IGameSessionModel sessionModel,
            ISceneTransitionService transitionService)
        {
            CheckUtil.ArgNotNull(stateModel);
            CheckUtil.ArgNotNull(victoryView);
            CheckUtil.ArgNotNull(gameOverView);
            CheckUtil.ArgNotNull(sessionModel);
            CheckUtil.ArgNotNull(transitionService);

            _stateModel = stateModel;
            _victoryView = victoryView;
            _gameOverView = gameOverView;
            _sessionModel = sessionModel;
            _transitionService = transitionService;
        }

        public void Initialize()
        {
            // 初期状態は非表示
            _victoryView.Hide();
            _gameOverView.Hide();

            // 状態に応じたViewの表示切替
            _stateModel.CurrentState
                .Subscribe(state =>
                {
                    if (state == BattleState.VictoryResult) _victoryView.Show();
                    if (state == BattleState.GameOver) _gameOverView.Show();
                })
                .AddTo(_disposables);

            // 勝利からの戻る処理
            _victoryView.OnReturnButtonClicked
                .Subscribe(_ => _transitionService.LoadMapScene())
                .AddTo(_disposables);

            // 敗北からの戻る処理（座標リセットを含む）
            _gameOverView.OnReturnButtonClicked
                .Subscribe(_ =>
                {
                    _sessionModel.ClearSavedPosition();
                    _transitionService.LoadMapScene();
                })
                .AddTo(_disposables);
        }

        public void Dispose() => _disposables.Dispose();
    }
}
