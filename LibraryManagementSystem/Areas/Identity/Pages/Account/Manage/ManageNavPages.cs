
#nullable disable

using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace  LibraryManagementSystem.Areas.Identity.Pages.Account.Manage
{
    
    public static class ManageNavPages
    {
        
        public static string Index => "Index";

        public static string MyEvent => "MyEvent";

        public static string MyItem => "MyItem";

        public static string MyPayment => "MyPayment";

        public static string Email => "Email";

        public static string ChangePassword => "ChangePassword";


        public static string IndexNavClass(ViewContext viewContext) => PageNavClass(viewContext, Index);

        public static string MyEventNavClass(ViewContext viewContext) => PageNavClass(viewContext, MyEvent);

        public static string MyItemNavClass(ViewContext viewContext) => PageNavClass(viewContext, MyItem);

        public static string MyPaymentNavClass(ViewContext viewContext) => PageNavClass(viewContext, MyPayment);

        public static string EmailNavClass(ViewContext viewContext) => PageNavClass(viewContext, Email);

        public static string ChangePasswordNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChangePassword);

        
        public static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
