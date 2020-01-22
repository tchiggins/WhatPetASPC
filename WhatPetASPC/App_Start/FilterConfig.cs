using System.Diagnostics.Contracts;
using System.Web.Mvc;
namespace WhatPetASPC
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            Contract.Requires(filters != null);
            filters.Add(new HandleErrorAttribute());
        }
    }
}