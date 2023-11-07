using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace MongoDB
{
    internal class Helpers
    {
        

            public static object ListDBs(MongoClient client)
            {

         
                var dbList = client.ListDatabases().ToList();
                return dbList;

            }

            public static object SimpleConnect(string connectstring)
            {
                MongoClient dbClient = new MongoClient(connectstring);
                return dbClient;

            }

            //TODO: GetAllCollecitonNames

            //db.getCollectionNames()

            public static object GetCollection(MongoClient dbClient, string database, string collection)
            {
                
                var db = dbClient.GetDatabase(database);
                var col = db.GetCollection<BsonDocument>(collection);
                //Console.WriteLine(col);
                return col;

            }

            public static async Task InsertJson(MongoClient client, string json, string database_name, string collection_name)
            {
                var database = client.GetDatabase(database_name);
                var document = BsonSerializer.Deserialize<BsonDocument>(json);
                var collection = database.GetCollection<BsonDocument>(collection_name);
                await collection.InsertOneAsync(document);
                Console.WriteLine("Added Log to MongoDB");
            }

            public static string constructLog(string scriptName)
            {

                //Create my object
                var logData = new
                {
                    ScriptName = scriptName,
                    UserName = Environment.UserName,
                };

                //Tranform it to Json object
                string jsonData = JsonConvert.SerializeObject(logData);

                return jsonData;
            }


        }

    }



