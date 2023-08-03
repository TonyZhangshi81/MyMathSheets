using MyMathSheets.CommonLib.Configurations;
using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.FromProcess;
using MyMathSheets.CommonLib.Main.FromProcess.Support;
using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.Policy;
using MyMathSheets.CommonLib.Message;
using MyMathSheets.CommonLib.Util;
using MyMathSheets.WebApi.Properties;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.IO;
using System.Text;

namespace MyMathSheets.WebApi.Main.FromProcess
{
	/// <summary>
	/// API處理類
	/// </summary>
	[Export("Api", typeof(IMainProcess)), PartCreationPolicy(CreationPolicy.NonShared)]
	public class ApiProcess : IMainProcess
	{
		/// <summary>
		/// 替換資源
		/// </summary>
		private readonly Dictionary<string, ConcurrentDictionary<SubstituteType, string>> _htmlMaps
									= new Dictionary<string, ConcurrentDictionary<SubstituteType, string>>();

		/// <summary>題型參數</summary>
		public List<TopicManagement> TopicManagementList { get; set; }

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
		/// HTML 構築類實例
		/// </summary>
		[Import(typeof(IMakeHtml), RequiredCreationPolicy = CreationPolicy.Shared)]
		public IMakeHtml MakeHtml { get; set; }

		/// <summary>
		/// 構造函數
		/// </summary>
		[ImportingConstructor]
		public ApiProcess()
		{
			Stylesheet = new StringBuilder();
			Script = new StringBuilder();
			PrintSettingEvent = new StringBuilder();
			PrintAfterSettingEvent = new StringBuilder();
			ReadyEvent = new StringBuilder();
			MakeCorrectionsEvent = new StringBuilder();
			TictheirPapersEvent = new StringBuilder();
			Content = new StringBuilder();

			TopicManagementList = new List<TopicManagement>();
		}

		/// <summary>
		/// 取得HTML和JS的替換內容
		/// </summary>
		/// <returns>替換內容</returns>
		private void GetHtmlReplaceContentMaps()
		{
			TopicManagementList.ForEach(d =>
			{
				// 構造題型并取得結果
				TopicParameterBase parameter = PolicyHelper.Instance.Structure(d.TopicIdentifier, d.Number);
				// 題型HTML信息作成并對指定的HTML模板標識進行替換
				ConcurrentDictionary<SubstituteType, string> htmlMaps = MakeHtml.GetHtmlReplaceTagDict(d.TopicIdentifier, parameter);

				_htmlMaps.Add(d.TopicIdentifier, htmlMaps);
			});
		}

		/// <summary>
		///
		/// </summary>
		/// <returns></returns>
		public FileInfo Compile()
		{
			// HTML模板存放路徑
			string sourceFileName = System.Web.Hosting.HostingEnvironment.MapPath(ConfigurationUtil.HtmlTemplatePath);
			// 靜態頁面作成后存放的路徑（文件名：日期時間形式）
			string destFileName = System.Web.Hosting.HostingEnvironment.MapPath(ConfigurationUtil.GetKeyValue("HtmlWork")
										+ string.Format(CultureInfo.CurrentCulture, "{0}.html", DateTime.Now.ToString("yyyyMMddHHmmssfff", CultureInfo.CurrentCulture)));

			// 文件移動
			File.Copy(sourceFileName, destFileName);

			StringBuilder htmlTemplate = new StringBuilder();
			// 讀取HTML模板內容
			htmlTemplate.Append(File.ReadAllText(destFileName, Encoding.UTF8));
			// 取得HTML和JS的替換內容
			GetHtmlReplaceContentMaps();

			// 遍歷已選擇的題型
			foreach (KeyValuePair<string, ConcurrentDictionary<SubstituteType, string>> d in _htmlMaps)
			{
				LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0001A, d.Key));

				// 替換HTML模板中的預留內容（HTML、JS注入操作）
				foreach (KeyValuePair<SubstituteType, string> m in d.Value)
				{
					LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0002A, m.Key));

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
			htmlTemplate.Replace("<!--CONTENT-->", Content.Insert(0, IsEncryptScript).AppendLine().ToString());

			LogUtil.LogDebug(MessageUtil.GetMessage(() => MsgResources.I0003L));

			// 保存至靜態頁面
			File.WriteAllText(destFileName, htmlTemplate.ToString(), Encoding.UTF8);

			return (new FileInfo(destFileName));
		}

		/// <summary>
		/// Base64 編碼解碼變量標識作成
		/// </summary>
		/// <returns></returns>
		private static string IsEncryptScript
		{
			get
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendFormat("<script>var _isEncrypt = {0};</script>", ConfigurationUtil.GetIsEncrypt() ? "true" : "false");
				return sb.ToString();
			}
		}
	}
}