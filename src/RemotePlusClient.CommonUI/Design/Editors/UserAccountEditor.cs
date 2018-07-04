﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RemotePlusClient.CommonUI.Design.Editors.GUI;
using RemotePlusLibrary.Security.AccountSystem;

namespace RemotePlusClient.CommonUI.Design.Editors
{
    public class UserAccountEditor : CollectionEditor
    {
        public UserAccountEditor(Type type) : base(type)
        {
        }
        protected override string GetDisplayText(object value)
        {
            return base.GetDisplayText(((UserAccount)value).Credentials.Username);
        }
        protected override object CreateInstance(Type itemType)
        {
            SelectRoleDialogBox roleSelecter = new SelectRoleDialogBox();
            if (roleSelecter.ShowDialog() == DialogResult.OK)
            {
                Random r = new Random();
                Role role = Role.GetRole(roleSelecter.SelectedRoleName);
                UserAccount account = new UserAccount(new UserCredentials($"{r.Next(0, 9999999)}", $"{r.Next(0, 9999999)}"), role.RoleName);
                return account;
            }
            else
            {
                return null;
            }
        }
    }
}