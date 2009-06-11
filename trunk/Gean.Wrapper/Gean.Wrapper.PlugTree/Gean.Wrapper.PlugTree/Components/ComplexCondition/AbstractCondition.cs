using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Diagnostics;
using Gean.Wrapper.PlugTree.Exceptions;

namespace Gean.Wrapper.PlugTree.Components.ComplexCondition
{
    public abstract class AbstractCondition : ICondition
    {
        public AbstractCondition(ICondition condition, string conditionSwitch)
        {
            Debug.Assert(condition != null);
            this._Condition = condition;
            this._ConditionSwitch = conditionSwitch;
        }

        protected string _ConditionSwitch;
        protected ICondition _Condition;

        public virtual string Name
        {
            get { return _ConditionSwitch + " " + _Condition.Name; }
        }

        public virtual ConditionFailedAction Action
        {
            get { return _Action; }
            set { _Action = value; }
        }
        protected ConditionFailedAction _Action = ConditionFailedAction.Exclude;

        public virtual bool IsValid(object owner)
        {
            return !_Condition.IsValid(owner);
        }

        internal static ICondition ParseNode(XmlElement element, string conditionSwitch)
        {
            foreach (XmlNode node in element.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                XmlElement ele = (XmlElement)node;
                switch (conditionSwitch.ToLowerInvariant())
                {
                    case "and":
                        return new AndCondition(Condition.Parse(ele));
                    case "or":
                        return new OrCondition(Condition.Parse(ele));
                    case "not":
                        return new NegatedCondition(Condition.Parse(ele));
                    case "condition":
                        return Condition.Parse(ele);
                    default:
                        throw new PlugParseException("Invalid element name '" + element.LocalName
                                                     + "', entries in a <" + element.LocalName + "> " +
                                                     "must be <And>, <Or>, <Not> or <Condition>");
                }
            }
            return null;
        }
    }
}
/*
		<ComplexCondition>
			<Not>
				<Condition name = "Ownerstate" ownerstate = "Missing"/>
			</Not>
			<MenuItem id = "Add" label = "${res:ProjectComponent.ContextMenu.AddMenu}" type="Menu">
				<MenuItem id    = "New Item"
				          label = "${res:ProjectComponent.ContextMenu.NewItem}"
				          icon  = "Icons.16x16.NewDocumentIcon"
				          class = "ICSharpCode.SharpDevelop.Project.Commands.AddNewItemsToProject"/>
			</MenuItem>
			<MenuItem id = "AddSeparator" type = "Separator"  />
			<ComplexCondition>
				<Or>
					<Condition name = "Ownerstate" ownerstate = "InProject"/>
					<Condition name = "Ownerstate" ownerstate = "None"/>
				</Or>
				<Condition name = "Ownerstate" ownerstate = "InProject">
					<MenuItem id    = "ExcludeFile"
					          label = "${res:ProjectComponent.ContextMenu.ExcludeFileFromProject}"
					          class = "ICSharpCode.SharpDevelop.Project.Commands.ExcludeFileFromProject"/>
				</Condition>
				<Condition name = "Ownerstate" ownerstate = "None">
					<MenuItem id    = "IncludeFile"
					          label = "${res:ProjectComponent.ContextMenu.IncludeFileInProject}"
					          class = "ICSharpCode.SharpDevelop.Project.Commands.IncludeFileInProject"/>
				</Condition>
				<MenuItem id = "ExcludeSeparator" type = "Separator" />
			</ComplexCondition>
			<Include id="CutCopyPasteDeleteRename" path="/SharpDevelop/Pads/ProjectBrowser/ContextMenu/CutCopyPasteDeleteRename"/>
		</ComplexCondition>
*/