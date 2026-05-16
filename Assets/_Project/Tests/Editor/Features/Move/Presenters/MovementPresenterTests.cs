using Domains.Character;
using Domains.Session;
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
            var sessionMock = Substitute.For<IGameSessionModel>();
            var inputSubject = new Subject<Vector2>();

            var positionProperty = new ReactiveProperty<Vector2>(Vector2.zero);
            modelMock.CurrentPosition.Returns(positionProperty);

            viewMock.OnMoveInput.Returns(inputSubject);
            
            var correctPosition = new Vector2(1.5f, 2.5f);
            viewMock.GetActualPosition().Returns(correctPosition);

            // セッションに保存データがない状態
            sessionMock.HasSavedPosition.Returns(false);

            var presenter = new MovementPresenter(modelMock, viewMock, sessionMock);
            presenter.Initialize();

            inputSubject.OnNext(new Vector2(0, 1));
            presenter.Tick();

            modelMock.Received().Move(Arg.Any<Vector2>(), Arg.Any<float>());
            modelMock.Received().SetPosition(correctPosition);

            presenter.Dispose();
        }

        [Test]
        public void Initialize_WithSavedPosition_SetsModelAndAppliesMovement()
        {
            var modelMock = Substitute.For<IMovementModel>();
            var viewMock = Substitute.For<IMovementView>();
            var sessionMock = Substitute.For<IGameSessionModel>();
            var inputSubject = new Subject<Vector2>();

            var positionProperty = new ReactiveProperty<Vector2>(Vector2.zero);
            modelMock.CurrentPosition.Returns(positionProperty);
            viewMock.OnMoveInput.Returns(inputSubject);

            var savedPosition = new Vector2(10f, 20f);
            sessionMock.HasSavedPosition.Returns(true);
            sessionMock.SavedPlayerPosition.Returns(savedPosition);

            var presenter = new MovementPresenter(modelMock, viewMock, sessionMock);
            presenter.Initialize();

            // 初期化時に保存された座標が復元されていること
            modelMock.Received().SetPosition(savedPosition);
            viewMock.Received().ApplyMovement(savedPosition);

            presenter.Dispose();
        }
    }
}
