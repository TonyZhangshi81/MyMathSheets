using System;
using System.Runtime.Serialization;

namespace MyMathSheets.CommonLib.Composition
{
    /// <summary>
    /// LogicComposerで定義が見つからない場合の例外です。
    /// </summary>
    /// <remarks>
    /// この例外はキャッチしてはいけません。
    /// </remarks>
    [Serializable]
    public class ComposerException : Exception
    {
        /// <summary>
        /// 新しいインスタンスを初期化します。
        /// </summary>
        public ComposerException()
            : base()
        {
        }

        /// <summary>
        /// 現在の例外を表すメッセージを指定して新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        public ComposerException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// シリアル化したデータを使用して新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="info">シリアル化されたオブジェクト データを保持するオブジェクト。</param>
        /// <param name="context">転送元または転送先に関するコンテキスト情報。</param>
        protected ComposerException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// 現在の例外の原因である例外を指定して新しいインスタンスを初期化します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        /// <param name="innerException">内部例外</param>
        public ComposerException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
