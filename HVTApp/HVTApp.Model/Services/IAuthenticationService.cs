using System;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Services
{
    public interface IAuthenticationService
    {
        User GetAuthenticationUser();
    }
    public class NoUserException : Exception { }

}
