using Domains.Enemy;
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
        public void Initialize_WhenStateChangesToVictory_RecordsDefeatedEnemy_And_ShowsCorrectView()
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
            
            // テスト用にエンカウントした敵のIDをセット
            var testEnemyId = new EnemyId("enemy_001");
            sessionModel.CurrentEncounterEnemyId.Returns(testEnemyId);

            var presenter = new BattleResultPresenter(stateModel, victoryView, gameOverView, sessionModel, transitionService);
            presenter.Initialize();

            // Act
            stateProp.Value = BattleState.VictoryResult;

            // Assert
            victoryView.Received(1).Show();
            sessionModel.Received(1).RecordDefeatedEnemy(testEnemyId);

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
            sessionModel.DidNotReceive().ClearSessionData();

            presenter.Dispose();
        }

        [Test]
        public void GameOverReturnClicked_ClearsSessionData_And_CallsLoadMapScene()
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

            sessionModel.Received(1).ClearSessionData();
            transitionService.Received(1).LoadMapScene();

            presenter.Dispose();
        }
    }
}
