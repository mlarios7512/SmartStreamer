﻿using StreamBudget.Models.DTO.StreamAvail;
using StreamBudget.Models.Other;

namespace StreamBudget.ViewModels
{
    public class SeriesSearchVM
    {
        public string CurUserUsername { get; set; }
        public int WatchlistId { get; set; }

        public IEnumerable<WatchlistItemDTO> SearchResults { get; set; } = new List<WatchlistItemDTO>();
        public List<CompletionTime> CompletionTimes { get; set; } = new List<CompletionTime>();

        public SeriesSearchVM()
        {

        }
    }
}
