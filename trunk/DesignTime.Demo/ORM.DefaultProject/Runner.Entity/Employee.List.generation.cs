using System;
using System.Collections.Generic;
using System.Text;

namespace Runner.Entity
{
    public partial class EmployeeList : ICollection<Employee>
    {
        private List<Employee> _employee = new List<Employee>();

        #region ICollection<Employee> 成员

        public void Add(Employee item)
        {
            _employee.Add(item);
        }

        public void Clear()
        {
            _employee.Clear();
        }

        public bool Contains(Employee item)
        {
            return _employee.Contains(item);
        }

        public void CopyTo(Employee[] array, int arrayIndex)
        {
            _employee.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _employee.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Employee item)
        {
            return _employee.Remove(item);
        }

        #endregion

        #region IEnumerable<Employee> 成员

        public IEnumerator<Employee> GetEnumerator()
        {
            return _employee.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _employee.GetEnumerator();
        }

        #endregion
    }
}
