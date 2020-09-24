using AppGM.Core;

namespace AppGM
{
    public class DesignModelRolItem : BaseDesignModel<DesignModelRolItem, ViewModelRolItem>
    {
        public static ViewModelRolItem rolItem { get; set; } = new ViewModelRolItem();
        public DesignModelRolItem()
        {
            rolItem.Nombre = "Sisi";
        }

    }
}
