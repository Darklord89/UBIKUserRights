using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBIK.Data;
using UBIK.Kernel;

namespace UBIKUserRights.SystemDefinitons
{
    internal class UserRightsSystemDefinition_V450 : ISystemDefinitions
    {
        internal static readonly Version VERSION = new Version(4, 5, 0, 1);

        public Version Version => VERSION;

        public void DefineDefaultSystemStrings(UBIKEnvironment environment)
        {
            
        }

        public UBIKClassList<MetaClass> DefineSystemClassifications(UBIKEnvironment environment)
        {
            return new UBIKClassList<MetaClass>();
        }

        public UBIKClassList<MetaClass> DefineSystemMetaClasses(UBIKEnvironment environment)
        {
            return new UBIKClassList<MetaClass>();
        }

        public UBIKClassList<MetaProperty> DefineSystemMetaProperties(UBIKEnvironment environment)
        {
            return new UBIKClassList<MetaProperty>();
        }

        public UBIKClassList<SelectiveList> DefineSystemSelectiveLists(UBIKEnvironment environment)
        {
            return new UBIKClassList<SelectiveList>();
        }

        public void InitializeSystemContent(UBIKEnvironment environment)
        {
            
        }

        public string SystemRuntypeName(MetaClass metaClass)
        {
            return null;
        }

        public void UpdateSystemContent(UBIKEnvironment environment)
        {
            
        }
    }
}
