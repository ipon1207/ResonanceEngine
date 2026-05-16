using Domains.Session;
using Features.Battle.Presenters;
using Features.Battle.Views;
using NSubstitute;
using NUnit.Framework;
using R3;

namespace Tests.Editor.Features.Battle.Presenters
{
    public class BattlePresenterTests
    {
        private IBattleView _mockView;
        private ISceneTransitionService _mockTransitionService;
        private BattlePresenter _presenter;
        private Subject<Unit> _onReturnButtonSubject;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _mockView = Substitute.For<IBattleView>();
            _mockTransitionService = Substitute.For<ISceneTransitionService>();

            // Viewのイベントを制御するためのSubjectを作成
            _onReturnButtonSubject = new Subject<Unit>();
            _mockView.OnReturnButtonClicked.Returns(_onReturnButtonSubject);

            _presenter = new BattlePresenter(_mockView, _mockTransitionService);
            
            // Presenterの初期化（Subscribeの実行）
            _presenter.Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            _presenter.Dispose();
            _onReturnButtonSubject.Dispose();
        }

        [Test]
        public void Initialize_WhenReturnButtonClicked_CallsLoadMapScene()
        {
            // Act
            // Viewでボタンが押されたことをシミュレート
            _onReturnButtonSubject.OnNext(Unit.Default);

            // Assert
            // 遷移サービスの LoadMapScene() が1回だけ呼ばれたことを検証
            _mockTransitionService.Received(1).LoadMapScene();
        }
    }
}
