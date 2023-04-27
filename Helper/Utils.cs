using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using learnnet.Helper;
using learnnet.Models;

public class Utils : Permission
{

    readonly CustomQuery CQ = new CustomQuery();

    public static bool IsAdmin(string email)
    {
        return CustomQuery.IsAdmin(email);
    }

    public static bool IsEditor(string email)
    {
        return HasEdit(email);
    }
     
    public static bool IsSuperadmin(string email)
    {
        return CustomQuery.IsSuperAdmin(email);
    }

    public static bool HasDelete(string email)
    { 
        return CustomQuery.HasDeletePermission(email);
    }

    public static bool HasEdit(string email)
    {
        return CustomQuery.HasEditPermission(email);
    }

    public static bool HasCreate(string email)
    {
        return CustomQuery.HasCreatePermission(email);
    }

    public static bool LoggedUser(string email)
    {
        return CustomQuery.LoggedInUser(email);
    }

    public static string GetUserRole(int id)
    {
        string role = CustomQuery.SelectUser(id).role;
        return role;
    }

}

     

  
