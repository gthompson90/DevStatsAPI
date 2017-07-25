﻿using System;
using System.Web.Http;
using System.Web.Http.Cors;
using DevStats.Domain.Jira;

namespace DevStats.Controllers.API
{
    public class JiraStoryUpdatedController : ApiController
    {
        private readonly IJiraService service;

        public JiraStoryUpdatedController(IJiraService service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));

            this.service = service;
        }

        [EnableCors("*", "*", "*")]
        [HttpPost]
        public void StoryUpdated([FromUri]string issueId, [FromUri]string displayIssueId)
        {
            var jsonContent = Request.Content.ReadAsStringAsync().Result;

            service.ProcessStoryUpdate(issueId, displayIssueId, jsonContent);
        }
    }
}