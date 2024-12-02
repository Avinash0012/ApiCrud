using APICRUD.Entities_;

namespace APICRUD.Infratructure.Interface
{
    public interface IStudent
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<int> Save(Student stu);

        Task<int> Delete(int id);

        Task<Student> Edit(int id);
    }
}
