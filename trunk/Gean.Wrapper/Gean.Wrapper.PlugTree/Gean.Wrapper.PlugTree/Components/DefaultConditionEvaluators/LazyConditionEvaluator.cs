// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision: 1965 $</version>
// </file>

using System;
using Gean.Wrapper.PlugTree.Components;


namespace Gean.Wrapper.PlugTree
{
    /// <summary>
    /// Condition evaluator that lazy-loads another condition evaluator and executes it.
    /// </summary>
    public class LazyConditionEvaluator : IConditionEvaluator
    {
        Plug _Plug;
        string name;
        string className;

        public string Name
        {
            get
            {
                return name;
            }
        }

        public string ClassName
        {
            get
            {
                return className;
            }
        }

        public LazyConditionEvaluator(Plug plug, Properties properties)
        {
            this._Plug = plug;
            this.name = (string)properties["name"];
            this.className = (string)properties["class"];
        }

        public bool IsValid(object caller, Condition condition)
        {
            IConditionEvaluator evaluator = (IConditionEvaluator)_Plug.CreateObject(className);
            if (evaluator == null)
            {
                return false;
            }
            //////AddInTree.ConditionEvaluators[name] = evaluator;
            return evaluator.IsValid(caller, condition);
        }

        public override string ToString()
        {
            return String.Format("[LazyLoadConditionEvaluator: className = {0}, name = {1}]",
                                 className,
                                 name);
        }

    }
}
