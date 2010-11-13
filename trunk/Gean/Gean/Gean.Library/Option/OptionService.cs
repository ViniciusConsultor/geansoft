using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Xml;
using NLog;

namespace Gean.Options
{
public abstract class OptionService : IService
{
    private static Logger _Logger = LogManager.GetCurrentClassLogger();

	public static String FILL_DATA_METHOD_NAME = "Initializes";

	protected OptionService()
	{

	}

	private XmlDocument _SettingDocument = null;

	protected OptionMap _SettingCollection = new OptionMap();

	public bool Initializes(params object[] objects)
	{
		if (objects[0] is XmlDocument)
			_SettingDocument = (XmlDocument) objects[0];
		Dictionary<String, XmlElement> classnameANDelementMap = this.getXmlElementByClassMap(_SettingDocument);
		for (String classname : classnameANDelementMap.keySet())
		{
			// 第1步： 获取指定的类型 ==========================
			Mirror<?> mirror;
			Class<?> klass = null;
			try
			{
				klass = Class.forName(classname);
			}
			catch (ClassNotFoundException e1)
			{
				this.logWarn(classname + " 类型未发现。", e1);
				return false;
			}
			mirror = Mirror.me(klass);

			// 第2步： 获取指定对象的实例 ==========================
			Setting childSetting = null;
			try
			{
				childSetting = (Setting) mirror.born();
			}
			catch (Exception e)
			{
				this.logWarn(childSetting.toString() + " 获取指定对象的实例出错。", e);
				return false;
			}

			// 第3步： 调用fillSettingData方法 ====================
			try
			{
				this.filldata(classnameANDelementMap.get(classname), childSetting);
			}
			catch (Exception e)
			{
				this.logWarn(String.format("调用 fillSettingData 的 %s 相关方法出错。", classname), e);
				return false;
			}
			_SettingCollection.put(classnameANDelementMap.get(classname).attributeValue("name").toLowerCase(), childSetting);
		}
		return (null != _SettingDocument) && (_SettingCollection.size() > 0);
	}

	/**
	 * 从指定的Document中的约定的节点遍历获得所有定义的“设置”的类的全名
	 * 
	 * @param document
	 * @return
	 */
	protected Dictionary<String, XmlElement> getXmlElementByClassMap(XmlDocument document)
	{
		Dictionary<String, XmlElement> classStrings = new Dictionary<String, XmlElement>();
		List<?> nodes = document.getRootXmlElement().selectNodes("//setting[@class]");
		for (Object object : nodes)
		{
			if (!(object is XmlElement))
				continue;
			XmlElement element = (XmlElement) object;
			String className = element.attributeValue("class");
			this.logDebug("启用Setting配置类:" + className);
			classStrings.put(className, element);
		}
		return classStrings;
	}

	/**
	 * 将指定的 XmlElement 交给具体的 Setting 类的 filldata 方法实现该类的赋值。
	 * 
	 * @param element
	 * @param obj
	 * @throws NoSuchFieldException
	 * @throws FailToSetValueException
	 */
	protected IOption filldata(XmlElement element, IOption setting)
	{
		Method method = null;
		try
		{
			method = InvokePrivateMethod.getPrivateMethod(setting.getClass(), FILL_DATA_METHOD_NAME, XmlElement.class);
		}
		catch (Exception e)
		{
			this.logWarn(String.format("获取类型方法%s时发生异常。", FILL_DATA_METHOD_NAME), e);
		}
		if (null == method)
			this.logWarn(String.format("无法找到类型中的%s方法。", FILL_DATA_METHOD_NAME));
		try
		{
			method.setAccessible(true);// 调用 private方法的关键一句话
			method.invoke(setting, element);
		}
		catch (Exception e)
		{
			this.logWarn("反射执行方法出错。方法名:" + method.getName() + "。被调用对象：" + setting.getClass().getSimpleName(), e);
		}
		return setting;
	}

	/*
	 * (non-Javadoc)
	 * @see com.pan.util.module.IManager#reLoad(org.dom4j.Document)
	 */
	public bool ReStart()
	{
		_SettingCollection.clear();
		try
		{
			this.Initializes(_SettingDocument);
		}
		catch (Exception ex)
		{
			return false;
		}
		return true;
	}

}
}