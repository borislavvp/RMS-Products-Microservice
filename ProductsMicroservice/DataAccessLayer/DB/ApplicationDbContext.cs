using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using DatabaseLayer.Entities;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DatabaseLayer.DB
{
    class ApplicationDbContext : DbContext
    {
    }
}
