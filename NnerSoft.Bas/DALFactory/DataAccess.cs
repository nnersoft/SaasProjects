﻿using System;
using System.Reflection;
using NnerSoft.Commom;
namespace NnerSoft.Bas.DALFactory
{
    public class DataAccess<t>
    {
        protected static readonly string AssemblyPath = ConfigHelper.GetConfigString("DAL");
        protected static object CreateObjectNoCache(string AssemblyPath, string classNamespace)
        {
            object result;
            try
            {
                object obj = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
                result = obj;
            }
            catch
            {
                result = null;
            }
            return result;
        }
        protected static object CreateObject(string AssemblyPath, string classNamespace)
        {
            object obj = DataCache.GetCache(classNamespace);
            if (obj == null)
            {
                try
                {
                    obj = Assembly.Load(AssemblyPath).CreateInstance(classNamespace);
                    DataCache.SetCache(classNamespace, obj);
                }
                catch
                {
                }
            }
            return obj;
        }
        public static t Create(string ClassName)
        {
            string classNamespace = DataAccess<t>.AssemblyPath + "." + ClassName;
            object obj = DataAccess<t>.CreateObject(DataAccess<t>.AssemblyPath, classNamespace);
            return (t)((object)obj);
        }
    }
}
