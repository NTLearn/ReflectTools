using System;

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
        public UIControls Controls;

        /// <summary>
        /// 当前item对应的依赖
        /// </summary>
        public Dependency mDependency;

        /// <summary>
        /// 将UI上的值绑定到依赖上的方法
        /// </summary>
        public void SetValue()
        {
            if(Controls == null)
            {
                return;
            }
            object obj = null;
            if (Controls.GetValueHandle != null){
                obj = Controls.GetValueHandle.Invoke(Controls.Contols);
            }
            mDependency.Value = obj;
        }
    }

    public class UIControls
    {
        public delegate object GetValue(object value);

        public object Contols;

        public GetValue GetValueHandle;
    }
}
