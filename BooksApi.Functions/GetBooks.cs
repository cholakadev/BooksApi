using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Azure.Documents.Client;
using System.Text.Json;
using Microsoft.Azure.Documents;
using System.Configuration;
using BooksApi.Functions.Models;

namespace BooksApi.Functions
{
    public static class GetBooks
    {
        [FunctionName("GetBooks")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            [CosmosDB("bookstore", "bookstore_container", Connection = "CosmosConnection")] IEnumerable<Book> documents,
            ILogger log)
        {
            log.LogInformation("Started getting books from the database.");

            if (documents == null)
            {
                log.LogInformation("No books has been found.");
                return new NotFoundObjectResult(documents);
            }

            log.LogInformation("End of getting books from the database.");

            return new OkObjectResult(documents);
        }
    }
}
