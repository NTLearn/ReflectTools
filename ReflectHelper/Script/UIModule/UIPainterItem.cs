using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectHelper.UIModule
{
    /// <summary>
    /// 用于绑定UI与依赖的类
    /// </summary>
    public class UIPainterItem
    {
        /// <summary>
        /// 当前item对应的UI
        /// </summary>
        public object Controls;

        /// <summary>
        /// 当前item对应的依赖
        /// </summary>
        public Dependency mDependency;

        /// <summary>
        /// 将UI上的值绑定到依赖上的方法
        /// </summary>
        public void SetValue()
        {

        }
    }
}
