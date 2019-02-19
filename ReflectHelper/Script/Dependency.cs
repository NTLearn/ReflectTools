using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReflectHelper
{
    /*==============================================================
    * Copyright 2018 Tencent Inc. 
    *
    *  作者：Zach (zachzhong@21kunpeng.com)
    *  时间：2019/2/1 10:11:11
    *  文件名：Dependency
    *  版本：V1.0.1  
    *  说明： 构造属性依赖树
    ========================================*/

    public struct EnumStruct
    {
        public int index;
        public string name;
    }

    public enum PropShowType
    {
        ENUM,
        LIST,
        PRIMITIVE,
        CLASS
    }

    public class Dependency
    {
        public Dependency(PropertyInfo propertyInfo, PropShowType type)
        {
            propInfo = propertyInfo;
            showType = type;

        }

        public Dependency Parent;

        public List<Dependency> Children;

        public PropertyInfo propInfo;

        public Type propType
        {
            get
            {
                Type type = null;
                if (propInfo != null)
                {
                    type = propInfo.PropertyType;
                }
                return type;
            }
        }

        public PropShowType showType;

        public object Value { get; set; }

        public virtual object GetValue()
        {
            return Value;
        }

        public override string ToString()
        {
            return propType.ToString() + "类型的" + propInfo.Name;
        }
    }

    public class ListDependency : Dependency
    {
        public ListDependency(PropertyInfo prop ,PropShowType type) : base(prop, type)
        {
            ReflectCore rc = new ReflectCore();
            GenericDependency =rc.ReflectProperty(GenericType);
        }

        public List<Dependency> GenericDependency;

        public Type GenericType
        {
            get
            {
                Type type = null;
                if (propInfo!=null)
                {
                    type =  propInfo.PropertyType.GetGenericArguments()[0];
                }

                return type;
            }
        }

        public List<T> CreateListInstance<T>(Type t)
        {
            MethodInfo mi = typeof(ListDependency).GetMethod("MakeList", BindingFlags.NonPublic).MakeGenericMethod(t);
            return mi.Invoke(this,null) as List<T>;
        }

        List<T> MakeList<T>(){
            return new List<T>();
        }
    }

    public class EnumDependency : Dependency
    {

        public List<EnumStruct> EnumStructList;

        public EnumDependency(PropertyInfo propertyInfo, PropShowType type) : base(propertyInfo, type)
        {
        }
    }

    public class ClassDependency : Dependency
    {
        public ClassDependency(PropertyInfo propertyInfo, PropShowType type) : base(propertyInfo, type)
        {

        }

        public override object GetValue()
        {
            //return base.GetValue();
            ReflectCore rc = new ReflectCore() ;
            MethodInfo methods = propType.GetMethod("Build");
            methods.MakeGenericMethod(propType).Invoke(rc,new object[] { Value, Children });
            return Value;
        }
    }
}
