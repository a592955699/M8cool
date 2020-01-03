using System;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
using System.Text;
namespace InfoSniffer
{
	/// <summary>
	/// SnifferConfig 的摘要说明。
	/// </summary>
    public class SnifferConfig
    {
        static XmlDocument xmlDoc;
        static string[] _rootPageNames;
        static string defaultSnfFileName = "DefaultSniffer.xml";

        /// <summary>
        /// 根页名称集合
        /// </summary>
        public static string[] RootPageNames
        {
            get { return _rootPageNames; }
        }

        /// <summary>
        /// 构造
        /// </summary>
        static SnifferConfig()
        {
            //OpenSnfFile(defaultSnfFileName);
        }

        public static void OpenSnfFile(string file)
        {
            xmlDoc = new XmlDocument();
            xmlDoc.Load(file);

            XmlNodeList rootXmlNodeList = xmlDoc.DocumentElement.ChildNodes;

            _rootPageNames = new string[rootXmlNodeList.Count];

            for (int i = 0; i < rootXmlNodeList.Count; i++)
            {
                _rootPageNames[i] = rootXmlNodeList[i].Attributes["PageName"].Value;
            }
        }

        public static RootPageConfiguration GetRootPageConfiguration()
        {
            return GetRootPageConfiguration(null);
        }

        public static RootPageConfiguration GetRootPageConfiguration(string rootPageName)
        {
            XmlNode rootConfNode;
            if (rootPageName == null)
                rootConfNode = xmlDoc.SelectNodes("//SnifferRootPage")[0];
            else
                rootConfNode = xmlDoc.SelectSingleNode(string.Format("//SnifferRootPage[@PageName=\"{0}\"]", rootPageName));

            if (rootConfNode == null)
                return null;

            RootPageConfiguration rootConf = new RootPageConfiguration();

            rootConf.PageName = rootConfNode.Attributes["PageName"].Value;
            rootConf.PageUrl = rootConfNode.Attributes["PageUrl"].Value;
            rootConf.PageType = PageType.ListPage;

            if (rootConfNode.Attributes["IsSniffer"] != null)
                rootConf.IsSniffer = bool.Parse(rootConfNode.Attributes["IsSniffer"].Value);
            if (rootConfNode.Attributes["SavePath"] != null)
                rootConf.SavePath = rootConfNode.Attributes["SavePath"].Value;
            if (rootConfNode.Attributes["PluginType"] != null)
            {
                Type pluginType = Type.GetType(rootConfNode.Attributes["PluginType"].Value);
                rootConf.Plugin = (IPlugin)pluginType.GetConstructor(new Type[0]).Invoke(new object[0]);
            }

            XmlNode snifferUrlItemNode = rootConfNode.SelectSingleNode("SnifferSubPageUrlItem");

            if (snifferUrlItemNode != null)
            {
                rootConf.SnifferSubPageUrlItem = CreateSnifferUrlItem(snifferUrlItemNode);

            }
            else if (rootConf.IsSniffer)
            {
                throw new System.Exception(string.Format("{0} 页设置为需要采集，但是没有配置 SnifferUrlItem 节点", rootConf.PageName));
            }


            XmlNodeList subPageNodes = rootConfNode.SelectNodes("SnifferPage");

            foreach (XmlNode listPageNode in subPageNodes)
            {
                rootConf.SubPageConfigurations.Add(CreateListPageConfiguration(rootConf, listPageNode));
            }

            return rootConf;
        }

        public static RegexString CreateRegexString(XmlNode regexStringNode)
        {
            RegexString regexString = new RegexString();
            regexString.Expression = regexStringNode.InnerText;
            if (regexStringNode.Attributes["ValueGroupIndex"] != null)
                regexString.ValueGroupIndex = int.Parse(regexStringNode.Attributes["ValueGroupIndex"].Value);
            return regexString;
        }

