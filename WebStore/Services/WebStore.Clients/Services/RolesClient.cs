using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WebStore.Clients.Services
{
    public class RolesClient : BaseClient, IRoleStore<IdentityRole>
    {
        protected override string ServiceAddress { get; } = "api/roles";
        public RolesClient(IConfiguration configuration) : base (configuration)
        {

        }

        public async Task<IdentityResult> CreateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}";
            var result = await PostAsync(url, role);
            var ret = await result.Content.ReadAsAsync<bool>();
            return ret ? IdentityResult.Success : IdentityResult.Failed();
        }
        public async Task<IdentityResult> UpdateAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}";
            var result = await PutAsync(url, role);
            var ret = await result.Content.ReadAsAsync<bool>();
            return ret ? IdentityResult.Success : IdentityResult.Failed();
        }
        public async Task<IdentityResult> DeleteAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/delete";
            var result = await PostAsync(url, role);
            var ret = await result.Content.ReadAsAsync<bool>();
            return ret ? IdentityResult.Success : IdentityResult.Failed();
        }
        public async Task<string> GetRoleIdAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/GetRoleId";
            var result = await PostAsync(url, role);
            var ret = await result.Content.ReadAsAsync<string>();
            return ret;
        }
        public async Task<string> GetRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/GetRoleName";
            var result = await PostAsync(url, role);
            var ret = await result.Content.ReadAsAsync<string>();
            return ret;
        }
        public Task SetRoleNameAsync(IdentityRole role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;
            var url = $"{ServiceAddress}/SetRoleNameAsync/{roleName}";
            return PostAsync(url, role);
        }
        public async Task<string> GetNormalizedRoleNameAsync(IdentityRole role, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/GetNormalizedRoleName";
            var result = await PostAsync(url, role);
            var ret = await result.Content.ReadAsAsync<string>();
            return ret;
        }
        public Task SetNormalizedRoleNameAsync(IdentityRole role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;
            var url =
            $"{ServiceAddress}/SetNormalizedRoleName/{normalizedName}";
            return PostAsync(url, role);
        }
        public async Task<IdentityRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/FindById/{roleId}";
            var result = await GetAsync<IdentityRole>(url);
            return result;
        }
        public async Task<IdentityRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            var url = $"{ServiceAddress}/FindByName/{normalizedRoleName}";
            var result = await GetAsync<IdentityRole>(url);
            return result;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~RolesClient()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
