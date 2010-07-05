using System;
using System.Text;
using Gean;

namespace Pansoft.CQMS.Options
{
    /// <summary>
    /// 选项节点实现
    /// </summary>
    public abstract class Option : IOption
    {
        #region static fields

        /// <summary>
        /// 选项节另附的文件属性名
        /// </summary>
        public const string OptionFilePropertyName = "optionFile";
        /// <summary>
        /// 选项节在另附文件的节点名
        /// </summary>
        public const string OptionNodePropertyName = "optionNode";
        /// <summary>
        /// 选项节实际名称的属性名
        /// </summary>
        public const string OptionNamePropertyName = "name";

        #endregion static fields

        #region constructor

        /// <summary>
        /// 保护构造方法
        /// </summary>
        protected Option()
        {
        }

        #endregion constructor

        #region fields

        /// <summary>
        /// 是否只读
        /// </summary>
        private bool @readonly;
        /// <summary>
        /// 此选项节实际名称
        /// </summary>
        private string settingName;
        /// <summary>
        /// 选项值
        /// </summary>
        protected OptionValue value;
        /// <summary>
        /// 父选项节
        /// </summary>
        protected Option parent;
        /// <summary>
        /// 选项属性
        /// </summary>
        protected OptionSection property;
        /// <summary>
        /// 子选项节
        /// </summary>
        protected OptionCollection childSettings;
        /// <summary>
        /// 选项节命令集合
        /// </summary>
        protected OptionCollection operatorSettings;

        #endregion fields

        #region abstract methods

        /// <summary>
        /// 创建选项实例
        /// </summary>
        /// <returns></returns>
        protected abstract Option CreateOption();

        /// <summary>
        /// 创建选项值
        /// </summary>
        /// <param name="name">选项值名</param>
        /// <param name="value">选项值</param>
        /// <param name="readonly">是否只读</param>
        /// <returns>SettingValue</returns>
        protected abstract OptionValue CreateOptionValue(string name, string value, bool @readonly);

        /// <summary>
        /// 创建选项节点实例
        /// </summary>
        /// <param name="readonly">是否只读</param>
        /// <returns>SettingProperty</returns>
        protected abstract OptionSection CreateOptionSection(bool @readonly);

        /// <summary>
        /// 转换成字符串
        /// </summary>
        /// <param name="sb"><see cref="StringBuilder"/></param>
        /// <param name="layerIndex">所处层次</param>
        protected abstract void ToString(StringBuilder sb, int layerIndex);

        #endregion abstract methods

        #region members

        /// <summary>
        /// 创建选项节实例
        /// </summary>
        /// <param name="setting">被复制的选项节</param>
        /// <param name="deep">是否深度复制</param>
        /// <returns>选项节</returns>
        protected virtual Option CreateOptionSetting(Option setting, bool deep)
        {
            Option newSetting = this.CreateOption();
            newSetting.@readonly = setting.ReadOnly;
            newSetting.settingName = setting.settingName;
            if (deep)
            {
                newSetting.value = setting.Value.Clone();
                newSetting.property = setting.Property.Clone(this.@readonly, deep);
                newSetting.childSettings = setting.childSettings.Clone();
                newSetting.operatorSettings = setting.operatorSettings.Clone();
            }
            else
            {
                newSetting.value = setting.Value;
                newSetting.property = setting.Property;
                newSetting.childSettings = setting.childSettings;
                newSetting.operatorSettings = setting.operatorSettings;
            }
            return newSetting;
        }

        /// <summary>
        /// 当前选项节是否只读
        /// </summary>
        public virtual bool ReadOnly
        {
            get { return this.@readonly; }
            protected set { this.@readonly = value; }
        }

        /// <summary>
        /// 此选项节的名
        /// </summary>
        public virtual string Name
        {
            get { return this.Value.Name; }
        }

        /// <summary>
        /// 此选项节实际名称
        /// </summary>
        public virtual string SettingName
        {
            get
            {
                if (!string.IsNullOrEmpty(this.settingName))
                {
                    return this.settingName;
                }
                else
                {
                    return this.Name;
                }
            }
            protected set { this.settingName = value; }
        }

        /// <summary>
        /// 此选项节的名/值
        /// </summary>
        public virtual OptionValue Value
        {
            get { return this.value; }
            set
            {
                if (this.ReadOnly)
                {
                    throw new OptionException("选项节只读");
                }
                this.value = value;
            }
        }

        /// <summary>
        /// 包含此选项节的父选项节
        /// </summary>
        public virtual Option Parent
        {
            get { return this.parent; }
            set { this.parent = value; }
        }

