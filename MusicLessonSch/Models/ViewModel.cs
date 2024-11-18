using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Reflection;

namespace MusicLessonSch.Models
{
    public class ViewModel
    {
        /*
            copy the primitive type properties
            i.e., do not copy over reference types
            (e.g., user-defined objects or other objects)
         */
        public virtual ViewModel Copy()
        {
            throw new NotImplementedException();
        }
        public void MapPropsToModel(Model model)
        {
            PropertyInfo[] modelProps = model!.GetType().GetProperties();
            PropertyInfo[] vmProps = this.GetType().GetProperties();

            for (int i = 0; i < vmProps.Length; i++)
            {
                string vmPropName = vmProps[i].Name;
                var vmVal = vmProps[i].GetValue(this);
                Type vmType = vmProps[i].PropertyType;
                for(int j = 0; j < modelProps.Length; j++)
                {
                    string mPropName = modelProps[j].Name;
                    Type mType = modelProps[j].PropertyType;
                    if (vmPropName == mPropName && mType.FullName == vmType.FullName)
                    {
                        modelProps[j].SetValue(model, vmVal);
                    }
                }

            }

        }

        public static void MapListPropsToModel(ViewModel[] vmList, Model[] modelList, Model defaultModel)
        {
            if (vmList.Length < 1)
            {
                return;
            }

            for(int i = 0; i < vmList.Length; i++)
            {
                modelList[i] = defaultModel.Copy();
                vmList[i].MapPropsToModel(modelList[i]);
            }
        }


    }
}
