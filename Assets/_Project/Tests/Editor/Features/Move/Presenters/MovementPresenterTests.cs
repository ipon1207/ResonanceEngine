using Domains.Character;
using Features.Move.Presenters;
using Features.Move.Views;
using NSubstitute;
using NUnit.Framework;
using R3;
using UnityEngine;

namespace Tests.Editor.Features.Move.Presenters
{
    public class MovementPresenterTests
    {
        [Test]
        public void Tick_WithInput_CallsModelMove_AndSyncsActualPosition()
        {
            var modelMock = Substitute.For<IMovementModel>();
            var viewMock = Substitute.For<IMovementView>();
            var inputSubject = new Subject<Vector2>();

            // ViewのOnMoveInputがSubjectを返すように設定
            viewMock.OnMoveInput.Returns(inputSubject);
            
            // Viewから返される補正後の実座標（衝突スライド後）を定義
            var correctPosition = new Vector2(1.5f, 2.5f);
            viewMock.GetActualPosition().Returns(correctPosition);

            var presenter = new MovementPresenter(modelMock, viewMock);
            presenter.Initialize();

            // Tick（毎フレーム処理）を呼ぶ
            inputSubject.OnNext(new Vector2(0, 1)); // 前進入力
            presenter.Tick();

            // Modelに入力が渡されたか
            modelMock.Received().Move(Arg.Any <Vector2>(), Arg.Any<float>());
            // 衝突後の実座標がModelに書き戻されたか
            modelMock.Received().SetPosition(correctPosition);

            presenter.Dispose();
        }
    }
}