        /// <summary>
        /// 此选项节包含的子选项节数目
        /// </summary>
        public virtual int Children
        {
            get { return this.childSettings.Count; }
        }

        /// <summary>
        /// 选项节属性
        /// </summary>
        public virtual OptionSection Property
        {
            get { return this.property; }
            set
            {
                if (this.ReadOnly)
                {
                    throw new OptionException("选项节只读");
                }
                this.property = value;
            }
        }

        /// <summary>
        /// 获取此选项文件
        /// </summary>
        public virtual string OptionFile
        {
            get { return this.Property.TryGetPropertyValue(OptionFilePropertyName); }
        }

        /// <summary>
        /// 获取此选项节另附文件中的节点
        /// </summary>
        public virtual string OptionNode
        {
            get { return this.Property.TryGetPropertyValue(OptionNodePropertyName); }
        }

        /// <summary>
        /// 此节是否为选项节命令
        /// </summary>
        protected virtual OptionOperatorEnum SettingOperator
        {
            get { return Converting.StringToEnum<OptionOperatorEnum>(this.SettingName); }
        }

        /// <summary>
        /// 获取/设置子选项节
        /// </summary>
        /// <param name="childSettingName">子选项节名</param>
        /// <remarks>
        /// 如果不存在，将返回<c>null</c><br />
        /// 如果设置时存在相同的节，则替换
        /// </remarks>
        public virtual Option this[string childSettingName]
        {
            get { return this.childSettings[childSettingName]; }
        }

        /// <summary>
        /// 获取子选项节
        /// </summary>
        /// <param name="childSettingIndex">子选项节顺序</param>
        /// <remarks>
        /// 如果不存在，将返回null
        /// </remarks>
        public virtual Option this[int childSettingIndex]
        {
            get { return this.childSettings[childSettingIndex]; }
        }

        /// <summary>
        /// 获取所有子选项节
        /// </summary>
        /// <returns>选项节数组</returns>
        public virtual Option[] GetChildSettings()
        {
            return this.childSettings.CopyToArray();
        }

        /// <summary>
        /// 按XPath方式获取选项节
        /// </summary>
        /// <param name="xpath">XPath</param>
        /// <returns>选项节</returns>
        /// <remarks>
        /// XPath为类似XML的XPath，形如<c>framework/modules/module"</c><br />
        /// 如果有相同的选项节，则返回第一个选项节
        /// </remarks>
        public virtual Option GetChildSetting(string xpath)
        {
            string[] settingName = null;
            if (xpath != null)
            {
                if (xpath.StartsWith("/"))
                {
                    xpath = xpath.Substring(1);
                }
                settingName = xpath.Split('/');
            }
            return this.GetChildSetting(settingName);
        }

        /// <summary>
        /// 按多级方式获取选项节
        /// </summary>
        /// <param name="settingName">多级的选项节名</param>
        /// <returns>选项节</returns>
        /// <remarks>
        /// 多级的选项节名，形如有如下选项：
        ///		<code>
        ///			&lt;app1&gt;
        ///				&lt;app2&gt;
        ///					&lt;app3&gt;&lt;/app3&gt;
        ///				&lt;/app2&gt;
        ///			&lt;/app1&gt;
        ///		</code>
        ///	则按顺序传入，比如<c>GetChildSetting("app1", "app2", "app3")</c>，此时返回名为<c>app3</c>的选项节<br />
        /// "."表示当前选项节，".."表示上级选项节
        /// </remarks>
        public virtual Option GetChildSetting(params string[] settingName)
        {
            Option setting = this;
            if (settingName != null && settingName.Length > 0)
            {
                for (int i = 0; setting != null && i < settingName.Length; i++)
                {
                    string name = settingName[i];
                    switch (name)
                    {
                        case ".":
                        case null:
                            break;
                        case "..":
                            setting = setting.parent;
                            break;
                        default:
                            setting = setting[name];
                            break;
                    }
                }
            }
            return setting;
        }

        /// <summary>
        /// 克隆此选项节
        /// </summary>
        /// <param name="readonly">是否只读</param>
        /// <param name="deep">是否深层次的克隆</param>
        /// <returns>选项节</returns>
        public virtual Option Clone(bool @readonly, bool deep)
        {
            Option setting = this.CreateOptionSetting(this, deep);
            setting.@readonly = @readonly;
            return setting;
        }

        /// <summary>
        /// 克隆此选项节
        /// </summary>
        /// <returns>选项节</returns>
        public virtual Option Clone()
        {
            return this.Clone(this.ReadOnly, true);
        }

