using System;
using System.Collections.Generic;
using System.Text;
using Gean.Wrapper.PlugTree.Components;


namespace Gean.Wrapper.PlugTree
{

    public class ConditionCollection : EventList<ICondition> { }
    public class PlugAtomCollection : EventList<PlugAtom> { }
    public class PlugCollection : EventList<Plug> { }
    public class PlugPathCollection : EventList<PlugPath> { }
    public class PlugRuntimeCollention : EventList<PlugRuntime> { }

    public class BuilderCollection : EventIndexedDictionary<string, IBuilder> { }
    public class VersionPairCollection : EventIndexedDictionary<string, VersionPair> { }
    public class ConditionEvaluatorCollection : EventIndexedDictionary<string, IConditionEvaluator> { }

}
