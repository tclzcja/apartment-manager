using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models
{
    public enum CategoryType
    {
        insect = 0
    }

    public enum SubType
    {
        bedbug = 0
    }

    public enum StatusType
    {
        Creating = 0,
        New = 1,
    }

    public enum RoleType
    {
        Tenant = 0,
        Superintenent = 1
    }
}