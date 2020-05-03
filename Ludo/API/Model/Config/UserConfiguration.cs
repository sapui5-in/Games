using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ludo.API.Model.Config
{
    public static class UserConfiguration
    {
        public static void Configure(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(p => p.FirstName)
                .IsRequired(true)
                .HasMaxLength(50);

            // User to Address - One to One Relation
            modelBuilder.Entity<User>()
                .HasOne<Address>(a => a.Address)
                .WithOne(s => s.User)
                .HasForeignKey<Address>(a => a.UserId);

            modelBuilder.Entity<User>()
                .HasMany<Game>(a => a.Games)
                .WithOne(u => u.User)
                .HasForeignKey(g => g.CreatedByUserId);
        }
    }
}