        public static SnifferUrlItem CreateSnifferUrlItem(XmlNode snifferUrlItemNode)
        {
            SnifferUrlItem snifferUrlItem = new SnifferUrlItem();
            snifferUrlItem.Expression = snifferUrlItemNode.InnerText;
            snifferUrlItem.UrlGroupIndex = int.Parse(snifferUrlItemNode.Attributes["UrlGroupIndex"].Value);
            snifferUrlItem.TitleGroupIndex = int.Parse(snifferUrlItemNode.Attributes["TitleGroupIndex"].Value);
            if (snifferUrlItemNode.Attributes["UrlFormat"] != null)
                snifferUrlItem.UrlFormat = snifferUrlItemNode.Attributes["UrlFormat"].Value;
            return snifferUrlItem;

        }

        private static ListPageConfiguration CreateListPageConfiguration(PageConfiguration parent, XmlNode pageConfNode)
        {
            XmlNode commonConfigNode = null;

            if (pageConfNode.Attributes["Config"] != null)
            {
                commonConfigNode = pageConfNode.ParentNode.SelectSingleNode(string.Format("CommonConfig[@Name='{0}']", pageConfNode.Attributes["Config"].Value));
            }

            ListPageConfiguration pageConf = new ListPageConfiguration(parent);

            pageConf.PageName = pageConfNode.Attributes["PageName"].Value;
            pageConf.PageUrl = pageConfNode.Attributes["PageUrl"].Value;

            if (pageConfNode.Attributes["PageType"] != null)
            {
                pageConf.PageType = (PageType)Enum.Parse(typeof(PageType), pageConfNode.Attributes["PageType"].Value, false);
            }
            else if (commonConfigNode.Attributes["PageType"] != null)
            {
                pageConf.PageType = (PageType)Enum.Parse(typeof(PageType), commonConfigNode.Attributes["PageType"].Value, false);
            }


            if (pageConfNode.Attributes["PageQuery"] != null)
            {
                pageConf.PageQuery = pageConfNode.Attributes["PageQuery"].Value;
            }
            else if (commonConfigNode != null && commonConfigNode.Attributes["PageQuery"] != null)
            {
                pageConf.PageQuery = commonConfigNode.Attributes["PageQuery"].Value;
            }

            if (pageConfNode.Attributes["ReplacePageQuery"] != null)
            {
                pageConf.ReplacePageQuery = pageConfNode.Attributes["ReplacePageQuery"].Value;
            }
            else if (commonConfigNode != null && commonConfigNode.Attributes["ReplacePageQuery"] != null)
            {
                pageConf.ReplacePageQuery = commonConfigNode.Attributes["ReplacePageQuery"].Value;
            }

            if (pageConfNode.Attributes["PageIndexFormat"] != null)
            {
                pageConf.PageIndexFormat = pageConfNode.Attributes["PageIndexFormat"].Value;
            }
            else if (commonConfigNode != null && commonConfigNode.Attributes["PageIndexFormat"] != null)
            {
                pageConf.PageIndexFormat = commonConfigNode.Attributes["PageIndexFormat"].Value;
            }

            if (pageConfNode.Attributes["PageIndexSeed"] != null)
            {
                pageConf.PageIndexSeed = int.Parse(pageConfNode.Attributes["PageIndexSeed"].Value);
            }
            else if (commonConfigNode != null && commonConfigNode.Attributes["PageIndexSeed"] != null)
            {
                pageConf.PageIndexSeed = int.Parse(commonConfigNode.Attributes["PageIndexSeed"].Value);
            }

            if (pageConfNode.Attributes["PageIndexStep"] != null)
            {
                pageConf.PageIndexStep = int.Parse(pageConfNode.Attributes["PageIndexStep"].Value);
            }
            else if (commonConfigNode != null && commonConfigNode.Attributes["PageIndexStep"] != null)
            {
                pageConf.PageIndexStep = int.Parse(commonConfigNode.Attributes["PageIndexStep"].Value);
            }

            if (pageConfNode.Attributes["PageMethod"] != null)
            {
                pageConf.PageMethod = (PageMethod)Enum.Parse(typeof(PageMethod), pageConfNode.Attributes["PageMethod"].Value);
            }
            else if (commonConfigNode != null && commonConfigNode.Attributes["PageMethod"] != null)
            {
                pageConf.PageMethod = (PageMethod)Enum.Parse(typeof(PageMethod), commonConfigNode.Attributes["PageMethod"].Value);
            }

            if (pageConfNode.Attributes["EndPageDetermineRegex"] != null)
            {
                pageConf.EndPageDetermineRegex = pageConfNode.Attributes["EndPageDetermineRegex"].Value;
            }
            else if (commonConfigNode != null && commonConfigNode.Attributes["EndPageDetermineRegex"] != null)
            {
                pageConf.EndPageDetermineRegex = commonConfigNode.Attributes["EndPageDetermineRegex"].Value;
            }

            if (pageConfNode.Attributes["For"] != null)
            {
                pageConf.For = bool.Parse(pageConfNode.Attributes["For"].Value);
            }
            else if (commonConfigNode != null && commonConfigNode.Attributes["For"] != null)
            {
                pageConf.For = bool.Parse(commonConfigNode.Attributes["For"].Value);
            }

            if (pageConfNode.Attributes["Encoding"] != null)
            {
                pageConf.Encoding = Encoding.GetEncoding(pageConfNode.Attributes["Encoding"].Value);
            }
            else if (commonConfigNode != null && commonConfigNode.Attributes["Encoding"] != null)
            {
                pageConf.Encoding = Encoding.GetEncoding(commonConfigNode.Attributes["Encoding"].Value);
            }

            XmlNode snifferUrlItemNode = pageConfNode.SelectSingleNode("SnifferSubPageUrlItem");
            if (commonConfigNode != null && snifferUrlItemNode == null)
                snifferUrlItemNode = commonConfigNode.SelectSingleNode("SnifferSubPageUrlItem");

            if (snifferUrlItemNode != null)
            {
                pageConf.SnifferSubPageUrlItem = CreateSnifferUrlItem(snifferUrlItemNode);

            }
            else
            {
                throw new System.Exception(string.Format("{0} 页没有配置 SnifferUrlItem 节点", pageConf.PageName));
            }

            XmlNode subPageNode = pageConfNode.SelectSingleNode("SnifferPage");
            if (commonConfigNode != null && subPageNode == null)
                subPageNode = commonConfigNode.SelectSingleNode("SnifferPage");

            if (subPageNode != null)
            {
                if (subPageNode.Attributes["PageType"].Value == "DetailPage")
                    pageConf.SubPageConfiguration = CreateDetailPageConfiguration(pageConf, pageConfNode, subPageNode);
                else
                    pageConf.SubPageConfiguration = CreateListPageConfiguration(pageConf, subPageNode);
            }

            return pageConf;

        }


