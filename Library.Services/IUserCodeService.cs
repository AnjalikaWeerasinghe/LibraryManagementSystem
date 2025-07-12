using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services
{
    public interface IUserCodeService
    {
        Task<string> GenerateNextAsync(string roleKey, CancellationToken ct = default);
    }
}
