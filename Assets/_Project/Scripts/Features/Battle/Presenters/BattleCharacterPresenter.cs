using Domains.Battle;
using Features.Battle.Views;
using R3;
using System;
using VContainer.Unity;

namespace Features.Battle.Presenters
{
    public class BattleCharacterPresenter : IInitializable, IDisposable
    {
        private readonly BattleCharacterModel _model;
        private readonly IBattleCharacterView _view;
        private readonly CompositeDisposable _disposables = new();

        public BattleCharacterPresenter(BattleCharacterModel model, IBattleCharacterView view)
        {
            _model = model;
            _view = view;
        }

        public void Initialize()
        {
            // Viewの初期化（名前のセットなど）
            _view.Initialize(_model.MasterData.DisplayName);

            // HPの変更を購読し、Viewに反映
            _model.CurrentHp
                .Subscribe(hp => _view.UpdateHpBar(hp.Ratio))
                .AddTo(_disposables);

            // 行動ゲージの変更を購読し、Viewに反映
            _model.CurrentGauge
                .Subscribe(gauge => _view.UpdateGaugeBar(gauge.Ratio))
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
