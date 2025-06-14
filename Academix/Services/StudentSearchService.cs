using Academix.Models;

public class StudentSearchService
{
    public List<StudentDisplayModel> AllStudents { get; set; } = new List<StudentDisplayModel>
{
        new StudentDisplayModel(
            new Student("HS001", "Nguyễn Văn A", true, new DateTime(2007, 5, 10), "123 Đường A", "a@example.com", null),
            "10/1", 8.2, 8.5, "Đang học", 1, false),

        new StudentDisplayModel(
            new Student("HS002", "Trần Thị B", false, new DateTime(2007, 8, 15), "456 Đường B", "b@example.com", null),
            "10/1", 7.5, 7.8, "Đang học", 2, false),

        new StudentDisplayModel(
            new Student("HS003", "Lê Văn C", true, new DateTime(2007, 2, 20), "789 Đường C", "c@example.com", null),
            "10/2", 6.0, 6.5, "Nghỉ học", 3, false),

        new StudentDisplayModel(
            new Student("HS004", "Phạm Thị D", false, new DateTime(2006, 11, 5), "101 Đường D", "d@example.com", null),
            "11/1", 9.1, 9.4, "Đang học", 4, false),

        new StudentDisplayModel(
            new Student("HS005", "Đặng Văn E", true, new DateTime(2006, 7, 25), "202 Đường E", "e@example.com", null),
            "11/2", 5.0, 4.8, "Nghỉ học", 5, false),

        new StudentDisplayModel(
            new Student("HS006", "Hoàng Thị F", false, new DateTime(2006, 3, 12), "303 Đường F", "f@example.com", null),
            "11/3", 6.2, 6.9, "Đang học", 6, false),

        new StudentDisplayModel(
            new Student("HS007", "Mai Văn G", true, new DateTime(2005, 10, 30), "404 Đường G", "g@example.com", null),
            "12/1", 8.0, 8.3, "Đang học", 7, false),

        new StudentDisplayModel(
            new Student("HS008", "Ngô Thị H", false, new DateTime(2005, 4, 18), "505 Đường H", "h@example.com", null),
            "12/1", 9.0, 9.5, "Đang học", 8, false),

        new StudentDisplayModel(
            new Student("HS009", "Bùi Văn I", true, new DateTime(2005, 6, 8), "606 Đường I", "i@example.com", null),
            "12/2", 7.2, 7.1, "Nghỉ học", 9, false),

        new StudentDisplayModel(
            new Student("HS010", "Đỗ Thị K", false, new DateTime(2005, 9, 22), "707 Đường K", "k@example.com", null),
            "12/3", 8.5, 8.6, "Đang học", 10, false),

        new StudentDisplayModel(
            new Student("HS011", "Trịnh Văn L", true, new DateTime(2007, 1, 14), "808 Đường L", "l@example.com", null),
            "10/1", 5.5, 5.8, "Đang học", 11, false),

        new StudentDisplayModel(
            new Student("HS012", "Phan Thị M", false, new DateTime(2006, 2, 27), "909 Đường M", "m@example.com", null),
            "11/1", 7.8, 7.7, "Đang học", 12, false),

        new StudentDisplayModel(
            new Student("HS013", "Vũ Văn N", true, new DateTime(2006, 5, 3), "111 Đường N", "n@example.com", null),
            "11/2", 6.3, 6.2, "Nghỉ học", 13, false),

        new StudentDisplayModel(
            new Student("HS014", "Lý Thị O", false, new DateTime(2007, 12, 19), "222 Đường O", "o@example.com", null),
            "10/2", 9.0, 8.9, "Đang học", 14, false),

        new StudentDisplayModel(
            new Student("HS015", "Nguyễn Văn P", true, new DateTime(2005, 8, 7), "333 Đường P", "p@example.com", null),
            "12/3", 7.0, 7.4, "Đang học", 15, false)

};



    public List<StudentDisplayModel> SearchStudents(SearchCriteria criteria)
    {
        var query = AllStudents.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(criteria.Grade))
            query = query.Where(s => s.ClassName.StartsWith(criteria.Grade));

        if (!string.IsNullOrWhiteSpace(criteria.ClassName))
            query = query.Where(s => s.ClassName == criteria.ClassName);

        if (!string.IsNullOrWhiteSpace(criteria.Gender))
            query = query.Where(s => s.Gender == criteria.Gender);

        if (!string.IsNullOrWhiteSpace(criteria.Status))
            query = query.Where(s => s.Status == criteria.Status);

        if (!string.IsNullOrWhiteSpace(criteria.FullName))
            query = query.Where(s => s.Name.Contains(criteria.FullName, StringComparison.OrdinalIgnoreCase));

        if (!string.IsNullOrWhiteSpace(criteria.StudentID))
            query = query.Where(s => s.ID.Contains(criteria.StudentID));

        return query.ToList();
    }
}
