using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Amazon.Lambda.Core;
using System.Dynamic;
using System.Net.Http;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Newtonsoft.Json.Converters;



// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace amiibo
{
    public class Function
    {
        public static readonly HttpClient client = new HttpClient();

        //database setup
     //   private static AmazonDynamoDBClient client2 = new AmazonDynamoDBClient();
     //   private static string tablename = "AmiiboInfo";


            public async Task<ExpandoObject> FunctionHandler(APIGatewayProxyRequest input, ILambdaContext context)
        {
            //everthing done to get info and display
            string newYorkApi = $"https://www.amiiboapi.com/api/amiibo/?name=mario";
            //    string newYorkApi = $"https://www.amiiboapi.com/api/amiibo/?name={input}";
            string response = await client.GetStringAsync(newYorkApi);
            ExpandoObject config = JsonConvert.DeserializeObject<ExpandoObject>(response);

            string strCust = JsonConvert.SerializeObject(config, new ExpandoObjectConverter());


            return config;
        }
    }
}