        private static DetailPageConfiguration CreateDetailPageConfiguration(PageConfiguration parent, XmlNode parentConfNode, XmlNode pageConfNode)
        {
            XmlNode commonListConfigNode = null;
            XmlNode commonConfigNode = null;

            if (parentConfNode.Attributes["Config"] != null)
            {
                commonListConfigNode = parentConfNode.ParentNode.SelectSingleNode(string.Format("CommonConfig[@Name='{0}']", parentConfNode.Attributes["Config"].Value));
                commonConfigNode = commonListConfigNode.SelectSingleNode("SnifferPage");
            }

            DetailPageConfiguration detailPageConf = new DetailPageConfiguration(parent);

            detailPageConf.PageName = pageConfNode.Attributes["PageName"].Value;

            if (pageConfNode.Attributes["PageUrl"] != null)
                detailPageConf.PageUrl = pageConfNode.Attributes["PageUrl"].Value;
            else if (commonConfigNode != null && commonConfigNode.Attributes["PageUrl"] != null)
                detailPageConf.PageUrl = commonConfigNode.Attributes["PageUrl"].Value;
            if (pageConfNode.Attributes["PageType"] != null)
                detailPageConf.PageType = (PageType)Enum.Parse(typeof(PageType), pageConfNode.Attributes["PageType"].Value, false);
            else if (commonConfigNode != null && commonConfigNode.Attributes["PageType"] != null)
                detailPageConf.PageType = (PageType)Enum.Parse(typeof(PageType), commonConfigNode.Attributes["PageType"].Value, false);
            if (pageConfNode.Attributes["Encoding"] != null)
                detailPageConf.Encoding = Encoding.GetEncoding(pageConfNode.Attributes["Encoding"].Value);
            else if (commonConfigNode != null && commonConfigNode.Attributes["Encoding"] != null)
                detailPageConf.Encoding = Encoding.GetEncoding(commonConfigNode.Attributes["Encoding"].Value);


            //翻页
            if (pageConfNode.Attributes["PageQuery"] != null)
                detailPageConf.PageQuery = pageConfNode.Attributes["PageQuery"].Value;
            else if (commonConfigNode != null && commonConfigNode.Attributes["PageQuery"] != null)
                detailPageConf.PageQuery = commonConfigNode.Attributes["PageQuery"].Value;
            if (pageConfNode.Attributes["ReplacePageQuery"] != null)
                detailPageConf.ReplacePageQuery = pageConfNode.Attributes["ReplacePageQuery"].Value;
            else if (commonConfigNode != null && commonConfigNode.Attributes["ReplacePageQuery"] != null)
                detailPageConf.ReplacePageQuery = commonConfigNode.Attributes["ReplacePageQuery"].Value;
            if (pageConfNode.Attributes["PageIndexFormat"] != null)
                detailPageConf.PageIndexFormat = pageConfNode.Attributes["PageIndexFormat"].Value;
            else if (commonConfigNode != null && commonConfigNode.Attributes["PageIndexFormat"] != null)
                detailPageConf.PageIndexFormat = commonConfigNode.Attributes["PageIndexFormat"].Value;
            if (pageConfNode.Attributes["PageStartIndex"] != null)
                detailPageConf.PageStartIndex = int.Parse(pageConfNode.Attributes["PageStartIndex"].Value);
            else if (commonConfigNode != null && commonConfigNode.Attributes["PageStartIndex"] != null)
                detailPageConf.PageStartIndex = int.Parse(commonConfigNode.Attributes["PageStartIndex"].Value);

            if (pageConfNode.Attributes["PageIndexSeed"] != null)
                detailPageConf.PageIndexSeed = int.Parse(pageConfNode.Attributes["PageIndexSeed"].Value);
            else if (commonConfigNode != null && commonConfigNode.Attributes["PageIndexSeed"] != null)
                detailPageConf.PageIndexSeed = int.Parse(commonConfigNode.Attributes["PageIndexSeed"].Value);

            if (pageConfNode.Attributes["PageIndexStep"] != null)
                detailPageConf.PageIndexStep = int.Parse(pageConfNode.Attributes["PageIndexStep"].Value);
            else if (commonConfigNode != null && commonConfigNode.Attributes["PageIndexStep"] != null)
                detailPageConf.PageIndexStep = int.Parse(commonConfigNode.Attributes["PageIndexStep"].Value);

            if (pageConfNode.Attributes["EndPageDetermineRegex"] != null)
                detailPageConf.EndPageDetermineRegex = pageConfNode.Attributes["EndPageDetermineRegex"].Value;
            else if (commonConfigNode != null && commonConfigNode.Attributes["EndPageDetermineRegex"] != null)
                detailPageConf.EndPageDetermineRegex = commonConfigNode.Attributes["EndPageDetermineRegex"].Value;

            if (pageConfNode.Attributes["PageMethod"] != null)
                detailPageConf.PageMethod = (PageMethod)Enum.Parse(typeof(PageMethod), pageConfNode.Attributes["PageMethod"].Value);
            else if (commonConfigNode != null && commonConfigNode.Attributes["PageMethod"] != null)
                detailPageConf.PageMethod = (PageMethod)Enum.Parse(typeof(PageMethod), commonConfigNode.Attributes["PageMethod"].Value);





            XmlNode snifferUrlItemNode = pageConfNode.SelectSingleNode("SnifferSubPageUrlItem");
            if (snifferUrlItemNode == null && commonConfigNode != null && commonConfigNode.Attributes["SnifferSubPageUrlItem"] != null)
                snifferUrlItemNode = commonConfigNode.SelectSingleNode("SnifferSubPageUrlItem");
            if (snifferUrlItemNode != null)
                detailPageConf.SnifferSubPageUrlItem = CreateSnifferUrlItem(snifferUrlItemNode);


            List<XmlNodeList> lstItemLists = new List<XmlNodeList>();

            XmlNodeList itemNodes = pageConfNode.SelectNodes("SnifferItem");
            if (itemNodes != null)
                lstItemLists.Add(itemNodes);

            if (commonConfigNode != null)
            {
                XmlNodeList comItemNodes = commonConfigNode.SelectNodes("SnifferItem");
                if (comItemNodes != null)
                    lstItemLists.Add(comItemNodes);
            }

            for (int i = 0; i < lstItemLists.Count; i++)
            {
                itemNodes = lstItemLists[i];
                foreach (XmlNode itemNode in itemNodes)
                {
                    string itemName = itemNode.Attributes["ItemName"].Value;
                    bool bool1 = false;
                    foreach (SnifferItem itm in detailPageConf.SnifferItems)
                    {
                        if (itm.ItemName == itemName)
                            bool1 = true;
                    }

                    if (!bool1)
                    {
                        SnifferItem item = new SnifferItem();

                        item.ItemName = itemNode.Attributes["ItemName"].Value;

                        if (itemNode.Attributes["SaveImage"] != null)
                            item.SaveImage = bool.Parse(itemNode.Attributes["SaveImage"].Value);
                        if (itemNode.Attributes["SaveImagesPath"] != null)
                            item.SaveImagesPath = itemNode.Attributes["SaveImagesPath"].Value;
                        if (itemNode.Attributes["ImageUrlPath"] != null)
                            item.ImageUrlPath = itemNode.Attributes["ImageUrlPath"].Value;
                        if (itemNode.Attributes["IsClearHTML"] != null)
                            item.IsClearHTML = bool.Parse(itemNode.Attributes["IsClearHTML"].Value);
                        if (itemNode.Attributes["IsUrl"] != null)
                            item.IsUrl = bool.Parse(itemNode.Attributes["IsUrl"].Value);
                        if (itemNode.Attributes["UrlToAbs"] != null)
                            item.UrlToAbs = bool.Parse(itemNode.Attributes["UrlToAbs"].Value);
                        if (itemNode.Attributes["Separator"] != null)
                            item.Separator = itemNode.Attributes["Separator"].Value;
                        if (itemNode.Attributes["ClearAElement"] != null)
                            item.ClearAElement = bool.Parse(itemNode.Attributes["ClearAElement"].Value);
                        if (itemNode.Attributes["MutiPage"] != null)
                            item.MutiPage = bool.Parse(itemNode.Attributes["MutiPage"].Value);
                        if (itemNode.Attributes["MutiPageSeparator"] != null)
                            item.MutiPageSeparator = itemNode.Attributes["MutiPageSeparator"].Value;

                        XmlNode ndClearRegexString = itemNode.SelectSingleNode("ClearRegexString");
                        if(ndClearRegexString!=null)
                            item.ClearRegexString = ndClearRegexString.InnerText;

                        XmlNode ndDefaultValue = itemNode.SelectSingleNode("DefaultValue");
                        if (ndDefaultValue != null)
                            item.DefaultValue = ndDefaultValue.InnerText;

                        XmlNode ndRegexString = itemNode.SelectSingleNode("RegexString");
                        if (ndRegexString != null)
                            item.RegexString = CreateRegexString(ndRegexString);

                        detailPageConf.SnifferItems.Add(item);
                    }

                }
            }


            XmlNodeList subPageNodes = pageConfNode.SelectNodes("SnifferPage");
            foreach (XmlNode subPageNode in subPageNodes)
            {
                detailPageConf.SubPageConfigurations.Add(CreateDetailPageConfiguration(detailPageConf, pageConfNode, subPageNode));
            }

            if (commonConfigNode != null)
            {
                subPageNodes = commonConfigNode.SelectNodes("SnifferPage");
                foreach (XmlNode subPageNode in subPageNodes)
                {
                    detailPageConf.SubPageConfigurations.Add(CreateDetailPageConfiguration(detailPageConf, pageConfNode, subPageNode));
                }
            }

            return detailPageConf;
        }
    }
}
