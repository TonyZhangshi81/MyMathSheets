using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.OperationStrategy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Properties;
using MyMathSheets.CommonLib.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace MyMathSheets.CommonLib.Main.FromProcess
{
	/// <summary>
	/// 畫面處理類
	/// </summary>
	[Export(typeof(IMainProcess))]
	public class MainProcess : IMainProcess
	{
		private static Log log = Log.LogReady(typeof(MainProcess));

		/// <summary>
		/// 替換資源
		/// </summary>
		private Dictionary<string, Dictionary<SubstituteType, string>> _htmlMaps = new Dictionary<string, Dictionary<SubstituteType, string>>();

		/// <summary>
		/// 
		/// </summary>
		[Import(typeof(IMakeHtml))]
		public IMakeHtml MakeHtml { get; set; }

		/// <summary>
		/// 題型預覽列表
		/// </summary>
		private List<LayoutSetting.Preview> _layoutSettingPreviewList;
		/// <summary>
		/// 題型預覽列表
		/// </summary>
		List<LayoutSetting.Preview> IMainProcess.LayoutSettingPreviewList
		{
			get
			{
				return _layoutSettingPreviewList;
			}
			set
			{
				_layoutSettingPreviewList = value;
			}
		}

		/// <summary>
		/// 構造函數
		/// </summary>
		[ImportingConstructor]
		public MainProcess()
		{
			Stylesheet = new StringBuilder();
			Script = new StringBuilder();
			PrintSettingEvent = new StringBuilder();
			PrintAfterSettingEvent = new StringBuilder();
			ReadyEvent = new StringBuilder();
			MakeCorrectionsEvent = new StringBuilder();
			TictheirPapersEvent = new StringBuilder();
			Content = new StringBuilder();
		}

		/// <summary>樣式庫引用注入點</summary>
		private StringBuilder Stylesheet { get; set; }
		/// <summary>腳本引用注入</summary>
		private StringBuilder Script { get; set; }
		/// <summary>打印前設置事件注入</summary>
		private StringBuilder PrintSettingEvent { get; set; }
		/// <summary>打印后設置事件注入</summary>
		private StringBuilder PrintAfterSettingEvent { get; set; }
		/// <summary>準備事件注入</summary>
		private StringBuilder ReadyEvent { get; set; }
		/// <summary>答題訂正事件注入</summary>
		private StringBuilder MakeCorrectionsEvent { get; set; }
		/// <summary>交卷事件注入</summary>
		private StringBuilder TictheirPapersEvent { get; set; }
		/// <summary>題型正文注入</summary>
		private StringBuilder Content { get; set; }

		/// <summary>
		/// 出題按鍵點擊事件
		/// </summary>
		/// <returns>靜態頁面文件名</returns>
		public string SureClick()
		{
			// HTML模板存放路徑
			string sourceFileName = Path.GetFullPath(ConfigurationManager.AppSettings.Get("Template"));
			// 靜態頁面作成后存放的路徑（文件名：日期時間形式）
			string destFileName = Path.GetFullPath(ConfigurationManager.AppSettings.Get("HtmlWork") + string.Format("{0}.html", DateTime.Now.ToString("yyyyMMddHHmmssfff")));
			// 文件移動
			File.Copy(sourceFileName, destFileName);

			StringBuilder htmlTemplate = new StringBuilder();
			// 讀取HTML模板內容
			htmlTemplate.Append(File.ReadAllText(destFileName, Encoding.UTF8));
			// 遍歷已選擇的題型
			foreach (KeyValuePair<string, Dictionary<SubstituteType, string>> d in _htmlMaps)
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0016L, d.Key));

				// 替換HTML模板中的預留內容（HTML、JS注入操作）
				foreach (KeyValuePair<SubstituteType, string> m in d.Value)
				{
					log.Debug(MessageUtil.GetException(() => MsgResources.I0015L, m.Key));

					switch (m.Key)
					{
						// 樣式庫引用注入
						case SubstituteType.Stylesheet:
							Stylesheet.AppendLine(m.Value);
							break;
						// 腳本引用注入
						case SubstituteType.Script:
							Script.AppendLine(m.Value);
							break;
						// 打印前設置事件注入
						case SubstituteType.PrintSettingEvent:
							PrintSettingEvent.AppendLine(m.Value);
							break;
						// 打印后設置事件注入
						case SubstituteType.PrintAfterSettingEvent:
							PrintAfterSettingEvent.AppendLine(m.Value);
							break;
						// 準備事件注入
						case SubstituteType.ReadyEvent:
							ReadyEvent.AppendLine(m.Value);
							break;
						// 交卷事件注入
						case SubstituteType.TheirPapersEvent:
							TictheirPapersEvent.AppendLine(m.Value);
							break;
						// 答題訂正事件注入
						case SubstituteType.MakeCorrectionsEvent:
							MakeCorrectionsEvent.AppendLine(m.Value);
							break;
						// 題型正文注入
						case SubstituteType.Content:
							Content.AppendLine(m.Value);
							break;
						default:
							break;
					}
				}
			}
			// 樣式庫注入
			htmlTemplate.Replace("<!--STYLESHEET-->", Stylesheet.ToString());
			// 腳本注入
			htmlTemplate.Replace("<!--SCRIPT-->", Script.ToString());
			// 打印前設置事件注入
			htmlTemplate.Replace("// PRINTSETTING", PrintSettingEvent.ToString());
			// 打印后設置事件注入
			htmlTemplate.Replace("// PRINTAFTERSETTING", PrintAfterSettingEvent.ToString());
			// 題型準備事件注入
			htmlTemplate.Replace("// READY", ReadyEvent.ToString());
			// 題型訂正事件注入
			htmlTemplate.Replace("// MAKECORRECTIONS", MakeCorrectionsEvent.ToString());
			// 題型交卷事件注入
			htmlTemplate.Replace("// TICTHEIRPAPERS", TictheirPapersEvent.ToString());
			// 題型正文注入
			htmlTemplate.Replace("<!--CONTENT-->", Content.ToString());

			log.Debug(MessageUtil.GetException(() => MsgResources.I0017L));

			// 保存至靜態頁面
			File.WriteAllText(destFileName, htmlTemplate.ToString(), Encoding.UTF8);

			return destFileName;
		}

		/// <summary>
		/// 題型選擇校驗
		/// </summary>
		/// <returns>題型選項未選擇:false  已選擇:true</returns>
		public bool ChooseCheck()
		{
			// 選題情況
			if (_layoutSettingPreviewList == null || _layoutSettingPreviewList.Count == 2)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// 題型預覽列表設置
		/// </summary>
		/// <param name="name">題型名稱</param>
		public void SetLayoutSettingPreviewList(LayoutSetting.Preview name)
		{
			// 初期化
			if (_layoutSettingPreviewList == null)
			{
				_layoutSettingPreviewList = new List<LayoutSetting.Preview>
				{
					// 標題區
					LayoutSetting.Preview.Title,
					// 答題區
					LayoutSetting.Preview.Ready
				};
			}
			// 如果列表中不存在，則添加在答題區之前
			if (!_layoutSettingPreviewList.Any(d => d == name))
			{
				_layoutSettingPreviewList.Insert(_layoutSettingPreviewList.Count - 1, name);
			}
		}

		/// <summary>
		/// 題型項目選擇事件
		/// </summary>
		/// <param name="isChecked">是否選擇</param>
		/// <param name="expression">用以獲取控件基本信息對象</param>
		public void TopicCheckedChanged(bool isChecked, Expression<Func<ControlInfo>> expression)
		{
			ControlInfo info = expression.Compile()();
			if (isChecked)
			{
				// 題型預覽添加
				SetLayoutSettingPreviewList(info.Preview);
				// 取得HTML和JS的替換內容
				Dictionary<SubstituteType, string> htmlMaps = GetHtmlReplaceContentMaps(info.Preview);
				// 按照題型將所有替換內容裝箱子
				_htmlMaps.Add(info.ControlId, htmlMaps);
			}
			else
			{
				// 題型預覽移除
				_layoutSettingPreviewList.Remove(info.Preview);
				// 題型移除
				_htmlMaps.Remove(info.ControlId);
			}
		}

		/// <summary>
		/// 取得HTML和JS的替換內容
		/// </summary>
		/// <param name="preview">題型種類</param>
		/// <returns>替換內容</returns>
		private Dictionary<SubstituteType, string> GetHtmlReplaceContentMaps(LayoutSetting.Preview preview)
		{
			// 構造題型并取得結果
			ParameterBase parameter = OperationStrategyHelper.Instance.Structure(preview);
			// 題型HTML信息作成并對指定的HTML模板標識進行替換
			Dictionary<SubstituteType, string> htmlMaps = MakeHtml.GetHtmlStatement(preview, parameter);
			return htmlMaps;
		}

		private List<ControlInfo> _controlList;
		/// <summary>
		/// 控件基本屬性取得
		/// </summary>
		public List<ControlInfo> ControlList
		{
			get
			{
				if (_controlList == null)
				{
					_controlList = new List<ControlInfo>();

					int indexX = 1, indexY = 1;
					ComposerFactory.AssemblyInfoCache.ToList().ForEach(d =>
					{
						_controlList.Add(new ControlInfo()
						{
							IndexX = indexX,
							IndexY = indexY,
							// 控件ID設定
							ControlId = d.Value.Preview.ToString(),
							// 控件標題設定
							Title = d.Value.Description,
							// 題型類型
							Preview = d.Value.Preview
						});

						if (indexX == 3)
						{
							indexY++;
							indexX = 0;
						}
						indexX++;
					});
				}
				return _controlList;
			}
		}
	}

	/// <summary>
	/// 控件基本信息
	/// </summary>
	public class ControlInfo
	{
		/// <summary>
		/// 坐標X
		/// </summary>
		public int IndexX { get; set; }
		/// <summary>
		/// 坐標Y
		/// </summary>
		public int IndexY { get; set; }
		/// <summary>
		/// 控件ID
		/// </summary>
		public string ControlId { get; set; }

		/// <summary>
		/// 控件標題
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 題型類型
		/// </summary>
		public LayoutSetting.Preview Preview { get; set; }
	}
}
