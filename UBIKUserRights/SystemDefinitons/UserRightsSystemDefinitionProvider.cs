using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBIK.Data;

namespace UBIKUserRights.SystemDefinitons
{
    internal class UserRightsSystemDefinitionProvider : SystemDefinitionProviderBase
    {
        public override Guid ModuleID => UBIKUserRightsPlugin.ID;

        private static UserRightsSystemDefinitionProvider instance;

        public static UserRightsSystemDefinitionProvider Instance => instance ?? (instance = new UserRightsSystemDefinitionProvider());

        private UserRightsSystemDefinitionProvider()
            : base(new ISystemDefinitions[]
            {
                new UserRightsSystemDefinition_V450()
            })
        { }
    }
}
