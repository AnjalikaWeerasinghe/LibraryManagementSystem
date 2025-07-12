using Library.Models;
using Library.Repositories;
using Library.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public class UserCodeService : IUserCodeService
    {
        private readonly ApplicationDbContext _db;

        public UserCodeService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<string> GenerateNextAsync(string roleKey, CancellationToken ct = default)
        {
            roleKey = roleKey?.Trim();

            string? prefix = roleKey switch
            {
                "Member" => "MEM",
                "Staff" => "STF",
                _ => null
            };

            if (prefix is null)
                throw new ArgumentException("Role key must be 'Member' or 'Staff'", nameof(roleKey));

            // Find or create the counter
            var counter = await _db.UserCodeCounters
                .SingleOrDefaultAsync(c => c.RoleKey == prefix, ct);

            if (counter == null)
            {
                counter = new UserCodeCounter { RoleKey = prefix, LastNumber = 0 };
                _db.UserCodeCounters.Add(counter);
            }

            counter.LastNumber++;
            try
            {
                await _db.SaveChangesAsync(ct);
            }
            catch (DbUpdateConcurrencyException)
            {
                return await GenerateNextAsync(roleKey, ct); // retry once
            }

            return $"LIB-{prefix}-{counter.LastNumber:0000}";
        }
    }
}
