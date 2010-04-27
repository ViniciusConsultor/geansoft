using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Gean.Client.IHMSJsonBuilder
{

    public class JsonService
    {
        private static JSONObject Instance = null;

        public static JSONObject getJsonObject()
        {
            if (Instance == null)
            {
                Instance = new JSONObject();
            }
            return Instance;
        }
    }


    /*
     * 统计结果集合
     */
    public class AmountCollection : AbstractDictionary<Branch>
    {
    }

    /*
     * 机构集合
     */
    public class Branch : AbstractDictionary<AmountObject>
    {
    }

    public class Opreation : CommonAmountObject { }
    public class Worker : CommonAmountObject { }
    public class Counter : CommonAmountObject { }

    public abstract class CommonAmountObject : AmountObject
    {
        public CommonAmountObject()
        {
            this.add("D", new Amount());
            this.add("W", new Amount());
            this.add("W2", new Amount());
            this.add("M", new Amount());
            this.add("Q", new Amount());
            this.add("M6", new Amount());
            this.add("Y", new Amount());
            this.add("LY", new Amount());//上年度
        }
    }

    /*
     * 统计对象相关统计数据集合，一般包含不同时间段，如天，周，月等
     */
    public abstract class AmountObject : AbstractDictionary<Amount>
    {
        protected String _id;
        /*
         * 获取统计对象的id
         */
        public String getid()
        {
            return _id;
        }
        /*
         * 设置统计对象的id
         */
        public void setid(String id)
        {
            _id = id;
        }
    }

    /*
     * 统计数据集合的集合，一般包括TD，ED，WD，FD等数据，Key一般设置为较简单的英文说明
     */
    public class Amount : AbstractDictionary<D>
    {
    }

    /*
     * 与号票相关【正在等待，正在办理，取号量，有效服务人数，弃号人量】
     */
    public class TD : D
    {
        public TD()
            : base(5)
        {
        }
    }

    /*
     * 与评价相关【评价人数，未评价，未请评价，非常满意人数，满意人数，一般人数，不满意人数】
     */
    public class ED : D
    {
        public ED()
            : base(7)
        {
        }
    }

    /*
     * 与服务等待时间相关【<=3分钟，3至5分钟，5至10分钟，10至20分钟，20分钟以上】
     */
    public class WD : D
    {
        public WD()
            : base(5)
        {
        }
    }

    /*
     * 与服务效率相关【平均服务时间，最长服务时间，最短服务时间】
     */
    public class FD : D
    {
        public FD()
            : base(3)
        {
        }
    }

    /*
     * 统计数据
     */
    public abstract class D : IToJson
    {
        protected int[] _value;

        /*
         * 获取数据
         */
        public int[] getV()
        {
            return _value;
        }

        /*
         * 设置数据
         */
        public void setV(int[] value)
        {
            _value = value;
        }

        /*
         * 设置集合中指定索引位置的数据
         */
        public void setV(int index, int value)
        {
            if (index <0 || index >= _value.Length)
            {
                //TODO:报错
                //TODO:写日志
            }
            _value[index] = value;
        }

        public D(int count)
        {
            _value = new Int32[count];
        }

        public String toJson()
        {
            return JsonService.getJsonObject().fromObject(this).ToString();
        }
    
    }

    public abstract class AbstractDictionary<T> : IToJson
    {

        protected Dictionary<String, T> _dictionary;
        /*
         * 获取数据
         */
        public Dictionary<String, T> getDictionary()
        {
            return _dictionary;
        }

        public AbstractDictionary()
        {
            _dictionary = new Dictionary<string, T>();
        }

        public void add(String key, T value)
        {
            _dictionary.Add(key, value);
        }

        public void clear()
        {
            _dictionary.Clear();
        }

        public bool contains(String key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void remove(String key)
        {
            _dictionary.Remove(key);
        }

        public String toJson()
        {
            return JsonService.getJsonObject().fromObject(this).ToString();
        }
    
    }


    public interface IToJson
    {
        String toJson();
    }





    public class JSONObject
    {
        public Object fromObject(Object obj)
        {
            return null;
        }
    }

}