        /// <summary>
        /// 合并选项节
        /// </summary>
        /// <param name="setting">需被合并的选项节</param>
        /// <returns>合并后的选项节</returns>
        public virtual Option Merge(Option setting)
        {
            if (setting == null || string.Compare(this.Name, setting.Name, true) != 0)
            {
                return this;
            }
            this.Property.Merge(setting.Property);
            this.value = setting.Value.Clone(this.ReadOnly);

            foreach (Option optionSetting in setting.operatorSettings.Values)
            {
                this.operatorSettings.Add(optionSetting).Parent = this;
            }

            Compile(this, setting.operatorSettings);
            return this;
        }

        /// <summary>
        /// 编译本选项节，将执行一些选项命令，具有选项命令的选项节需执行本方法后才可以使用
        /// </summary>
        /// <param name="current">当前选项节</param>
        /// <param name="settings">选项命令集合</param>
        /// <returns>编译后的选项节</returns>
        protected static Option Compile(Option current, OptionCollection settings)
        {
            OptionCollection currentSettings = current.childSettings;
            if (settings.Count > 0)
            {
                for (int i = 0; i < settings.Count; i++)
                {
                    Option setting = settings[i];
                    switch (setting.SettingOperator)
                    {
                        case OptionOperatorEnum.Add:
                            if (currentSettings.Contains(setting.Name))
                            {
                                throw new OptionException(string.Format("已存在子选项节 {0}", setting.Name));
                            }
                            else
                            {
                                currentSettings.Add(setting).Parent = current;
                            }
                            break;
                        case OptionOperatorEnum.Remove:
                            currentSettings.Remove(setting.Name);
                            break;
                        case OptionOperatorEnum.Move:
                            Option moveSetting = currentSettings[setting.Name];
                            if (moveSetting != null)
                            {
                                currentSettings.Remove(moveSetting.Name);
                                currentSettings.Add(moveSetting);
                            }
                            break;
                        case OptionOperatorEnum.Clear:
                            currentSettings.Clear();
                            break;
                        case OptionOperatorEnum.Update:
                            if (currentSettings.Contains(setting.Name))
                            {
                                currentSettings[setting.Name].Merge(setting);
                            }
                            break;
                        case OptionOperatorEnum.Set:
                            if (currentSettings.Contains(setting.Name))
                            {
                                currentSettings[setting.Name].Merge(setting);
                            }
                            else
                            {
                                currentSettings.Add(setting).Parent = current;
                            }
                            break;
                        default:
                            if (currentSettings.Contains(setting.Name))
                            {
                                currentSettings[setting.Name].Merge(setting);
                            }
                            else
                            {
                                currentSettings.Add(setting).Parent = current;
                            }
                            break;
                    }
                }
            }
            return current;
        }

        /// <summary>
        /// 获取根选项节
        /// </summary>
        /// <returns>选项节</returns>
        public virtual Option GetRootSetting()
        {
            Option root = this;
            while (root.parent != null)
            {
                root = root.parent;
            }
            return root;
        }

        /// <summary>
        /// 转换成字符串格式
        /// </summary>
        /// <returns>字符串</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            this.ToString(sb, 0);
            return sb.ToString();
        }

        #endregion members

        #region ICloneable Members

        object ICloneable.Clone()
        {
            return this.Clone(this.ReadOnly, true);
        }

        #endregion

        #region IOptionSetting Members

        bool IOption.ReadOnly
        {
            get { return this.ReadOnly; }
        }

        string IOption.Name
        {
            get { return this.Name; }
        }

        string IOption.OptionName
        {
            get { return this.SettingName; }
        }

        IOptionValue IOption.Value
        {
            get { return this.Value; }
        }

        IOption IOption.Parent
        {
            get { return this.Parent; }
        }

        int IOption.Children
        {
            get { return this.Children; }
        }

        IOptionSection IOption.OptionSection
        {
            get { return this.Property; }
        }

        IOption IOption.this[string childSettingName]
        {
            get { return this[childSettingName]; }
        }

        IOption IOption.this[int childSettingIndex]
        {
            get { return this[childSettingIndex]; }
        }

        IOption[] IOption.GetChildOptions()
        {
            return this.GetChildSettings();
        }

        IOption IOption.GetChildOption(string xpath)
        {
            return this.GetChildSetting(xpath);
        }

        IOption IOption.GetChildOption(params string[] settingName)
        {
            return this.GetChildSetting(settingName);
        }

        IOption IOption.Clone(bool @readonly, bool deep)
        {
            return this.Clone(@readonly, deep);
        }

        IOption IOption.GetRootOption()
        {
            return this.GetRootSetting();
        }

        void IOption.Merge(IOption setting)
        {
            this.Merge((Option)setting);
        }

        #endregion
    }
}