using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Azure.Cosmos;
using System;
using BooksApi.Functions.Models;

namespace BooksApi.Functions
{
    public static class AddBook
    {
        [FunctionName("AddBooks")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [CosmosDB("bookstore", "bookstore_container", Connection = "CosmosConnection")] CosmosClient client,
            ILogger log)
        {
            log.LogInformation("Started inserting a book in the database.");

            var book = JsonSerializer.Deserialize<Book>(req.Body);
            book.Id = Guid.NewGuid().ToString();

            var container = client.GetContainer("bookstore", "bookstore_container");

            var created = await container.CreateItemAsync<Book>(book);

            log.LogInformation("End of inserting a book in the database.");

            return new OkObjectResult(created != null);
        }
    }
}
