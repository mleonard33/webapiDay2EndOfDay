using MongoDB.Bson;

namespace CoursesApi.Domain;

public class CourseEntity
{
    public ObjectId Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal NumberOfHours { get; set; }
    public DeliveryLocationTypes DeliveryLocation { get; set; }

    public DateTime WhenCreated { get; set; } = DateTime.Now;

    public bool IsRemoved { get; set; } = false;
}
