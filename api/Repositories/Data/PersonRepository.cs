using api.Data;
using api.Models;
using api.Model.DTO;
using Microsoft.EntityFrameworkCore;

using static api.Repositories.PictureRepository;

namespace api.Repositories.Data
{
    public class PersonRepository(AppDbContext db) : DataRepositoryBase<PersonEntity, PersonDTO>(db)
    {
        public async override Task<IEnumerable<PersonEntity>> GetListAsync()
        {
            IEnumerable<PersonEntity> personList = await
                _db.Person
                .Include(p => p.PassportEntity)
                .Include(p => p.StudentEntity)
                .ToArrayAsync();

            return personList;
        }

        public async Task<IEnumerable<PersonEntity>> GetListByCuratorAsync(Guid curatorId)
        {
            List<PersonEntity> personList = [];

            GroupEntity? groupEntity = await
                _db.Group
                .Include(g => g.StudentEntityList)
                .FirstOrDefaultAsync(g => g.CuratorId == curatorId);

            if (groupEntity is not null)
            {
                foreach (StudentEntity studentEntity in groupEntity.StudentEntityList)
                {
                    PersonEntity? personEntity = await _db.Person.FirstOrDefaultAsync(p => p.Id == studentEntity.PersonEntityId);

                    if (personEntity is not null)
                    {
                        personEntity.StudentEntity = studentEntity;

                        personList.Add(personEntity);
                    }

                }
            }

            return personList;
        }

        public async Task<IEnumerable<PersonEntity>> GetListByTeacherAsync(Guid teacherId)
        {
            List<PersonEntity> personEntityList = [];

            List<SubjectEntity> subjectEntityList = 
                _db
                .Subject
                .Where(s => s.TeacherId == teacherId)
                .ToList();

            foreach (SubjectEntity subjectEntity in subjectEntityList)
            {
                GroupEntity? groupEntity = await
                _db.Group
                .Include(g => g.StudentEntityList)
                .FirstOrDefaultAsync(g => g.Id == subjectEntity.GroupEntityId);

                if (groupEntity is not null)
                {
                    foreach (StudentEntity studentEntity in groupEntity.StudentEntityList)
                    {
                        PersonEntity personEntity = await
                        _db
                        .Person
                        .FirstOrDefaultAsync(p => p.Id == studentEntity.PersonEntityId)
                        ?? throw new Exception("Person on Student is null!");

                        personEntity.StudentEntity = studentEntity;

                        if (personEntity is not null) personEntityList.Add(personEntity);
                    }
                }
            }

            return personEntityList;
        }

        public async override Task<PersonEntity?> GetAsync(int personEntityId)
        {
            PersonEntity? personEntity = await
                _db.Person
                .Include(p => p.PassportEntity)
                .Include(p => p.StudentEntity)
                .FirstOrDefaultAsync(p => p.Id == personEntityId);

            return personEntity;
        }

        public async Task<PersonEntity?> GetAsync(string firstName, string middleName, string lastName)
        {
            PersonEntity? personEntity = await
                _db.Person
                .Include(p => p.PassportEntity)
                .Include(p => p.StudentEntity)
                .FirstOrDefaultAsync
                (
                    p =>
                        StringComparer.CurrentCultureIgnoreCase.Compare(p.FirstName, firstName) == 0 &&
                        StringComparer.CurrentCultureIgnoreCase.Compare(p.MiddleName, middleName) == 0 &&
                        StringComparer.CurrentCultureIgnoreCase.Compare(p.LastName, lastName) == 0
                );

            return personEntity;
        }

        public override PersonEntity Create(PersonDTO personDTO)
        {
            return new()
            {
                FirstName = personDTO.FirstName,
                MiddleName = personDTO.MiddleName,
                LastName = personDTO.LastName,
                BirthDate = personDTO.BirthDate,
                Sex = personDTO.Sex,
                AvatarFileName = UploadPersonAvatarAsync(personDTO.AvatarFile, personDTO.Sex).Result,
            };
        }

        public async override Task UpdateAsync(PersonEntity personEntityToUpdate, PersonDTO personDTO)
        {
            personEntityToUpdate.FirstName = personDTO.FirstName;
            personEntityToUpdate.MiddleName = personDTO.MiddleName;
            personEntityToUpdate.LastName = personDTO.LastName;
            personEntityToUpdate.BirthDate = personDTO.BirthDate;
            personEntityToUpdate.Sex = personDTO.Sex;
            personEntityToUpdate.AvatarFileName = await UploadPersonAvatarAsync(personDTO.AvatarFile, personEntityToUpdate.Sex);

            await _db.SaveChangesAsync();
        }
    }
}