using System.Collections.Generic;
using System.Xml;
using Gean.Wrapper.PlugTree.Components.ComplexCondition;
using Gean.Wrapper.PlugTree.Exceptions;


namespace Gean.Wrapper.PlugTree.Components
{

    public class Condition : ICondition
    {

        public string Name { get; private set; }
        public ConditionFailedAction Action { get; set; }
        public Properties Properties { get; private set; }

        public Condition(string name, Properties properties)
        {
            this.Name = name;
            this.Properties = properties;
            this.Action = Properties.Get("action", ConditionFailedAction.Exclude);
        }

        public string this[string key]
        {
            get { return (string)Properties[key]; }
        }

        public static ICondition Parse(XmlElement element)
        {
            Properties properties = Properties.Parse(element);
            string conditionName = (string)properties["name"];
            Condition condition = new Condition(conditionName, properties);
            return condition;
        }

        public static ICondition ParseComplex(XmlElement element)
        {
            Properties properties = Properties.Parse(element);
            ICondition condition = null;
            foreach (XmlNode node in element.ChildNodes)
            {
                if (node.NodeType != XmlNodeType.Element)
                {
                    continue;
                }
                XmlElement ele = (XmlElement)node;
                switch (ele.LocalName.ToLowerInvariant())
                {
                    case "and":
                        condition = AbstractCondition.ParseNode(ele, ele.LocalName);
                        goto exitforeach;
                    case "or":
                        condition = AbstractCondition.ParseNode(ele, ele.LocalName);
                        goto exitforeach;
                    case "not":
                        condition = AbstractCondition.ParseNode(ele, ele.LocalName);
                        goto exitforeach;
                    case "condition":
                        condition = Condition.Parse(ele);
                        goto exitforeach;
                    default:
                        throw new PlugParseException("Invalid XmlElement name '" + ele.LocalName
                                                     + "', the first entry in a ComplexCondition " +
                                                     "must be <And>, <Or> or <Not>");
                }
            }
        exitforeach:
            if (condition != null)
            {
                ConditionFailedAction action = properties.Get("action", ConditionFailedAction.Exclude);
                condition.Action = action;
            }
            return condition;
        }

        public static ConditionFailedAction GetFailedAction(IEnumerable<ICondition> conditionList, object caller)
        {
            ConditionFailedAction action = ConditionFailedAction.None;
            foreach (ICondition condition in conditionList)
            {
                if (!condition.IsValid(caller))
                {
                    if (condition.Action == ConditionFailedAction.Disable)
                    {
                        action = ConditionFailedAction.Disable;
                    }
                    else
                    {
                        return ConditionFailedAction.Exclude;
                    }
                }
            }
            return action;
        }

        #region ICondition 成员

        public bool IsValid(object caller)
        {
            try
            {
                return PlugManager.ConditionEvaluatorCollection[this.Name].IsValid(caller, this);
            }
            catch (KeyNotFoundException)
            {
                throw new PlugTreeException("Condition evaluator " + this.Name + " not found!");
            }
        }

        #endregion

    }
}
