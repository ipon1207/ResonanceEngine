using Core.Utilities;
using Features.Battle.Models;
using Features.Battle.Views;
using R3;
using System;
using VContainer.Unity;

namespace Features.Battle.Presenters
{
    public class BattleCommandPresenter : IInitializable, IDisposable
    {
        private readonly IBattleStateModel _stateModel;
        private readonly IBattleCommandView _view;
        private readonly CompositeDisposable _disposables = new();

        public BattleCommandPresenter(IBattleStateModel stateModel, IBattleCommandView view)
        {
            CheckUtil.ArgNotNull(stateModel);
            CheckUtil.ArgNotNull(view);

            _stateModel = stateModel;
            _view = view;
        }

        public void Initialize()
        {
            _view.OnWinButtonClicked
                .Subscribe(_ => _stateModel.SetVictory())
                .AddTo(_disposables);

            _view.OnLoseButtonClicked
                .Subscribe(_ => _stateModel.SetGameOver())
                .AddTo(_disposables);

            _stateModel.CurrentState
                .Subscribe(state =>
                {
                    if (state == BattleState.CommandSelection)
                    {
                        _view.Show();
                    }
                    else
                    {
                        _view.Hide();
                    }
                })
                .AddTo(_disposables);
        }

        public void Dispose() => _disposables.Dispose();
    }
}
