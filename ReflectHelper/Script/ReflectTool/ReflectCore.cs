using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;


namespace ReflectHelper
{
    /*==============================================================
    * Copyright 2018 Tencent Inc. 
    *
    *  作者：Zach (zachzhong@21kunpeng.com)
    *  时间：2019/2/1 10:33:29
    *  文件名：ReflectCore
    *  版本：V1.0.1  
    *  说明： 
    ========================================*/

    public class ReflectCore
    {
        public List<Dependency> ReflectProperty(Type type)
        {
            List<Dependency> dependencies = new List<Dependency>();
            PropertyInfo[] propInfos = type.GetProperties();

            for(int i = 0; i < propInfos.Length; i++)
            {
                if (propInfos[i].CanWrite)
                {
                    Type propType = propInfos[i].PropertyType;

                    do
                    {
                        //枚举
                        if (propType.IsEnum)
                        {
                            List<EnumStruct> enumStructs = new List<EnumStruct>();
                            FieldInfo[] fields = propType.GetFields();
                            for (int fieldIndex = 1; fieldIndex < fields.Length; fieldIndex++)
                            {
                                string Name = fields[fieldIndex].Name;
                                int Index = (int)fields[fieldIndex].GetValue(null);
                                enumStructs.Add(new EnumStruct()
                                {
                                    index = Index,
                                    name = Name
                                });
                            }
                            dependencies.Add(new EnumDependency(propInfos[i], PropShowType.ENUM, enumStructs));
                            break;
                        }

                        //列表
                        if (typeof(IList).IsAssignableFrom(propType) && propType.IsGenericType)//ArrayList也实现IList，但没有泛型，会出错。ArrayList可以添加任何对象，暂时不做
                        {
                            Type genericType = propType.GetGenericArguments()[0];
                            dependencies.Add(new ListDependency(propInfos[i], PropShowType.LIST));
                            break;
                        }

                        if (propType.IsPrimitive || propType == typeof(string))
                        {
                            dependencies.Add(new Dependency(propInfos[i], PropShowType.PRIMITIVE));
                            break;
                        }

                        //自定义类或者其他类
                        dependencies.Add(new ClassDependency(propInfos[i], PropShowType.CLASS));
                        ////自定义类或者其他类
                        //object propObj = null;
                        //propObj = info.GetValue(instance, null);
                        //propObj = ReflectClass(propObj, currPropType);
                        //info.SetValue(instance, propObj, null);

                    } while (false);
                }

            }

            return dependencies;
        }

        public void Build<T>(ref T _instance,List<Dependency> dependencies) where T:class ,new()
        {
            if (_instance == null)
            {
                _instance = Activator.CreateInstance(typeof(T)) as T;
            }
            PropertyInfo[] propInfos = typeof(T).GetProperties();

            for (int i = 0; i < propInfos.Length; i++)
            {
                Type propType = propInfos[i].PropertyType;
                if(dependencies.Exists(item=> {
                   return item.propType.Equals(propType);
                }))
                {
                    Dependency dependency = dependencies.Find(item => {
                        return item.propType.Equals(propType);
                    });
                    propInfos[i].SetValue(_instance, dependency.Value,null);
                }
            }


        }

    }
}
