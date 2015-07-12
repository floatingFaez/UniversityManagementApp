using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UniversityManagementSystemApp.Context;

namespace UniversityManagementSystemApp.Models
{
    public class SampleData: DropCreateDatabaseIfModelChanges<UniversityDbContext>
    {
    }
}