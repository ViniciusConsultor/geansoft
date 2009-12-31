using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using Gean.Exceptions;

namespace Gean.Config.DotNetConfig
{
    /// <summary>
    /// �����飨���� .NET�����üܹ���
    /// </summary>
    /// <remarks>
    ///		<code>
    ///			&lt;configSections&gt;
    ///				&lt;sectionGroup name="htb.devfx" type="Gean.Config.DotNetConfig.GroupHandler, HTB.DevFx.BaseFx"&gt;
    ///					&lt;section name="mail" type="HTB.DevFx.Utils.Mail.Config.SectionHandler, HTB.DevFx.BaseFx" /&gt;
    ///					......
    ///				&lt;/sectionGroup&gt;
    ///			&lt;/configSections&gt;
    /// 
    ///			......
    /// 
    ///			&lt;htb.devfx&gt;
    ///				&lt;mail&gt;
    ///					&lt;smtpSetting server="" port="" userName="" password="" /&gt;
    ///				&lt;/mail&gt;
    ///			&lt;/htb.devfx&gt;
    ///			......
    /// </code>
    /// </remarks>
    public class GroupHandler : ConfigurationSectionGroup
    {
        private static GroupHandler instance;
        private static bool isInit;
        private static bool isWebApp;
        private static Dictionary<Type, ConfigurationSection> sectionCache;
        private static readonly object lockObject = new object();

        private static void Init(bool throwOnError)
        {
            if (isInit)
            {
                return;
            }
            Configuration config;
            if (HttpContext.Current != null)
            {
                isWebApp = true;
                config = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            }
            else
            {
                isWebApp = false;
                config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            foreach (string key in config.SectionGroups.Keys)
            {
                ConfigurationSectionGroup csg = config.SectionGroups[key];
                if (csg == null || string.IsNullOrEmpty(csg.Type))
                {
                    continue;
                }
                Type csgType = UtilityType.CreateType(csg.Type, false);
                if (csgType == typeof(GroupHandler))
                {
                    break;
                }
            }

            sectionCache = new Dictionary<Type, ConfigurationSection>();

            if (instance == null)
            {
                isInit = true;
                if (throwOnError)
                {
                    throw new ConfigException("������δ��ȷ����");
                }
                return;
            }

            foreach (ConfigurationSection section in instance.Sections)
            {
                string sectionTypeName = section.SectionInformation.Type;
                string sectionName = section.SectionInformation.SectionName;
                Type sectionType = UtilityType.CreateType(sectionTypeName, false);
                object objectSection;
                if (isWebApp)
                {
                    objectSection = WebConfigurationManager.GetSection(sectionName);
                }
                else
                {
                    objectSection = ConfigurationManager.GetSection(sectionName);
                }
                if (objectSection != null)
                {
                    sectionCache.Add(sectionType, (ConfigurationSection)objectSection);
                }
            }

            isInit = true;
        }

        /// <summary>
        /// ��ǰ�������ʵ����������
        /// </summary>
        public static GroupHandler Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// ��ȡ���ýڣ����ͣ�
        /// </summary>
        /// <typeparam name="T">���ý�����</typeparam>
        /// <returns>���ý�</returns>
        public static T GetSection<T>() where T : ConfigurationSection
        {
            return GetSection<T>(true);
        }

        /// <summary>
        /// ��ȡ���ýڣ����ͣ�
        /// </summary>
        /// <typeparam name="T">���ý�����</typeparam>
        /// <param name="throwOnError">���������δ�����Ƿ��׳��쳣</param>
        /// <returns>���ý�</returns>
        public static T GetSection<T>(bool throwOnError) where T : ConfigurationSection
        {
            if (!isInit)
            {
                lock (lockObject)
                {
                    if (!isInit)
                    {
                        Init(throwOnError);
                    }
                }
            }

            Type type = typeof(T);
            ConfigurationSection sectionObject;
            sectionCache.TryGetValue(type, out sectionObject);
            return sectionObject as T;
        }

        /// <summary>
        /// ���췽��
        /// </summary>
        protected GroupHandler()
        {
            instance = this;
        }
    }
}