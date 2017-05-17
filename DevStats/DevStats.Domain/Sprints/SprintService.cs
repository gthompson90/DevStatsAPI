﻿using System;
using System.Collections.Generic;

namespace DevStats.Domain.Sprints
{
    public class SprintService : ISprintService
    {
        private readonly ISprintRepository repository;

        public SprintService(ISprintRepository repository)
        {
            if (repository == null) throw new ArgumentNullException(nameof(repository));

            this.repository = repository;
        }

        public IEnumerable<Sprint> Get()
        {
            return repository.Get();
        }

        public Sprint GetSprint(string pod)
        {
            if (string.IsNullOrWhiteSpace(pod)) return null;

            return repository.GetSprint(pod);
        }

        public Sprint GetSprint(string pod, string sprint)
        {
            if (string.IsNullOrWhiteSpace(pod)) return null;

            if (string.IsNullOrWhiteSpace(sprint)) return GetSprint(pod);

            return repository.GetSprint(pod, sprint);
        }

        public void Save(Sprint sprint)
        {
            if (sprint == null || string.IsNullOrWhiteSpace(sprint.Pod) || string.IsNullOrWhiteSpace(sprint.Name))
                return;

            repository.Save(sprint);
        }
    }
}