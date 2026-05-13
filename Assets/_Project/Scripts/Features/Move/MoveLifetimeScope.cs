using Domains.Character;
using Features.Move.Presenters;
using Features.Move.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Features.Move
{
    public class MoveLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private PlayerMovementView _playerMovementView;
        [SerializeField]
        private float _playerSpeed = 5f;

        protected override void Configure(IContainerBuilder builder)
        {
            // 1.Modelの登録
            // 値オブジェクト(Speed)を生成し、PlayerMovementModelのコンストラクタに渡して登録
            builder.Register<IMovementModel>(resolver =>
                new PlayerMovementModel(new Speed(_playerSpeed), Vector2.zero),
                Lifetime.Scoped);

            // 2.Viewの登録
            // Scene上でアタッチされたMonoBehaviourのインスタンスを注入
            builder.RegisterComponent<IMovementView>(_playerMovementView);

            // 3.Presenterの登録
            // IInitializable, ITickable, IDisposableのエントリーポイントとして登録
            builder.RegisterEntryPoint<MovementPresenter>(Lifetime.Scoped);
        }
    }
}
