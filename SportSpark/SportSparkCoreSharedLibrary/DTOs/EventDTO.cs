﻿using SportSparkCoreSharedLibrary.DTOs.Base;

namespace SportSparkCoreSharedLibrary.DTOs
{
    public class EventDTO : BaseDTO
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal? Lat { get; set; }

        public decimal? Long { get; set; }

        public DateTime? Time { get; set; }

        public double? Duration { get; set; }

        public double? Price { get; set; }

        public int? NumberOfParticipants { get; set; }

        public int Privacy { get; set; }

        public bool Active { get; set; }

        public double[]? LatLong { get; set; }

        public List<int>? ValidUserIds { get; set; }

        #region Relations
        public int UserId { get; set; }        

        public int TypeId { get; set; }        

        public int RepeatTypeId { get; set; }

        public UserDTO User { get; set; }
        public EventTypeDTO EventType { get; set; }
        public EventRepeatTypeDTO RepeatType { get; set; }
        #endregion
    }
}
