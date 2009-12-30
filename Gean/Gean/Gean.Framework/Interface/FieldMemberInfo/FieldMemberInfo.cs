using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Gean
{
	/// <summary>
	/// ʵ��<see cref="IFieldMemberInfo"/>�ӿ�
	/// </summary>
	public class FieldMemberInfo : IFieldMemberInfo
	{
		#region constructor
		
		/// <summary>
		/// ���췽��
		/// </summary>
		/// <param name="columnAttribute">����</param>
		/// <param name="property">������Ϣ</param>
		public FieldMemberInfo(ColumnAttribute columnAttribute, PropertyInfo property) {
			this.columnAttribute = columnAttribute;
			this.property = property;
			this.memberInfo = property;
			this.name = columnAttribute.ColumnName == null ? property.Name : columnAttribute.ColumnName;
			this.canRead = !columnAttribute.WriteOnly && property.CanRead;
			this.canWrite = !columnAttribute.ReadOnly && property.CanWrite;
		}

		/// <summary>
		/// ���췽��
		/// </summary>
		/// <param name="columnAttribute">����</param>
		/// <param name="field">�ֶ���Ϣ</param>
		public FieldMemberInfo(ColumnAttribute columnAttribute, FieldInfo field) {
			this.columnAttribute = columnAttribute;
			this.field = field;
			this.memberInfo = field;
			this.name = columnAttribute.ColumnName == null ? field.Name : columnAttribute.ColumnName;
			this.canRead = !columnAttribute.WriteOnly;
			this.canWrite = !columnAttribute.ReadOnly;
		}

		#endregion

		#region private members

		private ColumnAttribute columnAttribute;
		private PropertyInfo property;
		private FieldInfo field;
		private MemberInfo memberInfo;
		private string name;
		private bool canRead;
		private bool canWrite;

		#endregion

		#region IFieldMemberInfo Members

		/// <summary>
		/// �ֶ��Ƿ�ɶ�
		/// </summary>
		public bool CanRead {
			get { return this.canRead; }
		}

		/// <summary>
		/// �ֶ��Ƿ��д
		/// </summary>
		public bool CanWrite {
			get { return this.canWrite; }
		}

		/// <summary>
		/// �ֶ���
		/// </summary>
		public string Name {
			get { return this.name; }
		}

		/// <summary>
		/// �ֶμ��صĳ�Ա��Ϣ<see cref="IFieldMemberInfo.MemberInfo"/>
		/// </summary>
		public MemberInfo MemberInfo {
			get { return this.memberInfo; }
		}

		/// <summary>
		/// �ֶα���ļ�����Ϣ<see cref="ColumnAttribute"/>
		/// </summary>
		public ColumnAttribute Column {
			get { return this.columnAttribute; }
		}

		/// <summary>
		/// ��ȡ�ֶε�ֵ
		/// </summary>
		/// <param name="obj">�������ֶε�ʵ��</param>
		/// <returns>�ֶε�ֵ</returns>
		public object GetValue(object obj) {
			if(this.property != null) {
				return this.property.GetValue(obj, null);
			} else {
				return this.field.GetValue(obj);
			}
		}

		/// <summary>
		/// �����ֶε�ֵ
		/// </summary>
		/// <param name="obj">�������ֶε�ʵ��</param>
		/// <param name="value">�ֶε�ֵ</param>
		public void SetValue(object obj, object value) {
			Type type;
			if(this.property != null) {
				type = this.property.PropertyType;
			} else {
				type = this.field.FieldType;
			}
			if(Convert.IsDBNull(value)) {
				if(this.columnAttribute.DefaultValue != null) {
					value = this.columnAttribute.DefaultValue;
				} else if(!type.IsValueType) {
					value = null;
				} else {
					return;
				}
			}
			if(this.property != null) {
				this.property.SetValue(obj, value, null);
			} else {
				this.field.SetValue(obj, value);
			}
		}

		#endregion

		#region static members

		private static Dictionary<Type, IFieldMemberInfo[]> cache = new Dictionary<Type,IFieldMemberInfo[]>();
		private static object lockObject = new object();

        /// <summary>
        /// ʹ�÷�����÷���
        /// </summary>
        /// <param name="obj">����ʵ��</param>
        /// <param name="methodName">������</param>
        /// <param name="parameters">�����б�</param>
        /// <returns>��������ֵ</returns>
        public static object Invoke(object obj, string methodName, params object[] parameters)
        {
            if (obj == null)
            {
                return obj;
            }
            return obj.GetType().GetMethod(methodName, FieldMemberInfo.FieldBindingFlags).Invoke(obj, parameters);
        }

		/// <summary>
		/// �ֶΰ�Ԥ��ֵ
		/// </summary>
		public const BindingFlags FieldBindingFlags = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;

		/// <summary>
		/// ��ȡ�󶨵��ֶ���Ϣ�б�
		/// </summary>
		/// <param name="type">����ȡ������</param>
		/// <returns>IFieldMemberInfo[]</returns>
		public static IFieldMemberInfo[] GetFieldMembers(Type type) {
			return GetFieldMembers(type, FieldBindingFlags);
		}

		/// <summary>
		/// ��ȡ�󶨵��ֶ���Ϣ�б�
		/// </summary>
		/// <param name="type">����ȡ������</param>
		/// <param name="columnBindingFlags">�󶨱�ʶ</param>
		/// <returns>IFieldMemberInfo[]</returns>
		public static IFieldMemberInfo[] GetFieldMembers(Type type, BindingFlags columnBindingFlags) {
			return GetFieldMembers(type, columnBindingFlags, false);
		}

		/// <summary>
		/// ��ȡ�󶨵��ֶ���Ϣ�б�
		/// </summary>
		/// <param name="type">����ȡ������</param>
		/// <param name="columnBindingFlags">�󶨱�ʶ</param>
		/// <param name="inherit">�Ƿ�����̳���</param>
		/// <returns>IFieldMemberInfo[]</returns>
		public static IFieldMemberInfo[] GetFieldMembers(Type type, BindingFlags columnBindingFlags, bool inherit) {
			if(cache.ContainsKey(type)) {
				return cache[type];
			}
			lock (lockObject) {
				if (cache.ContainsKey(type)) {
					return cache[type];
				}
				ArrayList fieldMembers = new ArrayList();
				PropertyInfo[] props = type.GetProperties(columnBindingFlags);
				for (int i = 0; i < props.Length; i++) {
					ColumnAttribute[] columnAttributes = (ColumnAttribute[])props[i].GetCustomAttributes(typeof(ColumnAttribute), inherit);
					if (columnAttributes != null && columnAttributes.Length > 0) {
						fieldMembers.Add(new FieldMemberInfo(columnAttributes[0], props[i]));
					}
				}
				FieldInfo[] fields = type.GetFields(columnBindingFlags);
				for (int i = 0; i < fields.Length; i++) {
					ColumnAttribute[] columnAttributes = (ColumnAttribute[])fields[i].GetCustomAttributes(typeof(ColumnAttribute), inherit);
					if (columnAttributes != null && columnAttributes.Length > 0) {
						fieldMembers.Add(new FieldMemberInfo(columnAttributes[0], fields[i]));
					}
				}
				IFieldMemberInfo[] members = (IFieldMemberInfo[])fieldMembers.ToArray(typeof(IFieldMemberInfo));
				if (!cache.ContainsKey(type)) {
					cache.Add(type, members);
				}
				return members;
			}
		}

		/// <summary>
		/// ��ȡ�󶨵��ֶ���Ϣ
		/// </summary>
		/// <param name="type">����ȡ������</param>
		/// <param name="fieldName">�ֶ���</param>
		/// <param name="columnBindingFlags">�󶨱�ʶ</param>
		/// <param name="inherit">�Ƿ�����̳���</param>
		/// <returns>IFieldMemberInfo</returns>
		public static IFieldMemberInfo GetFieldMember(Type type, string fieldName, BindingFlags columnBindingFlags, bool inherit) {
			PropertyInfo prop = type.GetProperty(fieldName, columnBindingFlags);
			if(prop != null) {
				ColumnAttribute[] columnAttributes = (ColumnAttribute[])prop.GetCustomAttributes(typeof(ColumnAttribute), inherit);
				if(columnAttributes != null && columnAttributes.Length > 0) {
					return new FieldMemberInfo(columnAttributes[0], prop);
				}
			}

			FieldInfo field = type.GetField(fieldName, columnBindingFlags);
			if(field != null) {
				ColumnAttribute[] columnAttributes = (ColumnAttribute[])field.GetCustomAttributes(typeof(ColumnAttribute), inherit);
				if(columnAttributes != null && columnAttributes.Length > 0) {
					return new FieldMemberInfo(columnAttributes[0], field);
				}
			}

			return null;
		}

		#endregion
	}
}
