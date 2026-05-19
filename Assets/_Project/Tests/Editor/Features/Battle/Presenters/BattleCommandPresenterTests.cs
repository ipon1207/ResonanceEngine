using Features.Battle.Models;
using Features.Battle.Presenters;
using Features.Battle.Views;
using NSubstitute;
using NUnit.Framework;
using R3;

namespace Tests.Editor.Features.Battle.Presenters
{
    public class BattleCommandPresenterTests
    {
        [Test]
        public void Initialize_WhenWinClicked_CallsSetVictory()
        {
            var stateModel = Substitute.For<IBattleStateModel>();
            var view = Substitute.For<IBattleCommandView>();
            var winSubject = new Subject<Unit>();
            var loseSubject = new Subject<Unit>();
            var stateProp = new ReactiveProperty<BattleState>(BattleState.CommandSelection);

            view.OnWinButtonClicked.Returns(winSubject);
            view.OnLoseButtonClicked.Returns(loseSubject);
            stateModel.CurrentState.Returns(stateProp);

            var presenter = new BattleCommandPresenter(stateModel, view);
            presenter.Initialize();

            winSubject.OnNext(Unit.Default);

            stateModel.Received(1).SetVictory();

            presenter.Dispose();
        }

        [Test]
        public void Initialize_WhenLoseClicked_CallsSetGameOver()
        {
            var stateModel = Substitute.For<IBattleStateModel>();
            var view = Substitute.For<IBattleCommandView>();
            var winSubject = new Subject<Unit>();
            var loseSubject = new Subject<Unit>();
            var stateProp = new ReactiveProperty<BattleState>(BattleState.CommandSelection);

            view.OnWinButtonClicked.Returns(winSubject);
            view.OnLoseButtonClicked.Returns(loseSubject);
            stateModel.CurrentState.Returns(stateProp);

            var presenter = new BattleCommandPresenter(stateModel, view);
            presenter.Initialize();

            loseSubject.OnNext(Unit.Default);

            stateModel.Received(1).SetGameOver();

            presenter.Dispose();
        }

        [Test]
        public void Initialize_StateChanges_UpdatesViewVisibility()
        {
            var stateModel = Substitute.For<IBattleStateModel>();
            var view = Substitute.For<IBattleCommandView>();
            var winSubject = new Subject<Unit>();
            var loseSubject = new Subject<Unit>();
            var stateProp = new ReactiveProperty<BattleState>(BattleState.CommandSelection);

            view.OnWinButtonClicked.Returns(winSubject);
            view.OnLoseButtonClicked.Returns(loseSubject);
            stateModel.CurrentState.Returns(stateProp);

            var presenter = new BattleCommandPresenter(stateModel, view);
            
            // Initialize is called, and CurrentState is CommandSelection, so Show() should be called
            presenter.Initialize();
            view.Received(1).Show();

            // Change state to Victory
            stateProp.Value = BattleState.VictoryResult;
            view.Received(1).Hide();

            presenter.Dispose();
        }
    }
}
