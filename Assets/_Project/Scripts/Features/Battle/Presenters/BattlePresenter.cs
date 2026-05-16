using Core.Utilities;
using Domains.Session;
using Features.Battle.Views;
using R3;
using System;
using VContainer.Unity;

namespace Features.Battle.Presenters
{
    public class BattlePresenter : IInitializable, IDisposable
    {
        private readonly IBattleView _view;
        private readonly ISceneTransitionService _transitionService;
        private readonly CompositeDisposable _disposables = new();

        public BattlePresenter(IBattleView view, ISceneTransitionService transitionService)
        {
            CheckUtil.ArgNotNull(view);
            CheckUtil.ArgNotNull(transitionService);

            _view = view;
            _transitionService = transitionService;
        }

        public void Initialize()
        {
            // Viewのボタンクリックを購読し、遷移サービスを呼び出す
            _view.OnReturnButtonClicked
                .Subscribe(_ => _transitionService.LoadMapScene())
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
