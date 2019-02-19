using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Dependency> dependencies = new List<Dependency>();
            dependencies = GetDependencyProp(typeof(TestClass));
            foreach(var d in dependencies)
            {
                Console.WriteLine(d.ToString());
            } 
                Console.ReadKey();
        }

        public static List<Dependency> GetDependencyProp(Type type)
        {
            List<Dependency> dependencies = new List<Dependency>();
            ReflectCore core = new ReflectCore();
            dependencies = core.ReflectProperty(type);
            foreach(var dependency in dependencies)
            {
                switch (dependency.showType)
                {
                    case PropShowType.CLASS:
                        dependency.Children = GetDependencyProp(dependency.propType);
                        break;
                    case PropShowType.LIST:
                        ListDependency listDependency = dependency as ListDependency;
                        listDependency.Children = GetDependencyProp(listDependency.GenericType);
                        break;
                    case PropShowType.ENUM:
                        break;
                    case PropShowType.PRIMITIVE:
                        break;
                }
            }
            return dependencies;
        }
    }
}
