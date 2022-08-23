using CoursesApi.Domain;

namespace CoursesApi.Controllers;

[ApiController]
public class CoursesController : ControllerBase
{

    private readonly CourseManager _courseManager;
   

    public CoursesController(CourseManager courseManager)
    {
        _courseManager = courseManager;
    }

    [HttpPost("/courses")]
    public async Task<ActionResult<CourseDetailsResponse>> AddCourseAsync(
        [FromBody] CourseCreateRequest request)
    {
        // 1 Validate it - if not good, return a 400.
        // 2. If it's good, do the side effect (save it, whatever)
        CourseDetailsResponse response = await _courseManager.AddCourseAsync(request);
        // 3. Return:
        //    - 201 Created Status Code
        //    - Add a header called "Location" which says where that new resource lives. (uri)
        //    - Include a copy of what they would get if they went to URL
        //    - Location: http://localhost:9090/courses/938938938

        return CreatedAtRoute("courses#getcoursebyid", new { courseId = response.Id }, response);
    }


    [HttpGet("/courses")]
    public async Task<ActionResult<CoursesResponse>> GetAllCoursesAsync()
    {
        // WTCYWYH

        CoursesResponse response = await _courseManager.GetAllCoursesAsync();

        return Ok(response);
        
    }

    [HttpGet("/courses/{courseId}", Name = "courses#getcoursebyid")]
    public async Task<ActionResult<CourseDetailsResponse>> GetCourseByIdAsync(string courseId)
    {
        CourseDetailsResponse? response = await _courseManager.GetCourseByIdAsync(courseId);

        if(response is null)
        {
            return NotFound();
        } else
        {
            return Ok(response);
        }
    }
}
