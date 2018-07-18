using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YeniDersProgrami.Models;

namespace YeniDersProgrami
{
    public static class Auth
    {
        private const string UserKey = "YeniDersProgrami.Auth.Userkey";
        public static Admin User
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    return null;
                }

                var user = HttpContext.Current.Items["UserKey"] as Admin;

                if (user == null)
                {
                    user = new Admin { Name = "Yönetici" };

                }


                if (user == null)
                {
                    return null;
                }


                HttpContext.Current.Items["UserKey"] = user;

                return user;
            }
        }
    }
}