using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebHooks;
using Newtonsoft.Json.Linq;
using Triggr.UI.Models;
using Microsoft.Extensions.Primitives;
using System.Linq;
using Hangfire;

namespace Triggr.UI.Controllers
{
    public class GithubWebhookController : Controller
    {
        public GithubWebhookController()
        {
            
        }
        [HttpPost]
        public IActionResult HandlerForPush([FromBody]GithubPushModel model)
        {
            if (ModelState.IsValid)
            {
                if (Request.Headers.TryGetValue("X-GitHub-Event", out StringValues value))
                {
                    if (value.Count == 1)
                    {
                        var eventName = value[0];

                        if (eventName.Equals("push"))
                        {
                            var changedFiles = model.Commits
                                            .SelectMany(c => c.Modified)
                                            .Distinct()
                                            .ToList();
                                            
                            var owner = model.Repository.Owner.Name;
                            var repoName = model.Repository.Name;
                            BackgroundJob.Enqueue<TController>(i => i.Trigger(null, repoName, owner, changedFiles));
                            
                            return Ok();
                        }
                    }

                }
            }

            return BadRequest(ModelState);
        }
    }
}