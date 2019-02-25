using System;
using System.Collections.Generic;
using System.Reflection;

namespace ReflectHelper.UIModule
{
    /// <summary>
    /// 绘制依赖UI的逻辑层。通过调用DrawTools的派生类绘制
    /// </summary>
    public class UIPainter
    {
        UIDrawTool mTool;

        //public UIPainter(UIDrawTool tool)
        //{
        //    mTool = tool;
        //}
        List<UIPainterItem> uiLogicItems;

        public UIPainter()
        {
            uiLogicItems = new List<UIPainterItem>();
        }

        public void SetUiTool(UIDrawTool tool)
        {
            mTool = tool;
        }

        public void Draw(List<Dependency> dependencies)
        {
            if(dependencies == null || dependencies.Count ==0)
            {
                return;
            }
            for(int i = 0; i < dependencies.Count; i++)
            {
                DrawUIInput(dependencies[i]);
                Draw(dependencies[i].Children);
            }
        }

        public UIControls DrawUIInput(Dependency dependency)
        {
            if (mTool == null)
            {
                return null;
            }
            UIPainterItem item = new UIPainterItem();
            item.mDependency = dependency;
            switch (dependency.showType)
            {
                case PropShowType.CLASS:
                    break;
                case PropShowType.ENUM:
                     EnumDependency enumDependency = dependency as EnumDependency;
                    item.Controls = mTool.DrawEnumInputArea(dependency.propInfo.Name, dependency.Value, enumDependency.EnumStructList);
                    break;
                case PropShowType.LIST:
                    ListDependency listDependency = dependency as ListDependency;
                    MethodInfo mi = typeof(UIDrawTool).GetMethod("DrawListInputArea");
                    item.Controls = mi.MakeGenericMethod(listDependency.GenericType).Invoke(mTool, new object[] {
                        dependency.propInfo.Name,
                        dependency.Value,
                       new Func<object>( ()=> {
                        return DrawUIInput(listDependency.GenericDependency[0]);
                    })
                    }) as UIControls;
                    break;
                case PropShowType.PRIMITIVE:
                    item.Controls = mTool.DrawPrimativeInputArea(dependency.propInfo.Name,dependency.Value);
                    break;
            }
            uiLogicItems.Add(item);
            return item.Controls;
        }

        public void SetValue()
        {
            if(uiLogicItems== null || uiLogicItems.Count == 0)
            {
                return;
            }

            for(int i = 0; i < uiLogicItems.Count; i++)
            {
                uiLogicItems[i].SetValue();
            }
        }

    }
}
