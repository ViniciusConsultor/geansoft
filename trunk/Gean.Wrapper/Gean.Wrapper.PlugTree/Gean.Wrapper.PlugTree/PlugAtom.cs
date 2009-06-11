using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Gean.Wrapper.PlugTree.Exceptions;

using Gean.Wrapper.PlugTree.Components;
using System.Reflection;

namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// 一个描述Path节点下的每个组件化的节点的类型，如MenuItem, Pad, View等。
    /// 同时，该类型更是一个对象创建器。被创建的对象具有某个具体的功能。
    /// 通常在Plug的Xml文件里表现为Path节点下的子节点。
    /// Atom：n. 原子,微粒,微量。
    /// 例如：
    /// ＜Path name="/abc/123/xxx/yyy/zzz"＞
    ///    ＜Pad id   = "ProjectBrowser"
    ///	     category = "Main"
    ///	     title    = "${res:MainWindow.Windows.ProjectScoutLabel}"
    ///	     icon     = "PadIcons.ProjectBrowser"
    ///	     shortcut = "Control|Alt|L"
    ///	     class    = "ICSharpCode.SharpDevelop.Project.ProjectBrowserPad"/＞
    ///    ＜Pad id   = "ProjectBrowser"
    ///	     category = "Main"
    ///	     title    = "${res:MainWindow.Windows.ProjectScoutLabel}"
    ///	     icon     = "PadIcons.ProjectBrowser"
    ///	     shortcut = "Control|Alt|L"
    ///	     class    = "ICSharpCode.SharpDevelop.Project.ProjectBrowserPad"/＞
    ///    ＜Pad id   = "ProjectBrowser"
    ///	     category = "Main"
    ///	     title    = "${res:MainWindow.Windows.ProjectScoutLabel}"
    ///	     icon     = "PadIcons.ProjectBrowser"
    ///	     shortcut = "Control|Alt|L"
    ///	     class    = "ICSharpCode.SharpDevelop.Project.ProjectBrowserPad"/＞
    ///	＜/Path＞
    /// </summary>
    public class PlugAtom
    {
        #region Static方法: 从Xml节点Parse出PlugAtom
        /// <summary>
        /// 从一组Xml节点中解析出若干的PlugAtom
        /// </summary>
        /// <param name="xmlNodeList">需解析成为PlugAtom的节点的集合，一般为Path节点的子节点集合</param>
        /// <param name="parentPlug">解析出来的PlugAtom所属的Plug</param>
        /// <returns></returns>
        public static IEnumerable<PlugAtom> Parse(XmlNodeList xmlNodeList, PlugPath ownerPlugPath, Plug ownerPlug)
        {
            List<PlugAtom> plugAtoms = new List<PlugAtom>();
            foreach (XmlNode item in xmlNodeList)
            {
                if (item.NodeType != XmlNodeType.Element)
                {
                    break;
                }
                XmlElement element = (XmlElement)item;
                plugAtoms.Add(PlugAtom.Parse(element, ownerPlugPath, ownerPlug));
            }
            return plugAtoms.ToArray();
        }
        private static PlugAtom Parse(XmlElement element, PlugPath ownerPlugPath, Plug ownerPlug)
        {
            PlugAtom plugAtom = new PlugAtom();
            plugAtom.Name = element.LocalName;
            plugAtom.OwnerPlugPath = ownerPlugPath;
            plugAtom.OwnerPlug = ownerPlug;
            plugAtom.Properties = Properties.Parse(element);
            switch (element.LocalName.ToLowerInvariant())
            {
                case "complexcondition":
                    break;
                case "condition":
                    break;
                case "and":
                    break;
                case "or":
                    break;
                case "not":
                    break;
                default:
                    break;
            }
            if (element.LocalName.ToLowerInvariant() == "condition")
            {
                plugAtom.ConditionCollection.Add(Condition.Parse(element));
            }
            return plugAtom;
        }
        #endregion

        public string Name { get; private set; }
        public Assembly Assembly { get; private set; }
        public Plug OwnerPlug { get; private set; }
        public PlugPath OwnerPlugPath { get; private set; }
        public Properties Properties { get; private set; }
        public ConditionCollection ConditionCollection { get; private set; }

        public string Id
        {
            get { return (string)Properties["id"]; }
        }

        public string InsertAfter
        {
            get
            {
                if (!Properties.ContainsKey("insertafter"))
                {
                    return "";
                }
                return (string)Properties["insertafter"];
            }
            set { Properties["insertafter"] = value; }
        }

        public string InsertBefore
        {
            get
            {
                if (!Properties.ContainsKey("insertbefore"))
                {
                    return "";
                }
                return (string)Properties["insertbefore"];
            }
            set { Properties["insertbefore"] = value; }
        }

        public string this[string key]
        {
            get { return (string)Properties[key]; }
        }

        private PlugAtom() 
        {
            this.ConditionCollection = new ConditionCollection();
        }

        public ConditionFailedAction GetFailedAction(object caller)
        {
            return Condition.GetFailedAction(ConditionCollection, caller);
        }

        public object BuildItem(object owner, ArrayList subItems)
        {
            IBuilder builder;
            if (!PlugManager.BuilderCollection.TryGetValue(this.Name, out builder))
                throw new PlugTreeException("Builder " + Name + " not found!");

            if (!builder.HandleConditions && ConditionCollection.Count > 0)
            {
                ConditionFailedAction action = GetFailedAction(owner);
                if (action != ConditionFailedAction.None)
                {
                    return null;
                }
            }
            return builder.BuildItem(owner, this, subItems);
        }

        public override string ToString()
        {
            return String.Format("[PlugAtom = {0}, PlugPath = {1}]",
                                 this.Name,
                                 this.OwnerPlugPath.ToString());
        }

    }

}
/*
	<Path name = "/SharpDevelop/Pads/ProjectBrowser/ContextMenu/FileNode">
 
		<ComplexCondition>
			<Not>
				<Condition name = "Ownerstate" ownerstate = "Missing"/>
			</Not>
			<MenuItem id = "OpenFile"
			          label = "${res:ProjectComponent.ContextMenu.Open}"
			          icon  = "Icons.16x16.OpenFileIcon"
			          class = "ICSharpCode.SharpDevelop.Project.Commands.OpenFileFromProjectBrowser"/>
			<MenuItem id      = "OpenFileWith"
			          label = "${res:Gui.ProjectBrowser.OpenWith}"
			          class   = "ICSharpCode.SharpDevelop.Project.Commands.OpenFileFromProjectBrowserWith"/>
			<MenuItem id = "OpenFolderContainingFile"
			          label = "${res:OpenFileTabEventHandler.FileContainingFolderInExplorer}"
			          class = "ICSharpCode.SharpDevelop.Project.Commands.OpenFolderContainingFile"/>
		</ComplexCondition>
 
 
		<Condition name = "Ownerstate" ownerstate = "Missing">
			<MenuItem id = "Remove"
			          label = "${res:Global.RemoveButtonText}"
			          type = "Item"
			          icon  = "Icons.16x16.DeleteIcon"
			          class = "ICSharpCode.SharpDevelop.Project.Commands.DeleteProjectBrowserNode"/>
		</Condition>
 
 
		<ComplexCondition>
			<Not>
				<Condition name = "Ownerstate" ownerstate = "Missing"/>
			</Not>
			<MenuItem id = "OpenSeparator" type = "Separator" />
			
			<Condition name = "Ownerstate" ownerstate = "InProject">
				<MenuItem id = "Add" label = "${res:ProjectComponent.ContextMenu.AddMenu}" type="Menu">
					<Include path="/SharpDevelop/Pads/ProjectBrowser/ContextMenu/FolderNode/Add"/>
					<MenuItem id    = "AddDependentSeparator"
					          type  = "Separator" />
					<MenuItem id    = "NewDependentItem"
					          label = "${res:ProjectComponent.ContextMenu.NewDependentItem}"
					          icon  = "ProjectBrowser.CodeBehind"
					          class = "ICSharpCode.SharpDevelop.Project.Commands.AddNewDependentItemsToProject"/>
					<MenuItem id    = "ExistingItemAsDependent"
					          label = "${res:ProjectComponent.ContextMenu.ExistingItemAsDependent}"
					          icon  = "ProjectBrowser.CodeBehind"
					          class = "ICSharpCode.SharpDevelop.Project.Commands.AddExistingItemsToProjectAsDependent"/>
				</MenuItem>
				<MenuItem id = "AddSeparator" type = "Separator"  />
			</Condition>
			
			<ComplexCondition>
				<Or>
					<Condition name = "Ownerstate" ownerstate = "InProject"/>
					<Condition name = "Ownerstate" ownerstate = "None"/>
				</Or>
				<Condition name = "Ownerstate" ownerstate = "InProject">
					<MenuItem id    = "ExcludeFile"
					          label = "${res:ProjectComponent.ContextMenu.ExcludeFileFromProject}"
					          class = "ICSharpCode.SharpDevelop.Project.Commands.ExcludeFileFromProject"/>
					<MenuItem id     = "ExecuteCustomTool"
					          label  = "${res:ProjectComponent.ContextMenu.ExecuteCustomTool}"
					          class  = "ICSharpCode.SharpDevelop.Project.ExecuteCustomToolCommand"/>
				</Condition>
				
				<Condition name = "Ownerstate" ownerstate = "None">
					<MenuItem id    = "IncludeFile"
					          label = "${res:ProjectComponent.ContextMenu.IncludeFileInProject}"
					          class = "ICSharpCode.SharpDevelop.Project.Commands.IncludeFileInProject"/>
				</Condition>
				
				<MenuItem id = "ExcludeSeparator" type = "Separator" />
			</ComplexCondition>
			<Include id="CutCopyPasteDeleteRename" path="/SharpDevelop/Pads/ProjectBrowser/ContextMenu/CutCopyPasteDeleteRename"/>
			<MenuItem id = "RenameSeparator" type = "Separator" />
			<MenuItem id    = "Properties"
			          icon  = "Icons.16x16.PropertiesIcon"
			          label = "${res:XML.MainMenu.FormatMenu.ShowProperties}"
			          class = "ICSharpCode.SharpDevelop.Project.Commands.ShowPropertiesForNode"/>
		</ComplexCondition>
	</Path>
*/