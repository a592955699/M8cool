using System;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;

namespace InfoSniffer
{
	/// <summary>
	/// SnifferPage 的摘要说明。
	/// </summary>
    public class DetailPageConfiguration : ListPageConfiguration
    {

        List<DetailPageConfiguration> _subPageConfigurations;
        List<SnifferItem> _snifferItems;
        SnifferUrlItem _snifferSubPageUrlItem;
        int _pageStartIndex = 1;

        #region 属性
        /// <summary>
        /// 采集项集合
        /// </summary>
        public List<SnifferItem> SnifferItems
        {
            get
            {
                if (_snifferItems == null)
                    _snifferItems = new List<SnifferItem>();
                return _snifferItems;
            }
        }

        /// <summary>
        /// 子页配置集合
        /// </summary>
        public List<DetailPageConfiguration> SubPageConfigurations
        {
            get
            {
                if (_subPageConfigurations == null)
                    _subPageConfigurations = new List<DetailPageConfiguration>();
                return _subPageConfigurations;

            }
        }

        /// <summary>
        /// 开始页的index
        /// </summary>
        public int PageStartIndex
        {
            get { return _pageStartIndex; }
            set { _pageStartIndex = value; }
        }

        ///// <summary>
        ///// 采集本页Url项
        ///// </summary>
        //public SnifferUrlItem SnifferSubPageUrlItem
        //{
        //    get { return _snifferSubPageUrlItem; }
        //    set { _snifferSubPageUrlItem = value; }
        //}
        #endregion

        public DetailPageConfiguration() : base() { }

        public DetailPageConfiguration(PageConfiguration parent)
            : base(parent)
        {
        }

        /// <summary>
        /// 创建数据表
        /// </summary>
        /// <returns></returns>
        public DataTable CreateDataTable()
        {
            DataTable table = new DataTable();

            DataColumn col = new DataColumn("ID", typeof(int));
            col.AutoIncrement = true;
            col.AutoIncrementSeed = 1;
            col.AutoIncrementStep = 1;
            table.Columns.Add(col);
            table.PrimaryKey = new DataColumn[] { col };

            List<DetailPageConfiguration> confis = new List<DetailPageConfiguration>();

            AddDetailPageConfiguration(confis, this);

            //添加子页的字段
            foreach (DetailPageConfiguration confi in confis)
            {
                foreach (SnifferItem item in confi.SnifferItems)
                {
                    if (!table.Columns.Contains(item.ItemName))
                        table.Columns.Add(item.ItemName, typeof(string));
                }
            }
            return table;
        }

        private void AddDetailPageConfiguration(List<DetailPageConfiguration> confis, DetailPageConfiguration conf)
        {
            confis.Add(conf);
            foreach (DetailPageConfiguration c in conf.SubPageConfigurations)
            {
                AddDetailPageConfiguration(confis, c);
            }
        }
    }
}
