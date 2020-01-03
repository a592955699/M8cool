using System;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Windows.Forms;

namespace InfoSniffer
{
    /// <summary>
    /// SnifferPage 的摘要说明。
    /// </summary>
    public class RootPageConfiguration : ListPageConfiguration
    {
        bool _isSniffer = true;
        string _savePath;
        IPlugin _plugin;
        List<ListPageConfiguration> _subPageConfigurations;


        #region 属性
        /// <summary>
        /// 是否采集
        /// </summary>
        public bool IsSniffer
        {
            get { return _isSniffer; }
            set { _isSniffer = value; }
        }

        /// <summary>
        /// 子页配置集合
        /// </summary>
        public List<ListPageConfiguration> SubPageConfigurations
        {
            get
            {
                if (_subPageConfigurations == null)
                    _subPageConfigurations = new List<ListPageConfiguration>();
                return _subPageConfigurations;
            }
        }

        /// <summary>
        /// 第一个子页配置
        /// </summary>
        public override PageConfiguration SubPageConfiguration
        {
            get
            {
                if (_subPageConfigurations.Count > 0)
                    return _subPageConfigurations[0];
                return null;
            }
        }

        /// <summary>
        /// 保存路径
        /// </summary>
        public string SavePath
        {
            get
            {
                if (string.IsNullOrEmpty(_savePath))
                {
                    _savePath = Application.StartupPath + "\\";
                }
                return _savePath;
            }
            set
            {
                _savePath = value;
                if (!_savePath.EndsWith("\\"))
                {
                    _savePath += "\\";
                }
            }
        }

        /// <summary>
        /// 插件
        /// </summary>
        public IPlugin Plugin
        {
            get { return _plugin; }
            set { _plugin = value; }
        }
        #endregion

        public RootPageConfiguration() : base() { }
    }
}
