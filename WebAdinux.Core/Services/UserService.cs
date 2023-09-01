using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebAdinux.Context.Context;
using WebAdinux.Context.Entities;
using WebAdinux.Context.Enums;
using WebAdinux.Core.Interfaces;
using WebAdinux.Core.Security;
using WebAdinux.Core.ViewMoels;

namespace WebAdinux.Core.Services
{
    public class UserService : IUser
    {
        private readonly DataBaseContext _context;

        public UserService(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<User?> FindUser(LoginViewModel viewModel)
        {
            var usr = await _context.Users.FirstOrDefaultAsync(x=> (x.UserName == viewModel.UserName || x.Email == viewModel.UserName) && x.Password == viewModel.Password.TOMD5() && x.Role == (short)Role.Admin);
            return usr;
        }
    }
}
