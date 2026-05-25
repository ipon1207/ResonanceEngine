using Domains.Battle;
using Features.Battle.Views;
using VContainer;
using VContainer.Unity;

namespace Features.Battle.Presenters
{
    public class BattleCharacterPresenterFactory
    {
        private readonly IObjectResolver _resolver;

        public BattleCharacterPresenterFactory(IObjectResolver resolver)
        {
            _resolver = resolver;
        }

        public BattleCharacterPresenter Create(BattleCharacterModel model, IBattleCharacterView view)
        {
            // 動的に生成された Model と View を使って Presenter を生成し、DIコンテナの機能（IInitializable 等）を注入・実行させる
            var presenter = new BattleCharacterPresenter(model, view);
            _resolver.Inject(presenter);
            
            // 手動生成した場合はIInitializableが自動で呼ばれないため明示的に呼ぶか、
            // もしくはVContainerの仕組み（Builderで登録してResolve）を使うか。
            // ここではシンプルにInitializeを呼んで返す形とする。
            presenter.Initialize();
            
            return presenter;
        }
    }
}
