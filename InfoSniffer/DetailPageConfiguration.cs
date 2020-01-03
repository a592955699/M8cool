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
	/// SnifferPage ��ժҪ˵����
	/// </summary>
    public class DetailPageConfiguration : ListPageConfiguration
    {

        List<DetailPageConfiguration> _subPageConfigurations;
        List<SnifferItem> _snifferItems;
        SnifferUrlItem _snifferSubPageUrlItem;
        int _pageStartIndex = 1;

        #region ����
        /// <summary>
        /// �ɼ����
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
        /// ��ҳ���ü���
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
        /// ��ʼҳ��index
        /// </summary>
        public int PageStartIndex
        {
            get { return _pageStartIndex; }
            set { _pageStartIndex = value; }
        }

        ///// <summary>
        ///// �ɼ���ҳUrl��
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
        /// �������ݱ�
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

            //�����ҳ���ֶ�
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
