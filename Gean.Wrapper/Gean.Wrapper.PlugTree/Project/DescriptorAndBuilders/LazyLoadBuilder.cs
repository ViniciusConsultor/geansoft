using System;
using System.Collections.Generic;
using System.Text;
using Gean.Wrapper.PlugTree.Components;
using System.Collections;
using Gean.Framework;

namespace Gean.Wrapper.PlugTree
{
    public class LazyLoadBuilder : IBuilder
    {
        protected Plug OwnerPlug { get; private set; }

        public string Name { get; private set; }

        public string ClassName { get; private set; }

        public LazyLoadBuilder(Plug plug, Properties properties)
		{
            this.OwnerPlug = plug;
            this.Name = (string)properties["name"];
            this.ClassName = (string)properties["class"];
        }

        #region IBuilder 成员

        /// <summary>
		/// Gets if the doozer handles codon conditions on its own.
		/// If this property return false, the item is excluded when the condition is not met.
		/// </summary>
        public bool HandleConditions
        {
            get
            {
                IBuilder builder = (IBuilder)this.OwnerPlug.CreateObject(this.ClassName);
                if (builder == null)
                {
                    return false;
                }
                if (PlugManager.BuilderCollection.ContainsKey(this.Name))
                {
                    PlugManager.BuilderCollection[this.Name] = builder;
                }
                else
                {
                    PlugManager.BuilderCollection.Add(this.Name, builder);
                }
                return builder.HandleConditions;
            }
        }
		
		public object BuildItem(object caller, PlugAtom plugAtom, ArrayList subItems)
		{
            IBuilder builder = (IBuilder)this.OwnerPlug.CreateObject(this.ClassName);
            if (builder == null)
            {
				return null;
			}
            if (PlugManager.BuilderCollection.ContainsKey(this.Name))
            {
                PlugManager.BuilderCollection[this.Name] = builder;
            }
            else
            {
                PlugManager.BuilderCollection.Add(this.Name, builder);
            }
            return builder.BuildItem(caller, plugAtom, subItems);
		}

        #endregion
		
		public override string ToString()
		{
            return String.Format("[LazyLoadBuilder: className = {0}, name = {1}]",
                                 this.ClassName,
			                     this.Name);
		}

    }
}
