using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UBIK.Kernel;
using UBIK.Service.DTO.V240;
using UBIKUserRights.Helpers;
using UBIKUserRights.Model;

namespace UBIKUserRights
{
    public static class UserRightsManager
    {
        public static IEnumerable<GroupRight> GetRightsFromJson(string json)
        {
            List<GroupRight> rights = null;

            if (json != null)
            {
                JsonRights jsonRights = null;
                try
                {
                    jsonRights = JsonConvert.DeserializeObject<JsonRights>(json);
                }
                catch (Exception ex)
                {
                    UBIKKernel.LogException(Settings.JSON_CONVERT_ERROR_ID, null, ex, false);
                }

                if (jsonRights != null)
                {
                    rights = new List<GroupRight>();
                    List<UserGroup> users = new List<UserGroup>();
                    foreach (string writeRight in jsonRights.WRITE)
                    {
                        UserGroup group = UserGroupHelper.GetUserGroup(writeRight);
                        if (group != null)
                        {
                            UBIKKernel.LogDebugOutput(System.Reflection.MethodBase.GetCurrentMethod(),
                                4,
                                $"Adding WRITE right for user group {group.Name} {group.Description} ({group.ID}).",
                                null);
                            users.Add(group);
                            rights.Add(UserGroupHelper.CreateGroupRight(group, UserRights.Write));
                        }
                    }

                    if (jsonRights.READ.Contains("ALL"))
                    {
                        foreach (UserGroup group in UserGroupHelper.GetAllGroups(users.ToArray()))
                        {
                            UBIKKernel.LogDebugOutput(System.Reflection.MethodBase.GetCurrentMethod(),
                                5,
                                $"Adding READ (ALL) right for user group {group.Name} {group.Description} ({group.ID}).",
                                null);
                            users.Add(group);
                            rights.Add(UserGroupHelper.CreateGroupRight(group, UserRights.Read));
                        }
                    }
                    else
                    {
                        foreach (string readRight in jsonRights.READ)
                        {
                            UserGroup group = UserGroupHelper.GetUserGroup(readRight);
                            if (group != null)
                            {
                                UBIKKernel.LogDebugOutput(System.Reflection.MethodBase.GetCurrentMethod(),
                                    6,
                                    $"Adding READ right for user group {group.Name} {group.Description} ({group.ID}).",
                                    null);
                                users.Add(group);
                                rights.Add(UserGroupHelper.CreateGroupRight(group, UserRights.Read));
                            }
                        }
                    }

                    foreach (GroupRight group in UserGroupHelper.GetGroupRightsForAllExcept(users.ToArray(), UserRights.NoRight))
                    {
                        UBIKKernel.LogDebugOutput(System.Reflection.MethodBase.GetCurrentMethod(),
                            6,
                            $"Adding NORIGHT right for user group {group.GroupID}.",
                            null);
                        rights.Add(group);
                    }
                }
            }

            UBIKKernel.LogDebugOutput(System.Reflection.MethodBase.GetCurrentMethod(),
                3,
                "Empty json string, returning null.",
                null);
            
            return rights;
        }
    }
}
