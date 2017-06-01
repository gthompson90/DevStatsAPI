﻿using System;
using System.Collections.Generic;

namespace DevStats.Domain.Jira
{
    public interface IJiraService
    {
        void CreateSubTasks(string issueId, string displayIssueId, string content);

        void ProcessSubTaskUpdate(string issueId, string displayIssueId, string content);

        IEnumerable<JiraAudit> GetJiraAudit(DateTime from, DateTime to);
    }
}