using Core.Utilities;
using Domains.Character;
using Features.Move.Views;
using R3;
using System;
using UnityEngine;
using VContainer.Unity;

namespace Features.Move.Presenters
{
    public class MovementPresenter : IInitializable, ITickable, IDisposable
    {
        private readonly IMovementModel _model;
        private readonly IMovementView _view;
        private readonly CompositeDisposable _disposables = new();

        private Vector2 _currentInput;

        public MovementPresenter(IMovementModel model, IMovementView view)
        {
            CheckUtil.ArgNotNull(model);
            CheckUtil.ArgNotNull(view);

            _model = model;
            _view = view;
        }

        public void Initialize()
        {
            // Subscribeを開始する前に、Scene上の実際に初期座標をModelに同期させる
            _model.SetPosition(_view.GetActualPosition());

            // Viewからの入力を監視して最新の入力を保持
            _view.OnMoveInput
                .Subscribe(Input => _currentInput = Input)
                .AddTo(_disposables);

            // Modelの座標変更をViewに反映
            _model.CurrentPosition
                .Subscribe(idealPosition =>
                {
                    _view.ApplyMovement(idealPosition);
                })
                .AddTo(_disposables);
        }

        public void Tick()
        {
            // 入力がある場合のみ移動処理を行う
            if (_currentInput != Vector2.zero)
            {
                _model.Move(_currentInput, Time.deltaTime);
                // CharacterController等による衝突・スライド補正後の実座標をModelへ書き戻す
                // これにより、Modelの座標とViewの実際の座標が常に同期される
                _model.SetPosition(_view.GetActualPosition());
            }
        }

        public void Dispose() => _disposables.Dispose();
    }
}
