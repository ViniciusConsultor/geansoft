using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;

namespace Gean.Wrapper.PlugTree
{
    public class Condition : ICondition
    {
        public Condition(string name, Definer definers)
        {
            this.Name = name;
            this.Definers = definers;
            this.Action = definers.Get("action", ConditionFalseAction.Exclude);
        }

        public ConditionFalseAction Action { get; set; }
        public Definer Definers { get; private set; }
        public string Name { get; private set; }
        public string this[string key]
        {
            get { return (string)this.Definers[key]; }
        }

        internal static ICondition Load(Assembly assembly, string classname)
        {
            Type type = assembly.GetType(classname, true, false);
            if (typeof(ICondition).IsAssignableFrom(type))
            {
                return (ICondition)type;
            }
            else
            {
                return null;
            }
        }

        public static ICondition Read(XmlReader reader)
        {
            Definer definers = Definer.ReadFromAttributes(reader);
            string conditionName = (string)definers["name"];
            return new Condition(conditionName, definers);
        }

        public static ICondition ReadComplexCondition(XmlReader reader)
        {
            //Properties properties = Definers.ReadFromAttributes(reader);
            //reader.Read();
            ICondition condition = null;
        //    while (reader.Read())
        //    {
        //        switch (reader.NodeType)
        //        {
        //            case XmlNodeType.Element:
        //                switch (reader.LocalName)
        //                {
        //                    case "And":
        //                        condition = AndCondition.Read(reader);
        //                        goto exit;
        //                    case "Or":
        //                        condition = OrCondition.Read(reader);
        //                        goto exit;
        //                    case "Not":
        //                        condition = NegatedCondition.Read(reader);
        //                        goto exit;
        //                    default:
        //                        throw new AddInLoadException("Invalid element name '" + reader.LocalName
        //                                                     + "', the first entry in a ComplexCondition " +
        //                                                     "must be <And>, <Or> or <Not>");
        //                }
        //        }
        //    }
        //exit:
        //    if (condition != null)
        //    {
        //        ConditionFailedAction action = properties.Get("action", ConditionFailedAction.Exclude);
        //        condition.Action = action;
        //    }
            return condition;
        }

        public static ICondition[] ReadConditionList(XmlReader reader, string endElement)
        {
            List<ICondition> conditions = new List<ICondition>();
            //while (reader.Read())
            //{
            //    switch (reader.NodeType)
            //    {
            //        case XmlNodeType.EndElement:
            //            if (reader.LocalName == endElement)
            //            {
            //                return conditions.ToArray();
            //            }
            //            break;
            //        case XmlNodeType.Element:
            //            switch (reader.LocalName)
            //            {
            //                case "And":
            //                    conditions.Add(AndCondition.Read(reader));
            //                    break;
            //                case "Or":
            //                    conditions.Add(OrCondition.Read(reader));
            //                    break;
            //                case "Not":
            //                    conditions.Add(NegatedCondition.Read(reader));
            //                    break;
            //                case "Condition":
            //                    conditions.Add(Condition.Read(reader));
            //                    break;
            //                default:
            //                    throw new AddInLoadException("Invalid element name '" + reader.LocalName
            //                                                 + "', entries in a <" + endElement + "> " +
            //                                                 "must be <And>, <Or>, <Not> or <Condition>");
            //            }
            //            break;
            //    }
            //}
            return conditions.ToArray();
        }

        public static ConditionFalseAction GetFailedAction(IEnumerable<ICondition> conditionList, object caller)
        {
            ConditionFalseAction action = ConditionFalseAction.Nothing;
            foreach (ICondition condition in conditionList)
            {
                if (!condition.IsValid(caller))
                {
                    if (condition.Action == ConditionFalseAction.Disable)
                    {
                        action = ConditionFalseAction.Disable;
                    }
                    else
                    {
                        return ConditionFalseAction.Exclude;
                    }
                }
            }
            return action;
        }

        public bool IsValid(object caller)
        {
            try
            {
                return true;// AddInTree.ConditionEvaluators[_Name].IsValid(caller, this);
            }
            catch (KeyNotFoundException)
            {
                throw new PlugTreeException("Condition evaluator " + this.Name + " not found!");
            }

        }
    }
}
