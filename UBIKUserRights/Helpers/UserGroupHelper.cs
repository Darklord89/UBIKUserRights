using System;
using System.Collections.Generic;
using System.Linq;
using UBIK.Kernel;
using UBIK.Service.DTO.V240;

namespace UBIKUserRights.Helpers
{
    internal static class UserGroupHelper
    {
        public const string USERGROUP_NAME = "USERGROUP";
        public const string USERGROUP_FIELD_KEY = "ComosUID";

        private static Dictionary<string, UserGroup> userGroups = new Dictionary<string, UserGroup>();
        private static readonly object userGroupsLock = new object();

        internal static UserGroup GetUserGroup(string groupName)
        {
            if (userGroups.TryGetValue(groupName, out var userGroup))
            {
                lock (userGroupsLock)
                {
                    return userGroup;
                }
            }
            else
            {
                lock(userGroupsLock)
                {
                    LoadUserGroups();
                }
                return GetUserGroup(groupName);
            }
        }

        private static void LoadUserGroups()
        {
            MetaClass mc = UBIKUserRightsPlugin.UBIKEnvironment.UBIKDataFactory().MetaClasses().Where(w => w.Name == USERGROUP_NAME).First();

            foreach (BaseClass bc in mc.AllInstances(null))
            {
                if (bc.TryGetValue(USERGROUP_FIELD_KEY, out string key) && !userGroups.ContainsKey(key))
                {
                    try
                    {
                        userGroups.Add(key, bc as UserGroup);
                    }
                    catch (Exception ex)
                    {
                        UBIKKernel.LogException(256, bc, ex, false);
                    }
                }
            }
        }

        internal static GroupRight CreateGroupRight(UserGroup group, UserRights right)
        {
            if (group != null)
            {
                return new GroupRight() { GroupID = group.UID, Right = (int)right };
            }
            return null;
        }

        internal static GroupRight[] GetGroupsRightsForMany(UserGroup[] group, UserRights userRights)
        {
            return group.Select(x => CreateGroupRight(x, userRights)).ToArray();
        }

        internal static GroupRight[] GetGroupRightsForManyExcept(UserGroup[] group, UserRights userRights)
        {
            return userGroups.Values.Except(group).Select(x => CreateGroupRight(x, userRights)).ToArray();
        }
    }
}
