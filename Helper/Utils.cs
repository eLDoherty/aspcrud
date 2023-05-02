using System.Collections.Generic;
using learnnet.Helper;
using learnnet.Models;

public class Utils : Permission
{

    readonly CustomQuery CQ = new CustomQuery();
    readonly Pagination PG = new Pagination();

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

    public static int CurrentUserId(string email)
    {
        int id = CustomQuery.GetCurrentUserId(email);
        return id;
    }

    public static List<Section> AllSectionOrder()
    {
       return CustomQuery.AllSectionOrder();
    }

    public static string BaseURL()
    {
        return System.Configuration.ConfigurationManager.AppSettings["BaseRef"];
    }

    public static string SSLbaseUrl()
    {
        return System.Configuration.ConfigurationManager.AppSettings["BaseRefSSL"];

    }
    public static bool IsHasSomePermission(int sectionId, int userId)
    {
        if (GetAddPermission(sectionId, userId) || GetEditPermission(sectionId, userId) || GetDeletePermission(sectionId, userId))
        {
            return true;
        }
        return false;
    }

    // Total user record
    public static int TotalUser()
    {
        return Pagination.TotalUserRecord() - 1 ;
    }

}