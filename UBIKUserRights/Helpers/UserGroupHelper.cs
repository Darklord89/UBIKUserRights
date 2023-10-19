using System;
using System.Collections.Generic;
using System.Linq;
using UBIK.Kernel;
using UBIK.Service.DTO.V240;

namespace UBIKUserRights.Helpers
{
    internal static class UserGroupHelper
    {
        private static Dictionary<string, UserGroup> userGroups = new Dictionary<string, UserGroup>();
        private static readonly object userGroupsLock = new object();

        internal static UserGroup GetUserGroup(string groupName, int coutner = 0)
        {
            lock (userGroupsLock)
            {
                if (userGroups.Count > 0)
                {
                    if (userGroups.TryGetValue(groupName, out var userGroup))
                    {
                        UBIKKernel.LogDebugOutput(System.Reflection.MethodBase.GetCurrentMethod(),
                            7,
                            $"Returning group for groupname {groupName} -> {userGroup.Name} {userGroup.Description} ({userGroup.ID}).",
                            null);
                        return userGroup;
                    }
                }
                else
                {
                    LoadUserGroups();
                    return coutner > 1 ? null : GetUserGroup(groupName, ++coutner);
                }
            }
            return null;
        }

        private static void LoadUserGroups()
        {
            DateTime dateTime = DateTime.Now;
            UBIKKernel.LogDebugOutput(System.Reflection.MethodBase.GetCurrentMethod(),
                8,
                "Loading usergroups to the catche...",
                null);

            MetaClass mc = UBIKUserRightsPlugin.UBIKEnvironment.UBIKDataFactory().MetaClasses(Settings.USERGROUP_NAME).First();

            foreach (BaseClass bc in mc.AllInstances(null))
            {
                if (bc.TryGetValue(Settings.USERGROUP_FIELD_KEY, out string key) && !userGroups.ContainsKey(key))
                {
                    try
                    {
                        userGroups.Add(key, bc as UserGroup);
                        UBIKKernel.LogDebugOutput(System.Reflection.MethodBase.GetCurrentMethod(),
                            9,
                            $"UserGroup {bc.Name} {bc.Description} ({bc.ID}) was added to the collection catche.",
                            null);
                    }
                    catch (Exception ex)
                    {
                        UBIKKernel.LogException(256, bc, ex, false);
                    }
                }
            }

            UBIKKernel.LogDebugOutput(System.Reflection.MethodBase.GetCurrentMethod(),
                9,
                $"Usergroups were loaded in {DateTime.Now - dateTime}...",
                null);
        }

        internal static GroupRight CreateGroupRight(UserGroup group, UserRights right)
        {
            if (group != null)
            {
                return new GroupRight() { GroupID = group.UID, Right = (int)right };
            }
            return null;
        }

        internal static GroupRight[] GetGroupsRightsForAll(UserGroup[] group, UserRights userRights)
        {
            return group.Select(x => CreateGroupRight(x, userRights)).ToArray();
        }

        internal static GroupRight[] GetGroupRightsForAllExcept(UserGroup[] group, UserRights userRights)
        {
            lock (userGroupsLock)
            {
                return userGroups.Values.Except(group).Select(x => CreateGroupRight(x, userRights)).ToArray();
            }
        }

        internal static UserGroup[] GetAllGroups(UserGroup[] exceptGroups = null)
        {
            lock (userGroupsLock)
            {
                return exceptGroups == null ? userGroups.Values.ToArray() : userGroups.Values.Except(exceptGroups).ToArray();
            }
        }
    }
}
