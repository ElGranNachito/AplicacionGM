using System.Collections.Generic;
using System.Threading.Tasks;
using AppGM.Core;

namespace AppGMCore
{
    public class ViewModelListaRoles : BaseViewModel
    {
        public List<ViewModelRolItem> Roles { get; set; }

        public ViewModelListaRoles()
        {
            Roles = new List<ViewModelRolItem>
            {
                new ViewModelRolItem
                {
                    Nombre = "Super Rol"
                },

                new ViewModelRolItem
                {
                    Nombre =  "Mega Rol"
                },

                new ViewModelRolItem
                {
                    Nombre = "Rol rol rol"
                }
                
            };
        }
    }
}
