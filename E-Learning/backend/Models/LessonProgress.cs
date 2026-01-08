namespace backend.Models;

public class LessonProgress
{
    public int Id { get; set; }
    public int EnrollmentId { get; set; }
    public int LessonId { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }
    public double ProgressPercentage { get; set; }
    public int TimeSpentMinutes { get; set; }

    public Enrollment? Enrollment { get; set; }
    public Lesson? Lesson { get; set; }
}
