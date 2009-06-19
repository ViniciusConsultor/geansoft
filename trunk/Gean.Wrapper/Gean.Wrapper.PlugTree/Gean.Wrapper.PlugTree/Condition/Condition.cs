using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Gean.Wrapper.PlugTree
{
    public class Condition : ICondition
    {
        string _Name;
        Definers _Definers;
        ConditionFalseAction _Action;

        /// <summary>
        /// Returns the action which occurs, when this condition fails.
        /// </summary>
        public ConditionFalseAction Action
        {
            get { return _Action; }
            set { _Action = value; }
        }

        public string Name
        {
            get { return _Name; }
        }

        public string this[string key]
        {
            get { return (string)_Definers[key]; }
        }

        public Definers Definers
        {
            get { return _Definers; }
        }

        public Condition(string name, Definers definers)
        {
            this._Name = name;
            this._Definers = definers;
            _Action = definers.Get("action", ConditionFalseAction.Exclude);
        }

        public bool IsValid(object caller)
        {
            try
            {
                return true;// AddInTree.ConditionEvaluators[_Name].IsValid(caller, this);
            }
            catch (KeyNotFoundException)
            {
                throw new PlugTreeException("Condition evaluator " + _Name + " not found!");
            }

        }

        public static ICondition Read(XmlReader reader)
        {
            Definers definers = Definers.Parse(reader);
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
    }
}
