using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebformsCms.Src
{
    public interface IAuthentication
    {
        bool IsAdminEdit();
    }
    public class Authentication : IAuthentication
    {
        private static Authentication instance;
        private Authentication() { }

        public static Authentication Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Authentication();
                }
                return instance;
            }
        }

        public bool IsAdminEdit()
        {
            return true;
        }

        public int GetUserId()
        {
            return 1;
        }
    }
}