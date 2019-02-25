using System;
using System.Collections.Generic;

namespace ReflectHelper.UIModule
{
    /// <summary>
    /// 用于定义各种类型的输入绘制方法。
    /// </summary>
    public abstract class UIDrawTool
    {
        public abstract UIControls DrawListInputArea<T>(string name,object list,Func<UIControls> addItemHandler);

        public abstract UIControls DrawClassInputArea(string name, object value);

        public abstract UIControls DrawPrimativeInputArea(string name, object value);

        public abstract UIControls DrawEnumInputArea(string name, object value, List<EnumStruct> enumInfoList);
    }

}
