using System;

namespace MyMathSheets.CommonLib.Main
{
	/// <summary>
	/// 對象幾類（實現資源接口）
	/// </summary>
	public abstract class ObjectBase : IDisposable
	{
		private bool isDisposed = false;

		/// <summary>
		/// 資源釋放
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// 資源釋放
		/// </summary>
		/// <param name="disposing">是否正在釋放</param>
		/// <remarks>資源自動回收時觸發析構函數，以下資源自動釋放(即isDisposed=true)</remarks>
		protected virtual void Dispose(bool disposing)
		{
			if (isDisposed) return;

			// 正在釋放資源
			if (disposing)
			{
				DisposeManaged();
			}

			isDisposed = true;
		}

		/// <summary>
		/// 資源釋放
		/// </summary>
		~ObjectBase()
		{
			Dispose(false);
		}

		/// <summary>
		/// 資源釋放
		/// </summary>
		protected virtual void DisposeManaged()
		{
		}
	}
}