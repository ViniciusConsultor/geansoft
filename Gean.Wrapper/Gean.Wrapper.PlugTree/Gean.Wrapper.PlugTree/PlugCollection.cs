using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Wrapper.PlugTree
{
    public sealed class PlugCollection : OutList<Plug>
    {
        //private List<Plug> _Plugs = new List<Plug>();

        //internal PlugCollection()
        //{

        //}

        //public Plug this[int index]
        //{
        //    get { return this._Plugs[index]; }
        //    internal set { this._Plugs[index] = value; }
        //}

        //public int IndexOf(Plug item)
        //{
        //    return this._Plugs.IndexOf(item);
        //}

        //public bool Contains(Plug item)
        //{
        //    return this._Plugs.Contains(item);
        //}

        //public int Count
        //{
        //    get { return this._Plugs.Count; }
        //}

        //internal void Insert(int index, Plug item)
        //{
        //    this._Plugs.Insert(index, item);
        //}

        //internal void Add(Plug item)
        //{
        //    this._Plugs.Add(item);
        //}

        //internal void AddRange(IEnumerable<Plug> plugs)
        //{
        //    this._Plugs.AddRange(plugs);
        //}

        //internal void Clear()
        //{
        //    this._Plugs.Clear();
        //}

        //internal bool Remove(Plug item)
        //{
        //    return this._Plugs.Remove(item);
        //}

        //internal void RemoveAt(int index)
        //{
        //    this._Plugs.RemoveAt(index);
        //}

        //internal void RemoveAll()
        //{
        //    while (this._Plugs.Count <= 0)
        //    {
        //        this._Plugs.RemoveAt(0);
        //    }
        //}

        //#region IEnumerable<Plug> 成员

        //public IEnumerator<Plug> GetEnumerator()
        //{
        //    return this._Plugs.GetEnumerator();
        //}

        //#endregion

        //#region IEnumerable 成员

        //IEnumerator IEnumerable.GetEnumerator()
        //{
        //    return this._Plugs.GetEnumerator();
        //}

        //#endregion
    }
}
