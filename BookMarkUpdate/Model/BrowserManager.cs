using BookMarkUpdate.Enum;
using System;
using System.Collections.Generic;
using System.Xml;

namespace BookMarkUpdate
{
    /// <summary>
    /// 浏览器书签管理
    /// </summary>
    public abstract class BrowserManager
    {
        /// <summary>
        /// 导出浏览器的书签保存到文件中
        /// </summary>
        public abstract void DeriveBookmark();

        /// <summary>
        /// 从文件中导入书签
        /// </summary>
        /// <param name="bakPath">书签备份文件路径</param>
        /// <param name="import">书签的导入方式</param>
        public abstract void ImportBookmark(string bakPath, ImportEnum import);

        /// <summary>
        /// 根据浏览器收藏夹内容生成XML文件
        /// </summary>
        /// <param name="folderList">浏览器收藏夹内容</param>
        /// <param name="fileName">保存的文件名称</param>
        public void BuilderXml(List<FolderModel> folderList, string fileName)
        {

            XmlDocument document = new XmlDocument();

            XmlDeclaration dec = document.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            document.AppendChild(dec);

            XmlElement root = document.CreateElement("FavoritesColumn");
            document.AppendChild(root);

            foreach (var item in folderList)
            {
                BuilderByFolderNode(document, item, root);
            }

            document.Save(Environment.CurrentDirectory + "/" + fileName + ".xml");
        }

        /// <summary>
        /// 根据收藏夹中的文件夹生成XML节点
        /// </summary>
        /// <param name="document"></param>
        /// <param name="model"></param>
        /// <param name="parentNode"></param>
        private void BuilderByFolderNode(XmlDocument document, FolderModel model, XmlNode parentNode)
        {
            XmlNode currentNode = document.CreateNode(XmlNodeType.Element, "Folder", "");

            XmlAttribute attribute = document.CreateAttribute("Name");
            attribute.Value = model.Name;

            currentNode.Attributes.Append(attribute);

            if (parentNode == null)
                document.AppendChild(currentNode);
            else
                parentNode.AppendChild(currentNode);

            if (model.ChildrenFolders != null && model.ChildrenFolders.Count > 0)
            {
                foreach (var item in model.ChildrenFolders)
                {
                    BuilderByFolderNode(document, item, currentNode);
                }
            }
            if (model.ChildrenBookMark != null && model.ChildrenBookMark.Count > 0)
            {
                foreach (var item in model.ChildrenBookMark)
                {
                    BuilderByBookMarkNode(document, item, currentNode);
                }
            }
        }

        /// <summary>
        /// 根据收藏夹中的网址生成XML节点
        /// </summary>
        /// <param name="document"></param>
        /// <param name="model"></param>
        /// <param name="parentNode"></param>
        private void BuilderByBookMarkNode(XmlDocument document, BookMarkModel model, XmlNode parentNode)
        {
            XmlNode currentNode = document.CreateNode(XmlNodeType.Element, "BookMark", "");

            XmlAttribute nameAttribute = document.CreateAttribute("Name");
            nameAttribute.Value = model.Name;
            currentNode.Attributes.Append(nameAttribute);

            XmlAttribute urlAttribute = document.CreateAttribute("Url");
            urlAttribute.Value = model.Url;
            currentNode.Attributes.Append(urlAttribute);

            parentNode.AppendChild(currentNode);
        }
    }
}
