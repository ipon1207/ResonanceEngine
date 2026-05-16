using Domains.Character;
using Domains.Enemy;
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

            var patrolPosProp = new ReactiveProperty<Vector2>(new Vector2(5, 0));
            patrolModel.CurrentPosition.Returns(patrolPosProp);

            var isEncounteredProp = new ReactiveProperty<bool>(false);
            encounterModel.IsEncountered.Returns(isEncounteredProp);

            var playerPosProp = new ReactiveProperty<Vector2>(new Vector2(0, 0));
            playerModel.CurrentPosition.Returns(playerPosProp);

            var presenter = new EnemyPresenter(patrolModel, encounterModel, view, playerModel);

            presenter.Tick();

            // パトロールロジックが呼ばれ、かつプレイヤー座標との距離更新が行われたか
            patrolModel.Received().Tick(Arg.Any<float>());
            encounterModel.Received().CheckEncounter(new Vector2(0, 0), new Vector2(5, 0));

            presenter.Dispose();
        }

        [Test]
        public void WhenEncountered_StopPatrol()
        {
            var patrolModel = Substitute.For<IEnemyPatrolModel>();
            var encounterModel = Substitute.For<IEncounterModel>();
            var view = Substitute.For<IEnemyView>();
            var playerModel = Substitute.For<IMovementModel>();

            var patrolPosProp = new ReactiveProperty<Vector2>(Vector2.zero);
            patrolModel.CurrentPosition.Returns(patrolPosProp);

            var isEncounteredProp = new ReactiveProperty<bool>(false);
            encounterModel.IsEncountered.Returns(isEncounteredProp);

            var playerPosProp = new ReactiveProperty<Vector2>(Vector2.zero);
            playerModel.CurrentPosition.Returns(playerPosProp);

            var presenter = new EnemyPresenter(patrolModel, encounterModel, view, playerModel);
            presenter.Initialize();

            // エンカウント状態にする
            // Modelの状態変化イベントを発火
            isEncounteredProp.Value = true;

            patrolModel.Received().Stop();

            presenter.Dispose();
        }
    }
}