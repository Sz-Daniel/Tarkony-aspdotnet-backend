namespace Status
{
    namespace External
    {
        using System;
        using System.Collections.Generic;

        public class StatusQuery
        {
            public StatusData Status { get; set; }
        }

        public class StatusData
        {
            public List<CurrentStatus?>? CurrentStatuses { get; set; }
            public GeneralStatus? GeneralStatus { get; set; }
            public List<Message?>? Messages { get; set; }
        }

        public class CurrentStatus
        {
            public string? Message { get; set; }
            public string Name { get; set; }
            public int Status { get; set; }
            public string StatusCode { get; set; }
        }

        public class GeneralStatus
        {
            public string? Message { get; set; }
            public string Name { get; set; }
            public int Status { get; set; }
            public string StatusCode { get; set; }
        }

        public class Message
        {
            public string Content { get; set; }
            public string? SolveTime { get; set; }
            public string StatusCode { get; set; }
            public string Time { get; set; }
            public int Type { get; set; }
        }
    }
}
