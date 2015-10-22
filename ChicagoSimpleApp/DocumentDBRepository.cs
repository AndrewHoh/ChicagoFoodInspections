using System;
using System.Web;
using System.Linq;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System.Configuration;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ChicagoSimpleApp
{
    public static class DocumentDBRepository<T>
    {
        #region necessary stuff
        //Use the Database if it exists, if not create a new Database
        private static Database ReadOrCreateDatabase()
        {
            var db = Client.CreateDatabaseQuery()
                            .Where(d => d.Id == DatabaseId)
                            .AsEnumerable()
                            .FirstOrDefault();

            if (db == null)
            {
                db = Client.CreateDatabaseAsync(new Database { Id = DatabaseId }).Result;
            }

            return db;
        }

        //Use the DocumentCollection if it exists, if not create a new Collection
        private static DocumentCollection ReadOrCreateCollection(string databaseLink)
        {
            var col = Client.CreateDocumentCollectionQuery(databaseLink)
                              .Where(c => c.Id == CollectionId)
                              .AsEnumerable()
                              .FirstOrDefault();

            if (col == null)
            {
                var collectionSpec = new DocumentCollection { Id = CollectionId };
                var requestOptions = new RequestOptions { OfferType = "S1" };

                col = Client.CreateDocumentCollectionAsync(databaseLink, collectionSpec, requestOptions).Result;
            }

            return col;
        }

        //Use the DocumentCollection if it exists, if not create a new Collection
        private static DocumentCollection ReadOrCreateCollectionFeed(string databaseLink)
        {
            var col = Client.CreateDocumentCollectionQuery(databaseLink)
                              .Where(c => c.Id == CollectionIdFeed)
                              .AsEnumerable()
                              .FirstOrDefault();

            if (col == null)
            {
                var collectionSpec = new DocumentCollection { Id = CollectionId };
                var requestOptions = new RequestOptions { OfferType = "S1" };

                col = Client.CreateDocumentCollectionAsync(databaseLink, collectionSpec, requestOptions).Result;
            }

            return col;
        }

        //Expose the "database" value from configuration as a property for internal use
        private static string databaseId;
        private static String DatabaseId
        {
            get
            {
                if (string.IsNullOrEmpty(databaseId))
                {
                    databaseId = ConfigurationManager.AppSettings["database"];
                }

                return databaseId;
            }
        }

        //Expose the "collection" value from configuration as a property for internal use
        private static string collectionId;
        private static String CollectionId
        {
            get
            {
                if (string.IsNullOrEmpty(collectionId))
                {
                    collectionId = ConfigurationManager.AppSettings["collection"];
                }

                return collectionId;
            }
        }

        //Expose the "collection2" value from configuration as a property for internal use
        private static string collectionIdFeed;
        private static String CollectionIdFeed
        {
            get
            {
                if (string.IsNullOrEmpty(collectionIdFeed))
                {
                    collectionIdFeed = ConfigurationManager.AppSettings["collection2"];
                }

                return collectionIdFeed;
            }
        }

        //Use the ReadOrCreateDatabase function to get a reference to the database.
        private static Database database;
        private static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = ReadOrCreateDatabase();
                }

                return database;
            }
        }

        //Use the ReadOrCreateCollection function to get a reference to the collection.
        private static DocumentCollection collection;
        private static DocumentCollection Collection
        {
            get
            {
                if (collection == null)
                {
                    collection = ReadOrCreateCollection(Database.SelfLink);
                }

                return collection;
            }
        }

        //Use the ReadOrCreateCollectionFeed function to get a reference to the collection.
        private static DocumentCollection collectionFeed;
        private static DocumentCollection CollectionFeed
        {
            get
            {
                if (collection == null)
                {
                    collection = ReadOrCreateCollectionFeed(Database.SelfLink);
                }

                return collection;
            }
        }

        //This property establishes a new connection to DocumentDB the first time it is used, 
        //and then reuses this instance for the duration of the application avoiding the
        //overhead of instantiating a new instance of DocumentClient with each request
        private static DocumentClient client;
        private static DocumentClient Client
        {
            get
            {
                if (client == null)
                {
                    string endpoint = ConfigurationManager.AppSettings["endpoint"];
                    string authKey = ConfigurationManager.AppSettings["authKey"];
                    Uri endpointUri = new Uri(endpoint);
                    client = new DocumentClient(endpointUri, authKey);
                }

                return client;
            }
        }
        #endregion

        public static T GetUser(string username)
        {
            var query = Client.CreateDocumentQuery<T>(Collection.DocumentsLink, "SELECT * FROM root r WHERE r.UserName = '" + username + "'", new FeedOptions { MaxItemCount = 1 }).AsDocumentQuery<T>();
            var results = query.ExecuteNextAsync<T>().Result;
            return results.First();
        }

        public static IEnumerable<T> GetComments(string username)
        {
            var query = Client.CreateDocumentQuery<T>(Collection.DocumentsLink, "SELECT * FROM root r WHERE r.User = '" + username + "'", new FeedOptions { MaxItemCount = 10 }).AsDocumentQuery<T>();
            List<T> results = new List<T>();
            results.AddRange(query.ExecuteNextAsync<T>().Result);
            return results;
        }

        public static IEnumerable<T> Get10Items(string name)
        {
            var query = Client.CreateDocumentQuery<T>(Collection.DocumentsLink, "SELECT * FROM root r where r.AKA_Name = \"" + name + "\" OR r.DBA_Name = \"" + name + "\"", new FeedOptions { MaxItemCount = 10 }).AsDocumentQuery<T>();
            List<T> results = new List<T>();
            while (query.HasMoreResults && results.Count < 10)
            {
                results.AddRange(query.ExecuteNextAsync<T>().Result);
            }
            return results;
        }

        public static IEnumerable<T> Get10Items(string name, string street)
        {
            var query = Client.CreateDocumentQuery<T>(Collection.DocumentsLink, "SELECT * FROM root r where (r.AKA_Name = \"" + name + "\" OR r.DBA_Name = \"" + name + "\") AND r.Address.Street = \"" + street + "\"", new FeedOptions { MaxItemCount = 10 }).AsDocumentQuery<T>();
            List<T> results = new List<T>();
            while (query.HasMoreResults && results.Count < 10)
            {
                results.AddRange(query.ExecuteNextAsync<T>().Result);
            }
            return results;
        }

        public static T GetLetter(string letter)
        {
            var query = Client.CreateDocumentQuery<T>(Collection.DocumentsLink, "SELECT * FROM root r where r.Letter = \"" + letter.Trim() + "\"", new FeedOptions { MaxItemCount = 1 }).AsDocumentQuery<T>();
            List<T> results = new List<T>();
            while (query.HasMoreResults && results.Count < 1)
            {
                results.AddRange(query.ExecuteNextAsync<T>().Result);
            }
            return results.First();
        }

        public static async Task<Document> AddUserFavoriteAsync(string id, T user)
        {
            Document doc = GetDocument(id);
            return await Client.ReplaceDocumentAsync(doc.SelfLink, user);
        }

        public static async Task<Document> CreateItemAsync(T item)
        {
            return await Client.CreateDocumentAsync(Collection.SelfLink, item);
        }

        public static async Task<Document> UpdateCommentAsync(string id, string comment)
        {
            Document doc = GetDocument(id);
            doc.SetPropertyValue("Comment", comment);
            return await Client.ReplaceDocumentAsync(doc.SelfLink, doc);
        }

        private static Document GetDocument(string id)
        {
            return Client.CreateDocumentQuery(Collection.DocumentsLink)
                .Where(d => d.Id == id)
                .AsEnumerable()
                .FirstOrDefault();
        }

        public static T GetHeatMap()
        {
            var query = Client.CreateDocumentQuery<T>(Collection.DocumentsLink, "SELECT * FROM root r where r.Title = \"Heat Map\"", new FeedOptions { MaxItemCount = 1 }).AsDocumentQuery<T>();
            List<T> results = new List<T>();
            while (query.HasMoreResults && results.Count < 1)
            {
                results.AddRange(query.ExecuteNextAsync<T>().Result);
            }
            return results.First();
        }

        public static IEnumerable<T> Get10Feed(int timestamp)
        {
            var query = Client.CreateDocumentQuery<T>(CollectionFeed.DocumentsLink, "SELECT * FROM root r where r._ts > " + timestamp, new FeedOptions { MaxItemCount = 10 }).AsDocumentQuery<T>();
            List<T> results = new List<T>();
            while (query.HasMoreResults && results.Count < 5)
            {
                results.AddRange(query.ExecuteNextAsync<T>().Result);
            }
            return results;
        }
    }
}
