using App.MasterData;
using Domains.MasterData;
using Domains.Session;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App
{
    public class RootLifetimeScope : LifetimeScope
    {
        [Header("Master Data")]
        [SerializeField] private CharacterMasterTable _characterMasterTable;
        [SerializeField] private EnemyMasterTable _enemyMasterTable;

        protected override void Configure(IContainerBuilder builder)
        {
            // プロジェクト全体で共有するセッションデータ（状態）
            builder.Register<IGameSessionModel, GameSessionModel>(Lifetime.Singleton);

            // シーン遷移サービス
            builder.Register<ISceneTransitionService, SceneTransitionService>(Lifetime.Singleton);

            // マスターデータリポジトリ（Singleton: 不変データをアプリ全体で共有）
            builder.Register<ICharacterMasterRepository>(resolver =>
                new ScriptableObjectCharacterMasterRepository(_characterMasterTable.Entries),
                Lifetime.Singleton);
            builder.Register<IEnemyMasterRepository>(resolver =>
                new ScriptableObjectEnemyMasterRepository(_enemyMasterTable.Entries),
                Lifetime.Singleton);
        }
    }
}