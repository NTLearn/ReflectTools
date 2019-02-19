using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReflectHelper.UIModule
{
    /// <summary>
    /// 绘制依赖UI的逻辑层。通过调用DrawTools的派生类绘制
    /// </summary>
    public class UIPainter
    {
        UIDrawTool mTool;

        public UIPainter(UIDrawTool tool)
        {
            mTool = tool;
        }
    }
}
