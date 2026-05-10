using System;

namespace Core.Utilities
{
    public class CheckUtil
    {
        /// <summary>
        /// 引数がnullでないことを確認
        /// </summary>
        /// <typeparam name="T">メソッド・コンストラクタの参照型</typeparam>
        /// <param name="arg">メソッド・コンストラクタの引数</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ArgNotNull<T>(T arg) where T : class
        {
            if (arg == null) throw new ArgumentNullException("引数がnullです");
        }

        /// <summary>
        /// 引数が0以上であることを確認
        /// </summary>
        /// <param name="arg">メソッド・コンストラクタの引数</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ZeroOrMore(int arg)
        {
            if (arg < 0) throw new ArgumentOutOfRangeException("引数が0未満です");
        }

        /// <summary>
        /// 引数が0.0以上であることを確認
        /// </summary>
        /// <param name="arg">メソッド・コンストラクタの引数</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ZeroOrMore(float arg)
        {
            if (arg < 0) throw new ArgumentOutOfRangeException("引数が0.0未満です");
        }
    }
}