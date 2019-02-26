using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecondHandStoreASPNET_Project.Helpers
{
    public static class SignedUserWelcomeDate
    {
        public static string LoginUserMessageTimeOfdayRetrieval()
        {
            UserAuthenticationDetails sfsf = new UserAuthenticationDetails();

            if (sfsf.getFullUser() == null)
            {
                return "";
            }

            else
            {
                var date = DateTime.Now.Hour;

                date = DateTime.Now.Hour;

                if (date < 12)
                {
                    return "Good Morning ";
                }

                else if (date > 12 && date < 17)
                {
                    return "Good Afternoon ";
                }

                else
                {
                    return "Good Evening ";
                }
            }
        }
    }
}