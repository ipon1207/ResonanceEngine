using Domains.Battle;
using Domains.Character;
using Domains.MasterData;
using Features.Battle.Presenters;
using Features.Battle.Views;
using NSubstitute;
using NUnit.Framework;
using R3;

namespace Tests.Editor.Features.Battle
{
    public class BattleCharacterPresenterTests
    {
        [Test]
        public void Initialize_SubscribesToModelAndUpdatesView()
        {
            // Arrange
            // 1. Modelの準備（仮のマスターデータ）
            var master = new CharacterMasterData(
                new CharacterId("hero"),
                "勇者",
                new BaseStats(100, 50, 10, 10, 10, 10, 10, 10)
            );
            var model = new BattleCharacterModel(master);

            // 2. Viewのモック化
            var viewMock = Substitute.For<IBattleCharacterView>();

            // 3. Presenterの生成
            var presenter = new BattleCharacterPresenter(model, viewMock);

            // Act
            // IInitializable の処理をエミュレート
            presenter.Initialize();

            // Assert
            // Viewの初期化が呼ばれたか
            viewMock.Received(1).Initialize("勇者");
            
            // 初期状態のHP/ゲージ比率がViewに反映されているか（HP満タン、ゲージ0）
            viewMock.Received(1).UpdateHpBar(1.0f);
            viewMock.Received(1).UpdateGaugeBar(0.0f);

            // Act 2: Modelの状態を変更してみる
            model.ApplyDamage(20); // HPが80になる (ratio: 0.8)

            // Assert 2
            // Subscribeによって自動的にViewが更新されたか
            viewMock.Received(1).UpdateHpBar(0.8f);

            // クリーンアップ
            presenter.Dispose();
            model.Dispose();
        }
    }
}
