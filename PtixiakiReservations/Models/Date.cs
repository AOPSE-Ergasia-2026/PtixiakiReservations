using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PtixiakiReservations.Models;

public class Date
{
    public int Id { get; set; }
    public int EventId { get; set; }
    [ForeignKey("EventId")] public Event Event { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly EndTime { get; set; }
    public bool Active { get; set; }

}


