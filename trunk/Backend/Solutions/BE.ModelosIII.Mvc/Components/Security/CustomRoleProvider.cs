using System;
using System.Configuration.Provider;
using System.Linq;
using System.Web.Security;
using BE.ModelosIII.Domain;
using BE.ModelosIII.Domain.Contracts.Repositories;
using Microsoft.Practices.ServiceLocation;

namespace BE.ModelosIII.Mvc.Components.Security
{
    public class CustomRoleProvider : RoleProvider
    {
        public const int RoleNameMaxLength = 255;

        /// <summary>
        /// Este miembro se usa solo para testing unitario.
        /// </summary>
        private IUserRepository auxUserRepo;

        /// <summary>
        /// Este miembro se usa solo para testing unitario.
        /// </summary>
        private IRolRepository auxRoleRepo;

        /// <summary>
        /// Repositorio de usuarios a utilizar por el provider.
        /// Si se seteó un repositorio de usuarios se devuelve ese. Si no se crea un nuevo cada vez para
        /// que el provider sea thread safe (o HTTPRequest safe en este caso)
        /// </summary>
        public IUserRepository UserRepository
        {
            get
            {
                if (auxUserRepo != null)
                {
                    return auxUserRepo;
                }
                return ServiceLocator.Current.GetInstance<IUserRepository>();
            }
            set
            {
                auxUserRepo = value;
            }
        }

        /// <summary>
        /// Repositorio de roles a utilizar por el provider.
        /// Si se seteó un repositorio de usuarios se devuelve ese. Si no se crea un nuevo cada vez para
        /// que el provider sea thread safe (o HTTPRequest safe en este caso)
        /// </summary>
        public IRolRepository RoleRepository
        {
            get
            {
                if (auxRoleRepo != null)
                    return auxRoleRepo;
                return ServiceLocator.Current.GetInstance<IRolRepository>();
            }
            set
            {
                auxRoleRepo = value;
            }
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            if (username == null) throw new ArgumentNullException();
            if (roleName == null) throw new ArgumentNullException();
            var usuario = UserRepository.GetByEmail(username);
            if (usuario == null) throw new ProviderException(string.Format("No existe un usuario de nombre {0}", username));
            var rol = RoleRepository.GetByName(roleName);
            CheckRolNotNull(rol, roleName);
            return usuario.Role == rol;
        }

        public override string[] GetRolesForUser(string username)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentException();
            var usuario = UserRepository.GetByEmail(username);
            return new [] {usuario.Role.Name};
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
            //CheckRoleNameArgument(roleName);
            //if (RoleRepository.GetByMultiplexAndName(roleName) != null) throw new ProviderException(string.Format("Ya existe un rol con el nombre {0}", roleName));

            //RoleRepository.SaveOrUpdate(new Role {Name = roleName});
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            if (string.IsNullOrEmpty(roleName)) throw new ArgumentException();
            return RoleRepository.GetByName(roleName) != null;
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            CheckRoleNameArgument(roleName);
            var rol = RoleRepository.GetByName(roleName);
            CheckRolNotNull(rol, roleName);
            return UserRepository.FindyByRol(rol).Select(u => u.Name).ToArray();
        }

        public override string[] GetAllRoles()
        {
            return RoleRepository.GetAll().Select(r => r.Name).ToArray();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }

        private static void CheckRoleNameArgument(string roleName)
        {
            if (string.IsNullOrEmpty(roleName)) throw new ArgumentException();
            if (roleName.Contains(",") || roleName.Length > RoleNameMaxLength)
                throw new ArgumentException(
                    string.Format("El nombre de rol no puede tener comas ni más de {0} caracteres.", RoleNameMaxLength), "roleName");
        }

        private static void CheckRolNotNull(Role rol, string roleName)
        {
            if (rol == null)
            {
                throw new ProviderException(string.Format("No existe un rol de nombre {0}", roleName));
            }
        }
    }
}