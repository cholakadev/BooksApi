using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksApi.Functions.Models;

namespace BooksApi.Functions
{
    public static class GetBookById
    {
        [FunctionName("GetBookById")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            [CosmosDB("bookstore", "bookstore_container", Connection = "CosmosConnection", Id = "{Query.Id}")] Book document,
            ILogger log)
        {
            log.LogInformation("Started getting book by id from the database.");

            var bookId = req.Query["Id"];

            if (document == null)
            {
                log.LogInformation($"Books with id {bookId} is not found.");
                return new NotFoundObjectResult(document);
            }

            log.LogInformation($"End of getting book with id {bookId} from the database.");

            return new OkObjectResult(document);
        }
    }
}
