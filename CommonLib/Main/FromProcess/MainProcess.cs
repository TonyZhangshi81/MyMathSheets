using MyMathSheets.CommonLib.Composition;
using MyMathSheets.CommonLib.Logging;
using MyMathSheets.CommonLib.Main.HtmlSupport;
using MyMathSheets.CommonLib.Main.HtmlSupport.Attributes;
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
		private Dictionary<string, Dictionary<string, string>> _htmlMaps = new Dictionary<string, Dictionary<string, string>>();

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
		}

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
			foreach (KeyValuePair<string, Dictionary<string, string>> d in _htmlMaps)
			{
				log.Debug(MessageUtil.GetException(() => MsgResources.I0016L, d.Key));

				// 替換HTML模板中的預留內容（HTML、JS注入操作）
				foreach (KeyValuePair<string, string> m in d.Value)
				{
					log.Debug(MessageUtil.GetException(() => MsgResources.I0015L, m.Key));

					htmlTemplate.Replace(m.Key, m.Value);
				}
			}

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
		/// <param name="controlId">控件ID</param>
		public void TopicCheckedChanged(bool isChecked, string controlId)
		{
			ControlInfo item = _controlList.Where(d => d.ControlId.Equals(controlId)).First();
			if (isChecked)
			{
				// 題型預覽添加
				SetLayoutSettingPreviewList(item.Preview);
				// 取得HTML和JS的替換內容
				Dictionary<string, string> htmlMaps = GetHtmlReplaceContentMaps(item.Preview);
				// 按照題型將所有替換內容裝箱子
				_htmlMaps.Add(controlId, htmlMaps);
			}
			else
			{
				// 題型預覽移除
				_layoutSettingPreviewList.Remove(item.Preview);
				// 題型移除
				_htmlMaps.Remove(controlId);
			}
		}

		/// <summary>
		/// 題型項目選擇事件
		/// </summary>
		/// <param name="isChecked">是否選擇</param>
		/// <param name="preview">控件ID</param>
		public void TopicCheckedChanged(bool isChecked, LayoutSetting.Preview preview)
		{
			if (isChecked)
			{
				// 題型預覽添加
				SetLayoutSettingPreviewList(preview);
				// 取得HTML和JS的替換內容
				Dictionary<string, string> htmlMaps = GetHtmlReplaceContentMaps(preview);
				// 按照題型將所有替換內容裝箱子
				_htmlMaps.Add(preview.ToString(), htmlMaps);
			}
			else
			{
				// 題型預覽移除
				_layoutSettingPreviewList.Remove(preview);
				// 題型移除
				_htmlMaps.Remove(preview.ToString());
			}
		}

		/// <summary>
		/// 取得HTML和JS的替換內容
		/// </summary>
		/// <param name="preview">題型種類</param>
		/// <returns>替換內容</returns>
		private Dictionary<string, string> GetHtmlReplaceContentMaps(LayoutSetting.Preview preview)
		{
			// 構造題型并取得結果
			ParameterBase parameter = OperationStrategyHelper.Instance.Structure(preview);
			// HTML替換內容
			Dictionary<string, string> htmlMaps = new Dictionary<string, string>
			{
				// 題型HTML信息作成并對指定的HTML模板標識進行替換
				{ string.Format("<!--{0}-->", preview.ToString().ToUpper()), MakeHtml.GetHtmlStatement(preview, parameter, out Type supportType) }
			};
			// JS模板內容替換
			MarkJavaScriptReplaceContent(supportType, htmlMaps);
			return htmlMaps;
		}

		/// <summary>
		/// JS模板內容替換
		/// </summary>
		/// <param name="type">題型HTML支持類(類型)</param>
		/// <param name="htmlMaps">替換標籤以及喜歡內容</param>
		private void MarkJavaScriptReplaceContent(Type type, Dictionary<string, string> htmlMaps)
		{
			object[] attribute = type.GetCustomAttributes(typeof(SubstituteAttribute), false);
			attribute.ToList().ForEach(d =>
			{
				var attr = (SubstituteAttribute)d;
				htmlMaps.Add(attr.Source, attr.Target);
			});
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
