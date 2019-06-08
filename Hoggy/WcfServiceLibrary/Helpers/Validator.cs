using DataTransferObjects;
using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Bases;

namespace WcfServiceLibrary.Helpers
{
    public static class Validator
    {
        public static bool HasAccess<T>(IRepository repository, AuthenticationToken token, T entity) 
            where T : BaseSecureEntity
        {
            UserEntity user = repository.GetItem<AuthenticationTokenEntity>(x => x.Value == token.Value).User;

            if (user == null || entity == null)
                return false;

            SecurityGroupEntity securityGroup = repository.GetItem<SecurityGroupEntity>(x => x.Id == entity.SecurityGroupId);

            return securityGroup.Users.Contains(user);
        }
    }
}
