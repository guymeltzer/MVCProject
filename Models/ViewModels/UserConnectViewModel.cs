using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModels
{
    public class UserConnectViewModel
    {

        private bool IsConnect;
        private string Name;

        public UserConnectViewModel(string Name, bool IsConnect)
        {
            this.Name = Name;
            this.IsConnect = IsConnect;
        }

        public string getName() { return this.Name; }
        public bool getIsConnect
        {
            get
            {
                return this.IsConnect;
            }
        }

            
            
    }
}
