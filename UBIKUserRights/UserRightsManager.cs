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
                        users.Add(group);
                        rights.Add(UserGroupHelper.CreateGroupRight(group, UserRights.Write));
                    }
                    foreach (string readRight in jsonRights.READ)
                    {
                        UserGroup group = UserGroupHelper.GetUserGroup(readRight);
                        users.Add(group);
                        rights.Add(UserGroupHelper.CreateGroupRight(group, UserRights.Read));
                    }

                    foreach (GroupRight group in UserGroupHelper.GetGroupRightsForManyExcept(users.ToArray(), UserRights.NoRight))
                    {
                        rights.Add(group);
                    }
                }
            }
            
            return rights;
        }
    }
}
