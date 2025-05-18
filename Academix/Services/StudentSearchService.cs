using Academix.Models;

public class StudentSearchService
{
    public List<StudentDisplayModel> AllStudents { get; set; } = new List<StudentDisplayModel>
    {
        new StudentDisplayModel { ID = "HS001", Name = "Nguyễn Văn A", Gender = "Nam", ClassName = "10/1", GPA1 = 8.2, GPA2 = 8.5, Status = "Đang học"},
        new StudentDisplayModel { ID = "HS002", Name = "Trần Thị B", Gender = "Nữ", ClassName = "10/1", GPA1 = 7.5, GPA2 = 7.8, Status = "Đang học"},
        new StudentDisplayModel { ID = "HS003", Name = "Lê Văn C", Gender = "Nam", ClassName = "10/2", GPA1 = 6.0, GPA2 = 6.5, Status = "Nghỉ học"},
        new StudentDisplayModel { ID = "HS004", Name = "Phạm Thị D", Gender = "Nữ", ClassName = "11/1", GPA1 = 9.1, GPA2 = 9.4, Status = "Đang học"},
        new StudentDisplayModel { ID = "HS005", Name = "Đặng Văn E", Gender = "Nam", ClassName = "11/2", GPA1 = 5.0, GPA2 = 4.8, Status = "Nghỉ học" },
        new StudentDisplayModel { ID = "HS006", Name = "Hoàng Thị F", Gender = "Nữ", ClassName = "11/3", GPA1 = 6.2, GPA2 = 6.9, Status = "Đang học"},
        new StudentDisplayModel { ID = "HS007", Name = "Mai Văn G", Gender = "Nam", ClassName = "12/1", GPA1 = 8.0, GPA2 = 8.3, Status = "Đang học"},
        new StudentDisplayModel { ID = "HS008", Name = "Ngô Thị H", Gender = "Nữ", ClassName = "12/1", GPA1 = 9.0, GPA2 = 9.5, Status = "Đang học"},
        new StudentDisplayModel { ID = "HS009", Name = "Bùi Văn I", Gender = "Nam", ClassName = "12/2", GPA1 = 7.2, GPA2 = 7.1, Status = "Nghỉ học"},
        new StudentDisplayModel { ID = "HS010", Name = "Đỗ Thị K", Gender = "Nữ", ClassName = "12/3", GPA1 = 8.5, GPA2 = 8.6, Status = "Đang học"},
        new StudentDisplayModel { ID = "HS011", Name = "Trịnh Văn L", Gender = "Nam", ClassName = "10/1", GPA1 = 5.5, GPA2 = 5.8, Status = "Đang học"},
        new StudentDisplayModel { ID = "HS012", Name = "Phan Thị M", Gender = "Nữ", ClassName = "11/1", GPA1 = 7.8, GPA2 = 7.7, Status = "Đang học"},
        new StudentDisplayModel { ID = "HS013", Name = "Vũ Văn N", Gender = "Nam", ClassName = "11/2", GPA1 = 6.3, GPA2 = 6.2, Status = "Nghỉ học" },
        new StudentDisplayModel { ID = "HS014", Name = "Lý Thị O", Gender = "Nữ", ClassName = "10/2", GPA1 = 9.0, GPA2 = 8.9, Status = "Đang học"},
        new StudentDisplayModel { ID = "HS015", Name = "Nguyễn Văn P", Gender = "Nam", ClassName = "12/3", GPA1 = 7.0, GPA2 = 7.4, Status = "Đang học"},
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
