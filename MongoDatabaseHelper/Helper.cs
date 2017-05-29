using MongoDB.Bson.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDatabaseHelper
{
    public static partial class Helper
    {
        public static List<Type> RegisterClassTypes = new List<Type>();
        public static string ConnectionString;
        public static string Database;
        public static List<object> RegisteredServices = new List<object>();

        public static void Register(string connectionString, string database)
        {
            ConnectionString = connectionString;
            Database = database;
        }

        public static List<Type> GetRegisteredClassTypesNoId()
        {
            return RegisterClassTypes.Where(o => !o.GetType().IsSubclassOf(typeof(BObjectWithId))).ToList<Type>();
        }

        public static List<Type> GetRegisteredClassTypesId()
        {
            return RegisterClassTypes.Where(o => o.GetType().IsSubclassOf(typeof(BObjectWithId))).ToList<Type>();
        }

        public static List<Type> GetRegisteredClassTypes()
        {
            return RegisterClassTypes;
        }

        public static BsonClassMap<TClass> RegisterClassMap<TClass>(Action<BsonClassMap<TClass>> classMapInitializer) where TClass : BObject
        {
            var x = BsonClassMap.RegisterClassMap(classMapInitializer);
            RegisterClassType<TClass>();
            return x;
        }

        public static BsonClassMap<TClass> RegisterClassMap<TClass>() where TClass : BObject
        {
            var x = BsonClassMap.RegisterClassMap<TClass>();
            RegisterClassType<TClass>();
            return x;
        }

        public static void RegisterClassType<TClass>(bool MapClass = false) where TClass : BObject
        {
            if (MapClass)
            {
                BsonClassMap.RegisterClassMap<TClass>();
            }
            RegisterClassTypes.Add(typeof(TClass));
        }

        public static void RegisterClassService<TClass>(bool MapClass = false) where TClass : BObjectWithId
        {
            if (MapClass)
            {
                BsonClassMap.RegisterClassMap<TClass>();
            }
            RegisteredServices.Add(new DataService<TClass>(ConnectionString, Database));
        }
    }

    public static partial class Helper<HelperT> where HelperT : BObjectWithId
    {       
        public static DataService<HelperT> Services
        {
            get {
                foreach (var service in Helper.RegisteredServices)
                {
                    if (service is DataService<HelperT>)
                    {
                        return service as DataService<HelperT>;
                    }
                }
                return null;
            }
        }        
    }
}
