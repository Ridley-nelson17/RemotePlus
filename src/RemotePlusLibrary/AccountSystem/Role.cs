﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace RemotePlusLibrary.AccountSystem
{
    [DataContract]
    public class Role
    {
        public static Role Empty = new Role();
        public List<UserAccount> Members { get; private set; } = new List<UserAccount>();
        public static RolePool GlobalPool { get; private set; } = new RolePool();
        private Role()
        {
            RoleName = "NewRole";
            Privilleges = new SecurityPolicyFolder();
        }
        private Role(string roleName, SecurityPolicyFolder priv)
        {
            RoleName = roleName;
            this.Privilleges = priv;
        }
        public static Role GetRole(string roleName)
        {
            try
            {
                return GlobalPool.Roles.First(t => t.RoleName.ToLower() == roleName.ToLower());
            }
            catch (ArgumentNullException)
            {
                throw new RoleException("Role does not exist.");
            }
        }
        public static void InitializeRolePool()
        {
            GlobalPool = new RolePool();
        }
        public static Role CreateRole(string roleName)
        {
            return new Role(roleName, new SecurityPolicyFolder());
        }
        [DataMember]
        public string RoleName { get; set; }
        [DataMember]
        [Category("Security")]
        [Browsable(true)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public SecurityPolicyFolder Privilleges { get; set; }
        public override string ToString()
        {
            return RoleName;
        }
    }
}
