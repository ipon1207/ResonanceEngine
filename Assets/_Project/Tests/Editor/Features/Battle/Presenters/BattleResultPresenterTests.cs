using Domains.Session;
using Features.Battle.Models;
using Features.Battle.Presenters;
using Features.Battle.Views;
using NSubstitute;
using NUnit.Framework;
using R3;

namespace Tests.Editor.Features.Battle.Presenters
{
    public class BattleResultPresenterTests
    {
        [Test]
        public void Initialize_HidesViewsInitially()
        {
            var stateModel = Substitute.For<IBattleStateModel>();
            var victoryView = Substitute.For<IVictoryResultView>();
            var gameOverView = Substitute.For<IGameOverView>();
            var sessionModel = Substitute.For<IGameSessionModel>();
            var transitionService = Substitute.For<ISceneTransitionService>();

            var stateProp = new ReactiveProperty<BattleState>(BattleState.CommandSelection);
            stateModel.CurrentState.Returns(stateProp);
            victoryView.OnReturnButtonClicked.Returns(new Subject<Unit>());
            gameOverView.OnReturnButtonClicked.Returns(new Subject<Unit>());

            var presenter = new BattleResultPresenter(stateModel, victoryView, gameOverView, sessionModel, transitionService);
            presenter.Initialize();

            victoryView.Received(1).Hide();
            gameOverView.Received(1).Hide();

            presenter.Dispose();
        }

        [Test]
        public void Initialize_WhenStateChanges_ShowsCorrectView()
        {
            var stateModel = Substitute.For<IBattleStateModel>();
            var victoryView = Substitute.For<IVictoryResultView>();
            var gameOverView = Substitute.For<IGameOverView>();
            var sessionModel = Substitute.For<IGameSessionModel>();
            var transitionService = Substitute.For<ISceneTransitionService>();

            var stateProp = new ReactiveProperty<BattleState>(BattleState.CommandSelection);
            stateModel.CurrentState.Returns(stateProp);
            victoryView.OnReturnButtonClicked.Returns(new Subject<Unit>());
            gameOverView.OnReturnButtonClicked.Returns(new Subject<Unit>());

            var presenter = new BattleResultPresenter(stateModel, victoryView, gameOverView, sessionModel, transitionService);
            presenter.Initialize();

            stateProp.Value = BattleState.VictoryResult;
            victoryView.Received(1).Show();

            stateProp.Value = BattleState.GameOver;
            gameOverView.Received(1).Show();

            presenter.Dispose();
        }

        [Test]
        public void VictoryReturnClicked_CallsLoadMapScene()
        {
            var stateModel = Substitute.For<IBattleStateModel>();
            var victoryView = Substitute.For<IVictoryResultView>();
            var gameOverView = Substitute.For<IGameOverView>();
            var sessionModel = Substitute.For<IGameSessionModel>();
            var transitionService = Substitute.For<ISceneTransitionService>();

            var stateProp = new ReactiveProperty<BattleState>(BattleState.CommandSelection);
            stateModel.CurrentState.Returns(stateProp);
            
            var victoryReturnSubject = new Subject<Unit>();
            victoryView.OnReturnButtonClicked.Returns(victoryReturnSubject);
            gameOverView.OnReturnButtonClicked.Returns(new Subject<Unit>());

            var presenter = new BattleResultPresenter(stateModel, victoryView, gameOverView, sessionModel, transitionService);
            presenter.Initialize();

            victoryReturnSubject.OnNext(Unit.Default);

            transitionService.Received(1).LoadMapScene();
            sessionModel.DidNotReceive().ClearSavedPosition();

            presenter.Dispose();
        }

        [Test]
        public void GameOverReturnClicked_ClearsSavedPosition_And_CallsLoadMapScene()
        {
            var stateModel = Substitute.For<IBattleStateModel>();
            var victoryView = Substitute.For<IVictoryResultView>();
            var gameOverView = Substitute.For<IGameOverView>();
            var sessionModel = Substitute.For<IGameSessionModel>();
            var transitionService = Substitute.For<ISceneTransitionService>();

            var stateProp = new ReactiveProperty<BattleState>(BattleState.CommandSelection);
            stateModel.CurrentState.Returns(stateProp);
            
            victoryView.OnReturnButtonClicked.Returns(new Subject<Unit>());
            var gameOverReturnSubject = new Subject<Unit>();
            gameOverView.OnReturnButtonClicked.Returns(gameOverReturnSubject);

            var presenter = new BattleResultPresenter(stateModel, victoryView, gameOverView, sessionModel, transitionService);
            presenter.Initialize();

            gameOverReturnSubject.OnNext(Unit.Default);

            sessionModel.Received(1).ClearSavedPosition();
            transitionService.Received(1).LoadMapScene();

            presenter.Dispose();
        }
    }
}
