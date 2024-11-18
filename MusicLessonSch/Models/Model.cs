using System.Reflection;

namespace MusicLessonSch.Models
{
    public class Model
    {
        public void MapPropsToVM(ViewModel vm)
        {
            PropertyInfo[] modelProps = this.GetType().GetProperties();
            PropertyInfo[] vmProps = vm.GetType().GetProperties();

            for (int i = 0; i < modelProps.Length; i++)
            {
                string modelPropName = modelProps[i].Name;
                Type mType = modelProps[i].PropertyType;
                for (int j = 0; j < vmProps.Length;j++)
                {
                    string vmPropName = vmProps[j].Name;
                    Type vmType = vmProps[j].PropertyType;
                    if (vmPropName == modelPropName && vmType.FullName == mType.FullName)
                    {
                        var modelVal = modelProps[i].GetValue(this);
                        vmProps[i].SetValue(vm, modelVal);
                    }
                }

            }
        }

        public static void MapListVMToModel(Model[] modelList, ViewModel[] vmList, ViewModel defaultVM)
        {
            if (modelList.Length < 1)
                return;

            for(int i = 0; i < modelList.Length; i++)
            {
                Model model = modelList[i];
                vmList[i] = defaultVM.Copy();
                model.MapPropsToVM(vmList[i]);
            }
        }
    }
}
