﻿using MyMathSheets.CommonLib.Main;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MyMathSheets.CommonLib.Plugin
{
	/// <summary>
	/// 插件管理類
	/// </summary>
	public abstract class PluginsManagerBase : ObjectBase, IPluginsManager
	{
		/// <summary>
		/// 文件名檢索用關鍵字
		/// </summary>
		protected string SearchKeyword { get; set; }

		/// <summary>
		/// 檢索以外的條件
		/// </summary>
		protected List<string> ExcludeAssemblies { get; set; }

		/// <summary>
		/// 模塊文件列表
		/// </summary>
		public IList<PluginInfo> InitPluginsModuleList { get; private set; }

		/// <summary>
		/// <see cref="PluginsManagerBase"/>的構造函數
		/// </summary>
		/// <param name="searchKeyword">文件名檢索用關鍵字</param>
		/// <param name="excludeAssemblies">檢索以外的條件</param>
		protected PluginsManagerBase(string searchKeyword, List<string> excludeAssemblies)
		{
			InitPluginsModuleList = new List<PluginInfo>();
			SearchKeyword = searchKeyword;
			ExcludeAssemblies = excludeAssemblies;
		}

		#region 模塊加載中

		/// <summary>
		/// 模塊加載中
		/// </summary>
		/// <param name="sender">程序集對象</param>
		/// <param name="current">當前加載對象計數</param>
		public delegate void ModelLoadingEventHandler(object sender, int current);

		/// <summary>
		/// 模塊加載事件
		/// </summary>
		public event ModelLoadingEventHandler ModelLoading;

		/// <summary>
		///
		/// </summary>
		/// <param name="current"></param>
		protected virtual void OnModelLoading(int current)
		{
			if (ModelLoading == null)
			{
				return;
			}
			ModelLoading(this, current);
		}

		#endregion 模塊加載中

		#region 模塊加載完畢

		/// <summary>
		/// 模塊加載完畢
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="current">當前加載對象計數</param>
		public delegate void ModelLoadCompleteEventHandler(object sender, int current);

		/// <summary>
		/// 模塊加載完畢事件
		/// </summary>
		public event ModelLoadCompleteEventHandler ModelLoadComplete;

		/// <summary>
		///
		/// </summary>
		/// <param name="current"></param>
		protected virtual void OnModelLoadComplete(int current)
		{
			if (ModelLoadComplete == null)
			{
				return;
			}
			ModelLoadComplete(this, current);
		}

		#endregion 模塊加載完畢

		#region 模塊加載之前

		/// <summary>
		/// 模塊加載之前
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="modelCount">預加載對象合計數</param>
		public delegate void ModelPreLoadEventHandler(object sender, int modelCount);

		/// <summary>
		/// 模塊加載之前事件
		/// </summary>
		public event ModelPreLoadEventHandler ModelPreLoad;

		/// <summary>
		///
		/// </summary>
		/// <param name="modelCount"></param>
		protected virtual void OnModelPreLoad(int modelCount)
		{
			if (ModelPreLoad == null)
			{
				return;
			}
			ModelPreLoad(this, modelCount);
		}

		#endregion 模塊加載之前

		/// <summary>
		/// 插件信息初始化
		/// </summary>
		public virtual void Initialize()
		{
			// 檢索插件信息
			var plugins = SearchPlugins();
			var action = new Action<FileInfo>(f =>
			{
				InitPluginsModuleList.Add(new PluginInfo(f));
			});
			plugins.ToList().ForEach(f => action(f));
		}

        /// <summary>
        /// 在插件加載列表中返回指定題型識別ID的插件對象
        /// </summary>
        /// <param name="topicId">題型識別ID</param>
        /// <returns>插件對象</returns>
        public PluginInfo FindByTopicId(string topicId)
        {
            return InitPluginsModuleList.FirstOrDefault(d => d.TopicIdentifier.Equals(topicId));
        }

        /// <summary>
        /// 檢索並獲取插件文件信息
        /// </summary>
        /// <returns>插件文件信息集合</returns>
        public virtual IList<FileInfo> SearchPlugins()
		{
			var fileList = new List<FileInfo>();
			GetDirectoryFiles(Configurations.ConfigurationUtil.ApplicationRootPath, fileList);

			return fileList;
		}

		/// <summary>
		/// 獲取指定目錄下所有文件的列表信息
		/// </summary>
		/// <param name="path">指定目錄</param>
		/// <param name="fileList">文件列表信息</param>
		private void GetDirectoryFiles(string path, List<FileInfo> fileList)
		{
			DirectoryInfo folder = new DirectoryInfo(path);

			// 遍歷文件
			foreach (var fi in from _ in folder.GetFiles(SearchKeyword)
							   where !ExcludeAssemblies.Contains(_.Name)
							   orderby _.Name.Length descending
							   select _)
			{
				fileList.Add(fi);
			}

			// 遍歷文件夾
			folder.GetDirectories().ToList().ForEach(d => GetDirectoryFiles(d.FullName, fileList));
		}

		/// <summary>
		/// 資源釋放
		/// </summary>
		protected override void DisposeManaged()
		{
			if (InitPluginsModuleList != null)
			{
				InitPluginsModuleList.Clear();
			}
		}
	}
}