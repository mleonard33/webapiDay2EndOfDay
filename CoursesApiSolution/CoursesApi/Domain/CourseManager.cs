using MongoDB.Bson;
using MongoDB.Driver;

namespace CoursesApi.Domain;

public class CourseManager
{

    private readonly MongoDbCoursesAdapter _adapter;

    public CourseManager(MongoDbCoursesAdapter adapter)
    {
        _adapter = adapter;
    }

    public async Task<CoursesResponse> GetAllCoursesAsync()
    {
        // Do not check this in. 
        var coursesFromDatabase = await _adapter.Courses.Find(t => true).ToListAsync();

        var response = new CoursesResponse
        {
            Data = coursesFromDatabase.Select(c => new CourseSummaryItemResponse
            {
                Id = c.Id.ToString(),
                Title = c.Title
               
            }).ToList(),
        };

        return response;
    }

    public async Task<CourseDetailsResponse> AddCourseAsync(CourseCreateRequest request)
    {
        // From A Model -> CourseEntity
        var courseToAdd = new CourseEntity
        {
            Title = request.Title,
            NumberOfHours = request.NumberOfHours,
            DeliveryLocation = request.DeliveryLocation,
            IsRemoved = false,
            WhenCreated = DateTime.Now
        };

        await _adapter.Courses.InsertOneAsync(courseToAdd);

        var response = new CourseDetailsResponse
        {
            Id = courseToAdd.Id.ToString(),
            Title = courseToAdd.Title,
            DeliveryLocation = courseToAdd.DeliveryLocation,
            NumberOfHours = courseToAdd.NumberOfHours
        };

        return response;

    }

    public async Task<CourseDetailsResponse?> GetCourseByIdAsync(string courseId)
    {

        if (ObjectId.TryParse(courseId, out var id))
        {
            var savedCourse = await _adapter.Courses.Find(c => c.Id == id).SingleOrDefaultAsync();
            if (savedCourse != null)
            {
                var response = new CourseDetailsResponse
                {
                    Id = savedCourse.Id.ToString(),
                    Title = savedCourse.Title,
                    DeliveryLocation = savedCourse.DeliveryLocation,
                    NumberOfHours = savedCourse.NumberOfHours
                };
                return response;
            }
        }
        return null;
    }
}
