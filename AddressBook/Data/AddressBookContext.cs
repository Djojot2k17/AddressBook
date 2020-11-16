using AddressBook.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddressBook.Data
{
  public class AddressBookContext : DbContext
  {
      public AddressBookContext(DbContextOptions<AddressBookContext> options) : base(options)
      {

      }
      public DbSet<AddressBookEntry> AddressBooks { get; set; }
  }
}
