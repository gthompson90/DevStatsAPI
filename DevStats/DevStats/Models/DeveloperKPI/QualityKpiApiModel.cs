﻿using System;
using System.Collections.Generic;
using System.Linq;
using DevStats.Domain.DeveloperKpi;

namespace DevStats.Models.DeveloperKPI
{
    public class QualityKpiApiModel
    {
        public List<QualityKpiApiModelItem> KpiData { get; set; }

        public QualityKpiApiModel()
        {
        }

        public QualityKpiApiModel(Dictionary<string, DeveloperQualityKPI> kpis)
        {
            KpiData = new List<QualityKpiApiModelItem>();

            foreach (var kpi in kpis)
            {
                var stories = kpi.Value.Stories;
                var developer = kpi.Key;
                KpiData.AddRange(stories.Select(x => new QualityKpiApiModelItem(developer, x)));
            }
        }

        public QualityKpiApiModel(string developer, DeveloperQualityKPI kpi)
        {
            KpiData = new List<QualityKpiApiModelItem>();

            KpiData.AddRange(kpi.Stories.Select(x => new QualityKpiApiModelItem(developer, x)));
        }
    }

    public class QualityKpiApiModelItem
    {
        public string Developer { get; set; }

        public string Key { get; set; }

        public string Product { get; set; }

        public string Story { get; set; }

        public string Release { get; set; }

        public DateTime? Delivered { get; set; }

        public string DeliveredDescription { get; set; }

        public int EffortForUserStory { get; set; }

        public int PlannedDevelopment { get; set; }

        public int PlannedDevelopmentByDeveloper { get; set; }

        public decimal DevelopmentProportion { get; set; }

        public int ReworkEffort { get; set; }

        public decimal ReworkProportion { get; set; }

        public bool OnTrack { get; set; }

        public QualityKpiApiModelItem()
        {
        }

        public QualityKpiApiModelItem(string developer, StoryBreakdown storyBreakdown)
        {
            Developer = developer;
            Product = storyBreakdown.Product;
            Key = storyBreakdown.Key;
            Story = storyBreakdown.Description;
            Release = storyBreakdown.Release;
            Delivered = storyBreakdown.LastWorkedOn;
            DeliveredDescription = storyBreakdown.LastWorkedOn.HasValue ? string.Format("{0:yyyy MMMM}", storyBreakdown.LastWorkedOn) : string.Empty;
            EffortForUserStory = storyBreakdown.TotalDuration;
            PlannedDevelopment = storyBreakdown.TotalPlannedDevelopment;
            PlannedDevelopmentByDeveloper = storyBreakdown.TotalPlannedDevelopmentByDeveloper;
            DevelopmentProportion = storyBreakdown.DeveloperProportion;
            ReworkEffort = storyBreakdown.ReworkDuration;
            ReworkProportion = storyBreakdown.ReworkProportion;
            OnTrack = storyBreakdown.IsOnTrack;
        }
    }
}