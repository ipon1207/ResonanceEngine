using Domains.Character;
using Domains.Enemy;
using Domains.Session;
using Features.Move.Presenters;
using Features.Move.Views;
using NSubstitute;
using NUnit.Framework;
using R3;
using UnityEngine;

namespace Tests.Editor.Features.Move.Presenters
{
    public class EnemyPresenterTests
    {
        [Test]
        public void Tick_UpdatePatrol_And_ChecksEncounter()
        {
            var patrolModel = Substitute.For<IEnemyPatrolModel>();
            var encounterModel = Substitute.For<IEncounterModel>();
            var view = Substitute.For<IEnemyView>();
            var playerModel = Substitute.For<IMovementModel>();
            var sessionModel = Substitute.For<IGameSessionModel>();
            var transitionService = Substitute.For<ISceneTransitionService>();

            patrolModel.Id.Returns(new EnemyId("enemy_01"));
            var patrolPosProp = new ReactiveProperty<Vector2>(new Vector2(5, 0));
            patrolModel.CurrentPosition.Returns(patrolPosProp);

            var isEncounteredProp = new ReactiveProperty<bool>(false);
            encounterModel.IsEncountered.Returns(isEncounteredProp);

            var playerPosProp = new ReactiveProperty<Vector2>(new Vector2(0, 0));
            playerModel.CurrentPosition.Returns(playerPosProp);

            sessionModel.IsEnemyDefeated(Arg.Any<EnemyId>()).Returns(false);

            var presenter = new EnemyPresenter(patrolModel, encounterModel, view, playerModel, sessionModel, transitionService);
            presenter.Initialize();

            presenter.Tick();

            patrolModel.Received().Tick(Arg.Any<float>());
            encounterModel.Received().CheckEncounter(new Vector2(0, 0), new Vector2(5, 0));

            presenter.Dispose();
        }

        [Test]
        public void WhenEncountered_StopsPatrol_SavesState_And_TransitionsToBattle()
        {
            var patrolModel = Substitute.For<IEnemyPatrolModel>();
            var encounterModel = Substitute.For<IEncounterModel>();
            var view = Substitute.For<IEnemyView>();
            var playerModel = Substitute.For<IMovementModel>();
            var sessionModel = Substitute.For<IGameSessionModel>();
            var transitionService = Substitute.For<ISceneTransitionService>();

            var testEnemyId = new EnemyId("enemy_01");
            patrolModel.Id.Returns(testEnemyId);

            var patrolPosProp = new ReactiveProperty<Vector2>(Vector2.zero);
            patrolModel.CurrentPosition.Returns(patrolPosProp);

            var isEncounteredProp = new ReactiveProperty<bool>(false);
            encounterModel.IsEncountered.Returns(isEncounteredProp);

            var testPlayerPos = new Vector2(1, 1);
            var playerPosProp = new ReactiveProperty<Vector2>(testPlayerPos);
            playerModel.CurrentPosition.Returns(playerPosProp);

            sessionModel.IsEnemyDefeated(testEnemyId).Returns(false);

            var presenter = new EnemyPresenter(patrolModel, encounterModel, view, playerModel, sessionModel, transitionService);
            presenter.Initialize();

            // エンカウント状態にする
            isEncounteredProp.Value = true;

            patrolModel.Received().Stop();
            sessionModel.Received().SavePlayerPosition(testPlayerPos);
            sessionModel.Received().RecordDefeatedEnemy(testEnemyId);
            transitionService.Received().LoadBattleScene();

            presenter.Dispose();
        }

        [Test]
        public void Initialize_WhenEnemyIsDefeated_HidesViewAndDoesNotSubscribe()
        {
            var patrolModel = Substitute.For<IEnemyPatrolModel>();
            var encounterModel = Substitute.For<IEncounterModel>();
            var view = Substitute.For<IEnemyView>();
            var playerModel = Substitute.For<IMovementModel>();
            var sessionModel = Substitute.For<IGameSessionModel>();
            var transitionService = Substitute.For<ISceneTransitionService>();

            var testEnemyId = new EnemyId("enemy_01");
            patrolModel.Id.Returns(testEnemyId);

            // 倒されている状態
            sessionModel.IsEnemyDefeated(testEnemyId).Returns(true);

            var presenter = new EnemyPresenter(patrolModel, encounterModel, view, playerModel, sessionModel, transitionService);
            presenter.Initialize();

            // Viewが非表示になったか
            view.Received().Hide();

            // 以降Tickが呼ばれても何もしない
            presenter.Tick();
            patrolModel.DidNotReceive().Tick(Arg.Any<float>());

            presenter.Dispose();
        }
    }
}