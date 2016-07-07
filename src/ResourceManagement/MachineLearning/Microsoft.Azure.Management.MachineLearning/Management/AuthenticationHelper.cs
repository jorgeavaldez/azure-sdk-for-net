using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.Azure.MachineLearning
{
    public enum LoginType
    {
        /// <summary>
        /// User is logging in with orgid credentials
        /// </summary>
        OrgId,

        /// <summary>
        /// User is logging in with liveid credentials
        /// </summary>
        LiveId
    }

    public interface IAccessToken
    {
        void AuthorizeRequest(Action<string, string> authTokenSetter);

        string AccessToken { get; }

        string UserId { get; }

        string TenantId { get; }

        LoginType LoginType { get; }
    }

    /// <summary>
    /// Implementation of <see cref="IAccessToken"/> using data from ADAL
    /// </summary>
    public class AdalAccessToken : IAccessToken
    {
        internal readonly AdalConfiguration Configuration;
        internal AuthenticationResult AuthResult;
        private readonly UserTokenProvider tokenProvider;

        public AdalAccessToken(AuthenticationResult authResult, UserTokenProvider tokenProvider, AdalConfiguration configuration)
        {
            AuthResult = authResult;
            this.tokenProvider = tokenProvider;
            Configuration = configuration;
        }

        public void AuthorizeRequest(Action<string, string> authTokenSetter)
        {
            tokenProvider.Renew(this);
            authTokenSetter(AuthResult.AccessTokenType, AuthResult.AccessToken);
        }

        public string AccessToken { get { return AuthResult.AccessToken; } }

        public string UserId { get { return AuthResult.UserInfo.DisplayableId; } }

        public string TenantId { get { return AuthResult.TenantId; } }

        public LoginType LoginType
        {
            get
            {
                if (AuthResult.UserInfo.IdentityProvider != null)
                {
                    return LoginType.LiveId;
                }
                return LoginType.OrgId;
            }
        }
    }

    public class AuthenticationHelper
    {
        AuthenticationHelper()
        {
            
        }

        public IAccessToken GetAccessToken()
        {
            return null;
        }
    }
}
